using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PhilosopherProblem
{
    class Filozof
    {
        public int Id { get; set; }
        public StanjeFilozofa Stanje { get; set; } = StanjeFilozofa.Razmislja;
        public Stapic DesniStapic { get; set; }
        public Stapic LeviStapic { get; set; }

        public enum StanjeFilozofa
        {
            Razmislja,
            Jede,
            Zavrsio
        }

        public static List<Filozof> InicijalizujFilozofe(int n)
        {
            return Enumerable.Range(1, n).ToList().Select(i => new Filozof { Id = i }).ToList();
        }
        public void Deluj()
        {
            while (Stanje != StanjeFilozofa.Zavrsio)
            {
                if (DesniStapic.Mutex.WaitOne(100))
                {
                    Console.WriteLine($"Filozof {Id} je uzeo desni {DesniStapic}.");
                    if (LeviStapic.Mutex.WaitOne(100))
                    {
                        Console.WriteLine($"Filozof {Id} je uzeo levi {LeviStapic}.");

                        Stanje = StanjeFilozofa.Jede;
                        Console.WriteLine($"Filozof {Id} jede.");
                        Thread.Sleep(Program.Random.Next(0, 5000));

                        Stanje = StanjeFilozofa.Zavrsio;
                        Console.WriteLine($"Filozof {Id} je zavrsio.");

                        LeviStapic.Mutex.ReleaseMutex();
                        DesniStapic.Mutex.ReleaseMutex();
                        Console.WriteLine($"Filozof {Id} je spustio levi {LeviStapic} i desni {DesniStapic}.");
                    }
                    else
                    {
                        DesniStapic.Mutex.ReleaseMutex();
                        Console.WriteLine($"Filozof {Id} je spustio desni {DesniStapic}.");
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"Filozof {Id}. Stanje {Stanje}. Levi {LeviStapic}. Desni {DesniStapic}.";
        }
    }
}
