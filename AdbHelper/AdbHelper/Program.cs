using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdbHelper
{
    class Program
    {
        static Dictionary<string, string> GetOpts(string[] args)
        {
            if (args.Length == 0) return new Dictionary<string, string>();

            if ((args.Length + 1) % 2 == 0)
            {
                throw new Exception("Nieprawidłowa ilość argumentów (liczba parzysta)");
            }

            var buffor = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i += 2)
            {
                buffor.Add(args[i - 1], args[i]);
            }

            return buffor;
        }

        private static void GoIdle()
        {
            Console.WriteLine("Czekam na enter");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var arguments = GetOpts(args);

            if (arguments.Count == 0)
            {
                Console.WriteLine("Usage AdbHelper -p 5555 -ip 192.168.0.2");
                GoIdle();
                return;
            }

            var processInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                Arguments = $"tcpip {arguments["-p"]}",
                FileName = "adb",
                Verb = "runas"
            };




            using (var process = Process.Start(processInfo))
            {
                if (process == null)
                {
                    Console.WriteLine("Coś poszło nie tak z uruchomieniem usługi na telefonie");
                    return;
                }
                process.WaitForExit();
            }

            processInfo.Arguments = $"connect {arguments["-ip"]}";

            using (var process = Process.Start(processInfo))
            {
                if (process == null)
                {
                    Console.WriteLine("Coś poszło nie tak z próbą połączenia z telefonem");
                    return;
                }
                process.WaitForExit();
            }



            Console.WriteLine("Udane uruchomienie adb przez wifi");

            GoIdle();
            return;
        }
    }
}
