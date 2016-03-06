using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CMSC141_MP2 {

    class Program {
        static void Main(string[] args) {
            Puzzle Pz = new Puzzle();
            string[] PossibleSolutions = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "mp2.in"));
            List<string> Results = new List<string>();
            int i = 1;
            foreach(var Solution in PossibleSolutions) {
                Console.WriteLine(i);
                if (Pz.TrySolve(Solution) == true) {
                    Results.Add("OK");
                }
                else {
                    Results.Add("NG");
                }
                i++;
            }
            File.AppendAllLines(Path.Combine(Directory.GetCurrentDirectory(), "mp2.out"), Results);
        }
    }

    class Puzzle {

        List<char> EastBank;
        /*
        a comma-separated collection of all possible invalid states for the East Bank. 
        The invalid state of the opposite bank is inferred
        */
        string InvalidStates = "RC,CR,LR,RL,LRC,CRL,RCL,LCR,RLC,CLR,MC,CM,ML,M,LM";

        public Puzzle() {
            EastBank = new List<char>();
            Reset();
        }

        void Reset() {
            EastBank.Clear();
        }
        
        public void PrintState() {
            char[] T = new Char[] {'M', 'L', 'R', 'C' };
            foreach (var Item in T) {
                if (!EastBank.Contains(Item)) {
                    Console.Write(Item);    
                }
            }
            Console.Write("_");
            foreach(var Item in EastBank) {
                Console.Write(Item);
            }
            Console.WriteLine("");
        }

        public bool TrySolve(string Solution) {
            Reset();
            //PrintState();
            char[] Steps = Solution.ToCharArray();
            foreach (var Step in Steps) {
                if(Step == 'N') {
                    /* man crosses with nothing */
                    if (!EastBank.Contains('M')) {
                        EastBank.Add('M');
                    }
                    else {
                        EastBank.Remove('M');
                    }
                }
                else if(Step == 'R') {
                    /* man crosses with rabbit */
                    if(EastBank.Contains('R') && EastBank.Contains('M')) {
                        EastBank.Remove('R');
                        EastBank.Remove('M');
                    }
                    else if(!EastBank.Contains('R') && !EastBank.Contains('M')) {
                        EastBank.Add('R');
                        EastBank.Add('M');
                    }
                    else {
                        return false;
                    }
                }
                else if(Step == 'L') {
                    /* man crosses with lion */
                    if (EastBank.Contains('L') && EastBank.Contains('M')) {
                        EastBank.Remove('L');
                        EastBank.Remove('M');
                    }
                    else if (!EastBank.Contains('L') && !EastBank.Contains('M')) {
                        EastBank.Add('L');
                        EastBank.Add('M');
                    }
                    else {
                        return false;
                    }
                }
                else if(Step == 'C') {
                    /* man crosses with carrot */
                    if (EastBank.Contains('C') && EastBank.Contains('M')) {
                        EastBank.Remove('C');
                        EastBank.Remove('M');
                    }
                    else if (!EastBank.Contains('C') && !EastBank.Contains('M')) {
                        EastBank.Add('C');
                        EastBank.Add('M');
                    }
                    else {
                        return false;
                    }
                }
                else {
                    /* invalid character */
                    return false;
                }
                
                //PrintState();
                if(IsValidState() == false) {
                    return false;
                }
            }
            
            if(EastBank.LongCount() != 4) {
                //means not all of the animals + man was able to cross
                return false;
            }
            return IsValidState();
        }

        public bool IsValidState() {
            string[] IStates = InvalidStates.Split(',');
            foreach(var s in IStates) {
                if(ArraysEqual(s.ToCharArray(), EastBank.ToArray())) {
                    return false;
                }
            }
            return true;
        }

        //checks if two arrays are equal
        bool ArraysEqual<T>(T[] a1, T[] a2) {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++) {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }
    }
}
