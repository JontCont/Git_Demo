
using System.Diagnostics;

namespace TopicsAPI.Exception
{
    public class Efficiency
    {
        //public static Action _action {get;set;}
        public static Func<object> _func { get; set; }
        public long CPU { get; set; }
        public long Memory { get; set; }
        public TimeSpan Time { get; set; }

        //public Efficiency (Action action){
        //    _action = action;
        //}

        public Efficiency(Func<object> func)
        {
            _func = func;
        }

        public object getEfficiency()
        {
            var stopwatch = new Stopwatch();
            var process = Process.GetCurrentProcess();
            var startTime = process.TotalProcessorTime;
            stopwatch.Start();
            _func.Invoke();
            stopwatch.Stop();
            var endTime = process.TotalProcessorTime;

            var executionTime = stopwatch.Elapsed;
            long executionMemory = process.PrivateMemorySize64 / 1024 / 1024;
            long executionCpu = (long)((endTime - startTime).Ticks / stopwatch.Elapsed.Ticks * 100);
            return new 
            {
                Time = executionTime,
                Memory = executionMemory,
                CPU = executionCpu,
            };
        }



    }
}