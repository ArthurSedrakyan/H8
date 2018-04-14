using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Loops
{
    class Program
    {
        static void Main(string[] args)
        {
            //ParallelLoops.ParallelFor(0, 20, x => Console.WriteLine($"num = {x}"));

            List<int> list = new List<int>(25);
            Random rnd = new Random();

            for (int i = 0; i < 25; i++)
            {
                list.Add(rnd.Next(1, 30));
            }

            //ParallelLoops.ParallelForEach<int>(list, x => Console.WriteLine($"list {x}"));

            ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };
            CancellationToken ct = new CancellationToken(false);
            po.CancellationToken = ct;


            ParallelLoops.ParallelForEachWithOptions(list, po, x => Console.WriteLine($"list {x}dghdhd"));

            Console.ReadLine();
        }


    }
}
