using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PhilosopherProblem
{
    class Philosopher
    {
        public int Id { get; set; }
        public PhilosopherState State { get; set; } = PhilosopherState.Thinking;
        public Chopstick RightChopstick { get; set; }
        public Chopstick LeftChopstick { get; set; }

        public enum PhilosopherState
        {
            Thinking,
            Eating,
            Finished
        }

        public static List<Philosopher> InitilaizePhilosophers(int n)
        {
            return Enumerable.Range(1, n).ToList().Select(i => new Philosopher { Id = i }).ToList();
        }
        public void Act()
        {
            while (State != PhilosopherState.Finished)
            {
                if (RightChopstick.Mutex.WaitOne(100))
                {
                    Console.WriteLine($"Philosopher {Id} has taken the right {RightChopstick}.");
                    if (LeftChopstick.Mutex.WaitOne(100))
                    {
                        Console.WriteLine($"Philosopher {Id} has taken the left {LeftChopstick}.");

                        State = PhilosopherState.Eating;
                        Console.WriteLine($"Philosopher {Id} is eating.");
                        Thread.Sleep(Program.Random.Next(0, 5000));

                        State = PhilosopherState.Finished;
                        Console.WriteLine($"Philosopher {Id} has finished.");

                        LeftChopstick.Mutex.ReleaseMutex();
                        RightChopstick.Mutex.ReleaseMutex();
                        Console.WriteLine($"Philosopher {Id} has put down the left {LeftChopstick} and the right {RightChopstick}.");
                    }
                    else
                    {
                        RightChopstick.Mutex.ReleaseMutex();
                        Console.WriteLine($"Philosopher {Id} has put down the right {RightChopstick}.");
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"Philosopher {Id}. State {State}. Left {LeftChopstick}. Right {RightChopstick}.";
        }
    }
}
