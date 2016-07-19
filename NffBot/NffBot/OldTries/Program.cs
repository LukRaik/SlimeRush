//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Automation;
//using System.Windows.Forms;
//using System.Windows.Forms.VisualStyles;

//namespace NffBot
//{
//    class Programasdasd
//    {
//        [DllImport("User32.dll")]
//        static extern int SetForegroundWindow(IntPtr point);


//        private static string procName;

//        private static string[] script;

//        private static bool WorkProgram = true;

//        private static Task<string[]> WaitForScriptAsync()
//        {
//            bool endOfScript = false;
//            Console.WriteLine("Wpisz skrypt, wpisz [END] by zakończyć");
//            List<string> lines = new List<string>();
//            while (endOfScript != true)
//            {
//                var line = Console.ReadLine();
//                if (line == "[END]") endOfScript = true;
//                else lines.Add(line);
//            }
//            Console.WriteLine("Wpisano treść skryptu");
//            return Task.FromResult(lines.ToArray());
//        }
//        static void asdas(string[] args)
//        {
//            Task.Run(async () =>
//            {
//                Process.GetProcesses().ToList().ForEach(x=>  Console.WriteLine(x.ProcessName+" Title:"+ (string.IsNullOrEmpty(x.MainWindowTitle)? "":x.MainWindowTitle)));
//                Console.WriteLine("Podaj nazwę procesu");
//                Console.WriteLine("Wpisz 1 by wybrać naruto :D");
//                var line = Console.ReadLine();

//                procName = line == "1" ? "Naruto Final Fight" : line;

//                Console.WriteLine($"Wybrano proces: {procName}");

//                var p = Process.GetProcesses().FirstOrDefault(x => x.ProcessName == procName);
//                if (p != null)
//                {
//                    IntPtr h = p.MainWindowHandle;
//                    SetForegroundWindow(h);
//                    while (WorkProgram)
//                    {
//                        Console.WriteLine("Znaleziono proces\n");
//                        Console.WriteLine("Podaj formę podania skryptu:");
//                        Console.WriteLine("[1]Ścieżka pliku");
//                        Console.WriteLine("[2]Wpisz skrypt ręcznie");
//                        var result = Console.ReadLine();

//                        switch (result)
//                        {
//                            case "1":
//                                Console.WriteLine("Podaj ścieżkę do skryptu");
//                                var path = Console.ReadLine();
//                                if (path == "asd") path = "";
//                                if (path == null || !File.Exists(path))
//                                {
//                                    Console.WriteLine("Nie znaleziono pliku");
//                                    continue;
//                                }
//                                script = File.ReadAllLines(path);
//                                break;
//                            case "2":
//                                Console.WriteLine("Wpisz treść skryptu");
//                                script = await WaitForScriptAsync();
//                                break;
//                            default:
//                                continue;
//                        }

//                        ScriptWorker worker = new ScriptWorker(script, h);
//                        var ts = new CancellationTokenSource();
//                        var token = ts.Token;
//                        var myTask = worker.Run(token);
//                        Console.WriteLine("Wpisz [END] by zakonczyc");
//                        while (WorkProgram)
//                        {
//                            line = Console.ReadLine();
//                            if (line == "[END]")
//                            {
//                                WorkProgram = false;
//                                ts.Cancel();
//                            }
//                        }
                        
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("Nie znaleziono procesu");
//                }



//                Console.WriteLine("Koniec programu, nacisnij ENTER by zakończyć");
//                Console.ReadLine();
//                return Task.FromResult(true);
//            }).Wait();

//        }




//    }
//}
