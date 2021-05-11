using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PhilosopherProblem
{
    class Stapic
    {
        public int Id { get; set; }
        public Mutex Mutex { get; set; } = new Mutex();
        public static List<Stapic> InicijalizujStapice(int n)
        {
            return Enumerable.Range(1, n).ToList().Select(i => new Stapic { Id = i }).ToList();
        }

        public override string ToString()
        {
            return $"Stapic {Id}";
        }
    }
}
