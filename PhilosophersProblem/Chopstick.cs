using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PhilosopherProblem
{
    class Chopstick
    {
        public int Id { get; set; }
        public Mutex Mutex { get; init; } = new Mutex();
        public static List<Chopstick> InitializeChopsticks(int n)
        {
            return Enumerable.Range(1, n).ToList().Select(i => new Chopstick { Id = i }).ToList();
        }

        public override string ToString()
        {
            return $"Stapic {Id}";
        }
    }
}
