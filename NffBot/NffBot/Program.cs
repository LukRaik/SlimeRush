using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NffBot
{
    class Program
    {
        public enum BotState
        {
            Paused,
            Runing
        }

        private static Task<string[]> WaitForScriptAsync()
        {
            bool endOfScript = false;
            Console.WriteLine("Wpisz skrypt, wpisz [END] by zakończyć");
            List<string> lines = new List<string>();
            while (endOfScript != true)
            {
                var line = Console.ReadLine();
                if (line == "[END]") endOfScript = true;
                else lines.Add(line);
            }
            Console.WriteLine("Wpisano treść skryptu");
            return Task.FromResult(lines.ToArray());
        }

        private static string procName;

        private static string[] script;

        private static bool WorkProgram = true;

        public static void Event(object obj, EventArgs e)
        {
            Console.WriteLine(((ChatReaderEventArgs)e).Line);
        }

        static void Main(string[] args)
        {

            var text = NarutoButtonsDeliver.GetCurChatText();
            Task.Run(async () =>
            {
                string line;
                BotState state = BotState.Runing;
                while (WorkProgram)
                {
                    Console.WriteLine("Znaleziono proces\n");
                    Console.WriteLine("Podaj formę podania skryptu:");
                    Console.WriteLine("[1]Ścieżka pliku");
                    Console.WriteLine("[2]Wpisz skrypt ręcznie");
                    var result = Console.ReadLine();

                    switch (result)
                    {
                        case "1":
                            Console.WriteLine("Podaj ścieżkę do skryptu");
                            var path = Console.ReadLine();
                            if (path == "asd") path = "";
                            if (path == null || !File.Exists(path))
                            {
                                Console.WriteLine("Nie znaleziono pliku");
                                continue;
                            }
                            script = File.ReadAllLines(path);
                            break;
                        case "2":
                            Console.WriteLine("Wpisz treść skryptu");
                            script = await WaitForScriptAsync();
                            break;
                        default:
                            continue;
                    }

                    ScriptWorker worker = new ScriptWorker(script);
                    ChatReader reader = new ChatReader();

                    reader.RegisteredLine += Event;

                    var ts = new CancellationTokenSource();
                    var token = ts.Token;

                    reader.RegisteredLine += (sender, eventArgs) =>
                     {
                         if ((eventArgs as ChatReaderEventArgs).Line.ToLower().Contains("afk") ||
                         (eventArgs as ChatReaderEventArgs).Line.ToLower().Contains("summon"))
                         {
                             ts.Cancel();
                             state = BotState.Paused;
                             NarutoButtonsDeliver.SetForegroundWindow();
                             NarutoButtonsDeliver.SayHi();
                         }
                     };

                    reader.RegisteredLine += (sender, eventArgs) =>
                    {
                        if ((eventArgs as ChatReaderEventArgs).Line.ToLower().Contains("no chakra left"))
                        {
                            worker.OneTimeTasks.Add(new TaskInfo("t.1000:k.tm_rest;w.15000;k.tm_rest"));
                        }
                    };

                    reader.RegisteredLine += (sender, eventArgs) =>
                    {
                        if ((eventArgs as ChatReaderEventArgs).Line.ToLower().Contains("struggle"))
                        {
                            NarutoButtonsDeliver.SetForegroundWindow();
                        }
                    };

                    var myTask1 = reader.Run(token);
                    var myTask = worker.Run(token);
                    Console.WriteLine("Wpisz [END] by zakonczyc");
                    while (WorkProgram)
                    {
                        switch (state)
                        {
                            case BotState.Paused:
                                line = Console.ReadLine();
                                if (line == "[END]")
                                {
                                    ts = new CancellationTokenSource();
                                    token = ts.Token;
                                    myTask = reader.Run(token);
                                    myTask1 = worker.Run(token);
                                    state = BotState.Runing;
                                }
                                break;
                            case BotState.Runing:
                                if (Console.KeyAvailable)
                                {
                                    if (Console.ReadKey(true).Key == ConsoleKey.P)
                                    {
                                        state = BotState.Paused;
                                        Console.WriteLine("Pauza, [END] by uruchomić ponownie");
                                        reader.RegisteredLine -= Event;
                                        ts.Cancel();
                                    }
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    Console.WriteLine("Koniec programu, nacisnij ENTER by zakończyć");
                    Console.ReadLine();
                }
            }).Wait();
        }
    }
}
