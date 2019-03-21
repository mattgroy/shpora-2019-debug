using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPEG.Utilities
{
    public static class MathEx
    {
        public static double SumByTwoVariables(int from1, int to1, int from2, int to2, Func<int, int, double> function)
        {
            var sum = 0.0;

            for (var i = from1; i < to1; i++)
            for (var j = from2; j < to2; j++)
            {
                sum += function(i, j);
            }

            return sum;
        }

        public static void LoopByTwoVariables(int from1, int to1, int from2, int to2, Action<int, int> function)
        {
            Parallel.For(from1, to1, i => Parallel.For(from2, to2, j => function(i, j)));
        }

        public static void LoopByTwoVariablesStepped(int from1, int to1, int from2, int to2, int step,
            Action<int, int> function)
        {
            Parallel.ForEach(SteppedRange(from1, to1, step), i =>
                Parallel.ForEach(SteppedRange(from2, to2, step), j => 
                    function(i, j)));
        }
        
        private static IEnumerable<int> SteppedRange(int from, int to, int step) {
            for (var i = from; i < to; i += step) {
                yield return i;
            }
        }

//        public static List<(T, T)> CombineListsToTuples<T>(IEnumerable<T> first, IEnumerable<T> second)
//        {
//            var resTuples = new List<(T, T)>();
//            var enumerable = second.ToList();
//            foreach (var i in first)
//            foreach (var j in enumerable)
//            {
//                resTuples.Add((i, j));
//            }
//
//            return resTuples;
//        }
    }
}