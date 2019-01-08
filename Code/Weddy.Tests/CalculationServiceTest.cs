using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Weddy.Models;
using Weddy.Services;

namespace Weddy.Tests
{
    [TestClass]
    public class CalculationServiceTest
    {
        public static ICalculationService CalculationService { get; set; }
        
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            CalculationService = new CalculationService();
        }


        [TestMethod]
        public void EntriesWithAverageAmountShouldHaveZeroDeviation()
        {
            var pool = new Pool {Entries = new List<Entry>()};

            pool.Entries.Add(new Entry { Amount = 100 });
            pool.Entries.Add(new Entry { Amount = 100 });
            pool.Entries.Add(new Entry { Amount = 100 });

            CalculationService.UpdatePool(pool);

            Assert.IsTrue(pool.Entries.All(e => e.DeviationMax == 0));
            Assert.IsTrue(pool.Entries.All(e => e.DeviationMin == 0));
        }

        [TestMethod]
        public void EntriesWithSameDistanceFromAverageShouldSimilar()
        {
            var pool = new Pool { Entries = new List<Entry>() };

            var entry1 = new Entry { Amount = 100 };
            var entry2 = new Entry { Amount = 300 };
            pool.Entries.Add(entry1);
            pool.Entries.Add(entry2);

            CalculationService.UpdatePool(pool);

            Assert.AreEqual(-50, entry1.DeviationMin);
            Assert.AreEqual(-25, entry1.DeviationMax);
            Assert.AreEqual(25, entry2.DeviationMin);
            Assert.AreEqual(50, entry2.DeviationMax);
        }

        [TestMethod]
        public void PoolWith2EntriesShouldHaveLargeSteps()
        {
            var pool = new Pool { Entries = new List<Entry>() };

            var entry1 = new Entry { Amount = 100 };
            var entry2 = new Entry { Amount = 300 };
            pool.Entries.Add(entry1);
            pool.Entries.Add(entry2);

            CalculationService.UpdatePool(pool);

            Assert.AreEqual(25, Math.Abs(Math.Abs(entry1.DeviationMin) - Math.Abs(entry1.DeviationMax)));
        }

        [TestMethod]
        public void PoolWith3EntriesShouldHaveMediumSteps()
        {
            var pool = new Pool { Entries = new List<Entry>() };

            var entry1 = new Entry { Amount = 100 };
            var entry2 = new Entry { Amount = 300 };
            var entry3 = new Entry { Amount = 300 };
            pool.Entries.Add(entry1);
            pool.Entries.Add(entry2);
            pool.Entries.Add(entry3);

            CalculationService.UpdatePool(pool);

            Assert.AreEqual(20, Math.Abs(Math.Abs(entry1.DeviationMin) - Math.Abs(entry1.DeviationMax)));
        }

        [TestMethod]
        public void PoolWith4EntriesShouldHaveMediumSteps()
        {
            var pool = new Pool { Entries = new List<Entry>() };

            for (int i = 0; i < 4; i++)
            {
                var entry1 = new Entry { Amount = 100 + i * 10 };
                pool.Entries.Add(entry1);
            }

            CalculationService.UpdatePool(pool);

            var entry = pool.Entries.First();

            Assert.AreEqual(15, Math.Abs(Math.Abs(entry.DeviationMin) - Math.Abs(entry.DeviationMax)));
        }

        [TestMethod]
        public void PoolWith5EntriesShouldHaveMediumSteps()
        {
            var pool = new Pool { Entries = new List<Entry>() };

            for (int i = 0; i < 5; i++)
            {
                var entry1 = new Entry { Amount = 100 + i * 10 };
                pool.Entries.Add(entry1);
            }

            CalculationService.UpdatePool(pool);

            var entry = pool.Entries.First();

            Assert.AreEqual(10, Math.Abs(Math.Abs(entry.DeviationMin) - Math.Abs(entry.DeviationMax)));
        }

        [TestMethod]
        public void PoolWith10EntriesShouldHaveMediumSteps()
        {
            var pool = new Pool { Entries = new List<Entry>() };

            for (int i = 0; i < 10; i++)
            {
                var entry1 = new Entry { Amount = 100 + i * 10 };
                pool.Entries.Add(entry1);
            }

            CalculationService.UpdatePool(pool);

            var entry = pool.Entries.First();
            Assert.AreEqual(2, Math.Abs(Math.Abs(entry.DeviationMin) - Math.Abs(entry.DeviationMax)));
        }

        [TestMethod]
        public void EntriesWithSameDistanceFromAverageShouldSimilar2()
        {
            var pool = new Pool { Entries = new List<Entry>() };

            var entry1 = new Entry {Amount = 500};
            var entry2 = new Entry {Amount = 300};

            pool.Entries.Add(entry1);
            pool.Entries.Add(entry2);

            CalculationService.UpdatePool(pool);


            Assert.AreEqual(0, entry1.DeviationMin);
            Assert.AreEqual(25, entry1.DeviationMax);
            Assert.AreEqual(-25, entry2.DeviationMin);
            Assert.AreEqual(0, entry2.DeviationMax);


        }
    }
}
