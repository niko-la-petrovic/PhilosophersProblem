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
            int n = UnesiN();
            PokreniProblem(n);
        }

        public static void PokreniProblem(int n)
        {
            List<Filozof> filozofi = Filozof.InicijalizujFilozofe(n);
            List<Stapic> stapici = Stapic.InicijalizujStapice(n);

            DodeliStapice(filozofi, in stapici);
            PrikazDodele(filozofi);

            PokreniFilozofe(filozofi);
        }

        private static void PokreniFilozofe(List<Filozof> filozofi)
        {
            List<Thread> threadovi = filozofi.Select(p => new Thread(new ThreadStart(p.Deluj))).ToList();
            threadovi.ForEach(t => t.Start());
            threadovi.ForEach(t => t.Join());

            Console.WriteLine();
            Console.WriteLine("Filozofi su zavrsili.");
        }

        private static void PrikazDodele(List<Filozof> filozofi)
        {
            Console.WriteLine();
            Console.WriteLine("Dodela stapica filozofima je");
            Console.WriteLine(String.Join(Environment.NewLine, filozofi.Select(f => f.ToString()).ToArray()));
            Console.WriteLine();
        }

        private static void DodeliStapice(List<Filozof> filozofi, in List<Stapic> stapic)
        {
            if (filozofi.Count != stapic.Count || filozofi.Count == 0)
                throw new ArgumentException("Nije validna formulacija problema.");

            int n = filozofi.Count;
            int i;
            for (i = 0; i < n - 1; i++)
            {
                filozofi[i].LeviStapic = stapic[i];
                filozofi[i].DesniStapic = stapic[i + 1];
            }
            if (i < filozofi.Count)
            {
                filozofi[i].LeviStapic = stapic[i];
                filozofi[i].DesniStapic = stapic[0];
            }
        }

        private static int UnesiN()
        {
            Console.WriteLine("Problem filozofa.");
            bool validN;
            int n;
            do
            {
                Console.Write("Unesite broj filozofa: ");
                validN = int.TryParse(Console.ReadLine(), out n);
                if (!validN || n <= 1)
                {
                    validN = false;
                    throw new Exception("Nevalidan unos.");
                }
            } while (!validN);
            return n;
        }
    }
}
