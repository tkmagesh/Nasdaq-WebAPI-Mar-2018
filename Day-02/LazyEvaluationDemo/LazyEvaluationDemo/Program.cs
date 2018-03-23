using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyEvaluationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 0;
            foreach (var number in GetNumbers())
            {
                Console.WriteLine(number);
                ++count;
                if (count >= 2) break;
            }

            Console.ReadLine();
        }

        public static IEnumerable<int> GetNumbers()
        {
            yield return 10;
            yield return 20;
            yield return 30;
            yield return 40;
        }
    }
}
