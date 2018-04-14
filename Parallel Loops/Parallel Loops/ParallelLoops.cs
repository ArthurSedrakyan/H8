using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Loops
{
    public static class ParallelLoops
    {
        public static void ParallelFor(int fromInclusive, int toExclusive, Action<int> body)
        {
            for (int i = fromInclusive; i < toExclusive; i++)
            {
              int j = i;
              Task.Run(() => body(j));
               
            }
        }

        public static void ParallelForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
        {
            foreach (var item in source)
            {
                Task.Run(() => body(item));
            }
        }

        public static  void ParallelForEachWithOptions<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
        {
            if (parallelOptions.MaxDegreeOfParallelism <= 0 && parallelOptions.MaxDegreeOfParallelism !=-1)
            {
                throw new ArgumentOutOfRangeException("Your MaxDegreeOfParallelism is smaller then can be");
            }
            if (parallelOptions.CancellationToken.CanBeCanceled)
            {
                throw new OperationCanceledException("Someone had cancel operation");
            }

            Semaphore semaphore = new Semaphore(parallelOptions.MaxDegreeOfParallelism, parallelOptions.MaxDegreeOfParallelism);

            foreach (var item in source)
            {
                Task.Run(() =>
                {
                    semaphore.WaitOne();
                    body(item);
                    semaphore.Release();
                },parallelOptions.CancellationToken);

            }
        }
    }
}
