using System;
using System.Diagnostics;

namespace Recommender.API.Services
{
    public class ExecutionTimer : IDisposable
    {
        private readonly Stopwatch _stopwatch;

        public ExecutionTimer()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public TimeSpan Elapsed => _stopwatch.Elapsed;

        public void Dispose()
        {
            _stopwatch.Stop();
        }
    }
}
