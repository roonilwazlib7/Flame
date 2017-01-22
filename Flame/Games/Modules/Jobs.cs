using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Flame.Debug;

namespace Flame.Games.Modules
{
    public class Jobs: Module
    {
        Queue<Job<object, object>> _jobs = new Queue<Job<object, object>>();
        List<Thread> _threads = new List<Thread>();
        public Jobs(Game game, int threads):base(game)
        {
            DebugConsole.AddChannel("Flame-Jobs", ConsoleColor.Black, ConsoleColor.White);
            for (int i = 0; i < threads; i++)
            {
                ThreadStart start = new ThreadStart(ExamineQueue);
                Thread thread = new Thread(start);

                thread.Name = "JobsThread-" + i.ToString();
                thread.Start();

                _threads.Add(thread);

                DebugConsole.Output("Flame", "Started " + thread.Name);
            }
        }

        public void TerminateThreads()
        {
            foreach(Thread t in _threads)
            {
                t.Abort();
            }
        }

        public void Add(Func<object, object> execution, Func<object,object> callback)
        {
            _jobs.Enqueue(new Job<object,object>(execution, callback));
        }

        private object examineQueueLock = new object();
        private void ExamineQueue()
        {
            while(true)
            {
                lock (examineQueueLock)
                {
                    if (_jobs.Count != 0)
                    {
                        Job<object, object> job = _jobs.Dequeue();
                        object result = job.ExecutionFunction.Invoke(0);
                        job.CallBackFunction(result);
                    }
                }

            }

        }
    }

    class Job<U,T>
    {
        public Func<U,T> ExecutionFunction { get; }
        public Func<U,T> CallBackFunction { get; }

        public Job(Func<U, T> execution, Func<U, T> callback)
        {
            ExecutionFunction = execution;
            CallBackFunction = callback;
        }
    }
}
