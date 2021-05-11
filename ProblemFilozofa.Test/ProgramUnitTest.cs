using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemFilozofa.Test
{
    [Parallelizable(ParallelScope.Children)]
    public class ProgramUnitTest
    {
        PhilosopherProblem.Program program;

        [SetUp]
        public void Setup()
        {
            program = new();
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void ProgramNTest([ValueSource(nameof(ValidNumbersToTest))] int n)
        {
            PhilosopherProblem.Program.PokreniProblem(n);
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void ProgramInvalidNTest([ValueSource(nameof(InvalidNumbersToTest))] int n)
        {
            Assert.Throws(
                Is.TypeOf<ArgumentException>()
                    .Or.TypeOf<ArgumentOutOfRangeException>(),
                () => PhilosopherProblem.Program.PokreniProblem(n));
        }

        public static List<int> InvalidNumbersToTest()
        {
            return Enumerable.Range(-10, 11).ToList();
        }

        public static List<int> ValidNumbersToTest()
        {
            return Enumerable.Range(2, 9).ToList();
        }

    }
}