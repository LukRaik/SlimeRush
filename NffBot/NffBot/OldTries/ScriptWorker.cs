using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace NffBot
{

    public class WorkerTask
    {
        public enum WorkerTaskType
        {
            Key,
            Wait
        }
        private readonly string _type;

        public WorkerTaskType Type => _type == "k" ? WorkerTaskType.Key : WorkerTaskType.Wait;

        public string Info { get; }

        public WorkerTask(string taskInfo)
        {
            var info = taskInfo.Split('.');
            if (info.Length != 2) throw new InvalidOperationException("Nieprawidłowa komenda");
            _type = info[0];
            Info = info[1];
        }

        public Task GetTask()
        {
            switch (Type)
            {
                case WorkerTaskType.Key:
                    var item = NarutoButtonsDeliver.GetButtons().FirstOrDefault(x => x.Item1.ToLower().Contains(Info));
                    if (item != null)
                    {
                        NarutoButtonsDeliver.ClickButton(item.Item2);
                    }
                    break;
                case WorkerTaskType.Wait:
                    Thread.Sleep(Convert.ToInt32(Info));
                    break;
                default:
                    throw new InvalidOperationException("Coś poszło nie tak");
            }
            return Task.FromResult(true);
        }
    }

    public class TaskInfo
    {
        private readonly List<WorkerTask> _workerTasks = new List<WorkerTask>();

        public int Break { get; }

        public TaskInfo(string line)
        {
            var infoLine = line.Split(':');

            Break = Convert.ToInt32(infoLine[0].Split('.').Last());

            var tasks = infoLine.Last().Split(';');
            foreach (var task in tasks)
            {
                _workerTasks.Add(new WorkerTask(task));
            }
        }

        public async Task GetTask()
        {
            foreach (var task in _workerTasks)
            {
                await task.GetTask();
            }
            return;
        }
    }
    public class ScriptWorker
    {
        private readonly List<TaskInfo> _taskInfos = new List<TaskInfo>();

        private readonly List<WorkerTask> _normalTasks = new List<WorkerTask>();

        public ConcurrentBag<TaskInfo> OneTimeTasks = new ConcurrentBag<TaskInfo>();

        private void LoadTasks(string[] script)
        {
            foreach (var line in script)
            {
                if (line.Contains("t."))
                {
                    _taskInfos.Add(new TaskInfo(line));
                }
                else
                {
                    _normalTasks.Add(new WorkerTask(line));
                }
            }
        }


        public ScriptWorker(string[] script)
        {
            try
            {
                Console.WriteLine("Ładuje skrypt");
                Console.WriteLine("Treść skryptu:");
                Console.WriteLine("[START]");
                script.ToList().ForEach(Console.WriteLine);
                Console.WriteLine("[END]");

                LoadTasks(script);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}\n{ex.InnerException.Message}");
            }
        }

        public Task Run(CancellationToken cancellationToken)
        {
            var taskResult = Task.Factory.StartNew(async () =>
            {
                bool doAction = false;

                var timer = new Timer();
                timer.Interval = _taskInfos.First().Break;
                timer.Elapsed += (sender, args) => doAction = true;
                timer.Start();
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    foreach (var task in _normalTasks)
                    {
                        await task.GetTask();
                    }
                    if (_taskInfos.Any() && doAction)
                    {
                        await _taskInfos.First().GetTask();
                        doAction = false;
                    }
                    if (OneTimeTasks.Any())
                    {
                        TaskInfo task;
                        if (OneTimeTasks.TryTake(out task))
                        {
                            await task.GetTask();
                        }
                    }

                }
            }, cancellationToken);

            return taskResult;
        }
    }
}
