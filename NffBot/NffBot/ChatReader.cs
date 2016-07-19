using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NffBot
{
    public class ChatReaderEventArgs : EventArgs
    {
        public string Line { get; set; }
    }


    public class ChatReader
    {
        private List<string> _lastLines = new List<string>();

        public event EventHandler RegisteredLine;

        public Task Run(CancellationToken cancellationToken)
        {
            var taskResult = Task.Factory.StartNew(() =>
            {

                while (true)
                {
                    Thread.Sleep(100);
                    string[] newLines;
                    if (!_lastLines.Any())
                    {
                        newLines = NarutoButtonsDeliver.GetCurChatText();

                        _lastLines = newLines.ToList();
                    }
                    else
                    {
                        var lastLine = _lastLines.Last();

                        var texts = NarutoButtonsDeliver.GetCurChatText().ToList();

                        var index = texts.LastIndexOf(lastLine);

                        newLines = texts.Skip(index+1).ToArray();

                        foreach (var line in newLines)
                        {
                            _lastLines.Add(line);
                        }
                        foreach (var line in newLines)
                        {
                            OnRegisteredLine(line);
                        }
                    }
                    
                }
            }, cancellationToken);

            return taskResult;
        }

        protected virtual void OnRegisteredLine(string line)
        {
            RegisteredLine?.Invoke(this, new ChatReaderEventArgs() { Line = line });
        }
    }
}
