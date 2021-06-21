using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PhilosopherProblem
{
    public class Program
    {
        public static Random Random { get; set; } = new Random();
        static void Main(string[] args)
        {
            int n = InputPhilosopherNumber();
            Start(n);
        }

        public static void Start(int n)
        {
            List<Philosopher> philosophers = Philosopher.InitilaizePhilosophers(n);
            List<Chopstick> chopsticks = Chopstick.InitializeChopsticks(n);

            AssignChopsticks(philosophers, in chopsticks);
            ShowAssignments(philosophers);

            StartPhilosophers(philosophers);
        }

        private static void StartPhilosophers(List<Philosopher> philosophers)
        {
            List<Thread> threads = philosophers.Select(p => new Thread(new ThreadStart(p.Act))).ToList();
            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());

            Console.WriteLine();
            Console.WriteLine("Philosophers have finished.");
        }

        private static void ShowAssignments(List<Philosopher> philosophers)
        {
            Console.WriteLine();
            Console.WriteLine("The assignment of chopsticks to the philosophers is");
            Console.WriteLine(string.Join(Environment.NewLine, philosophers.Select(f => f.ToString()).ToArray()));
            Console.WriteLine();
        }

        private static void AssignChopsticks(List<Philosopher> philosophers, in List<Chopstick> chopstick)
        {
            if (philosophers.Count != chopstick.Count || philosophers.Count == 0)
                throw new ArgumentException("Invalid problem formulation.");

            int n = philosophers.Count;
            int i;
            for (i = 0; i < n - 1; i++)
            {
                philosophers[i].LeftChopstick = chopstick[i];
                philosophers[i].RightChopstick = chopstick[i + 1];
            }
            if (i < philosophers.Count)
            {
                philosophers[i].LeftChopstick = chopstick[i];
                philosophers[i].RightChopstick = chopstick[0];
            }
        }

        private static int InputPhilosopherNumber()
        {
            Console.WriteLine("Philosopher problem.");
            bool validN;
            int n;
            do
            {
                Console.Write("Enter the number of philosophers: ");
                validN = int.TryParse(Console.ReadLine(), out n);
                if (!validN || n <= 1)
                {
                    validN = false;
                    throw new Exception("Invalid input.");
                }
            } while (!validN);
            return n;
        }
    }
}
