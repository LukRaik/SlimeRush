using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace NffBot
{
    public static class NarutoButtonsDeliver
    {
        private const int BN_CLICKED = 245;

        private static bool _saidHi = false;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        static void GetElements(AutomationElement element, List<AutomationElement> elements)
        {
            foreach (AutomationElement ae in element.FindAll(TreeScope.Children, Condition.TrueCondition))
            {
                elements.Add(ae);
                GetElements(ae, elements);
            }
        }

        public static void ClickButton(AutomationElement elem)
        {
            object pattern;

            if (elem.TryGetCurrentPattern(InvokePattern.Pattern, out pattern))
            {
                SendMessage(elem.Current.NativeWindowHandle, BN_CLICKED, 0, IntPtr.Zero);
            }
        }

        public static void SayHi()
        {
            var first = GetButtons().FirstOrDefault(x => x.Item1.ToLower().Contains("tm_hi"));
            if (first != null && _saidHi == false)
            {
                ClickButton(first.Item2);
                _saidHi = true;
            }
        }

        public static List<Tuple<string, AutomationElement>> GetButtons()
        {
            var p = Process.GetProcesses().FirstOrDefault(x => x.MainWindowTitle == "Naruto Final Fight");

            var ptr = p.MainWindowHandle;
            AutomationElement elem = AutomationElement.FromHandle(ptr);
            var list = new List<AutomationElement>();
            GetElements(elem, list);
            var buttons = list.Where(x => x.Current.ControlType.ProgrammaticName.ToLower().Contains("button")).Select(x => new Tuple<string, AutomationElement>($"{x.Current.Name}", x));

            return buttons.ToList();
        }

        public static void SetForegroundWindow()
        {
            var p = Process.GetProcesses().FirstOrDefault(x => x.MainWindowTitle == "Naruto Final Fight");
            SetForegroundWindow(p.MainWindowHandle);
        }

        private static TextPattern _textPattern;
        private static TextPattern GetTextPattern()
        {
            if (_textPattern == null)
            {
                var p = Process.GetProcesses().FirstOrDefault(x => x.MainWindowTitle == "Naruto Final Fight");

                var ptr = p.MainWindowHandle;
                AutomationElement elem = AutomationElement.FromHandle(ptr);
                var list = new List<AutomationElement>();
                GetElements(elem, list);

                var ae =
                    list
                        .Select(x => new Tuple<string, AutomationElement>($"{x.Current.ControlType.ProgrammaticName.ToLower()}", x))
                        .FirstOrDefault(x => x.Item1.Contains("document"));
                object pattern;

                if (ae.Item2.TryGetCurrentPattern(TextPattern.Pattern, out pattern))
                {
                    var pt = pattern as TextPattern;
                    _textPattern = pt;
                }
            }
            return _textPattern;
        }

        public static string[] GetCurChatText()
        {
            var pt = GetTextPattern();
            string text = "";

            if (pt != null)
            {
                text = pt.GetVisibleRanges().First().GetText(Int32.MaxValue);
            }

            return text.Split(new[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
