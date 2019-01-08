using System;
using System.Collections.Generic;
using System.Linq;
using Weddy.Models;

namespace Weddy.Services
{
    public class CalculationService : ICalculationService
    {
        public void UpdatePool(Pool pool)
        {
            pool.Average = (int)pool.Entries.Average(e => e.Amount);

            int count = pool.Entries.Count;
            var percentageStep = _poolSizeToSteps.Last(t => t.Item1 <= count).Item2;
            foreach (var entry in pool.Entries)
            {
                var ratio = (entry.Amount*100/pool.Average) - 100;
                var sign = Math.Sign(ratio);


                var finalRatio1 = (int)(Math.Ceiling((double)Math.Abs(ratio) / percentageStep));
                var finalRatio2 = Math.Abs((sign * finalRatio1) - sign);

                finalRatio1 = percentageStep*finalRatio1;
                finalRatio2 = percentageStep*finalRatio2;

                entry.DeviationMin = Math.Min(sign * finalRatio1, sign * finalRatio2);
                entry.DeviationMax = Math.Max(sign * finalRatio1, sign * finalRatio2);
            }
        }

        private readonly List<Tuple<int, int>> _poolSizeToSteps = new List<Tuple<int, int>>(
            new[]
                {
                    Tuple.Create(0, 25),
                    Tuple.Create(3, 20),
                    Tuple.Create(4, 15),
                    Tuple.Create(5, 10),
                    Tuple.Create(10, 2),
                    Tuple.Create(int.MaxValue, 1)
                });
    }
}