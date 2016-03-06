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
            Pz.PrintState();
            Console.WriteLine(Pz.TrySolve("RNLNCN"));
        }
    }

    class Puzzle {

        List<char> EastBank;
        string InvalidStates = "RC,LR,LRC,MC,ML";

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

                PrintState();
            }
            return IsValidState();
        }

        public bool IsValidState() {

            if(!(EastBank.Contains('L') && EastBank.Contains('C') && EastBank.Contains('R') && EastBank.Contains('M'))) {
                return false;
            }

            string[] IStates = InvalidStates.Split(',');
            bool IsValid = true;
            foreach (var State in IStates) {
                char[] Eq = State.ToCharArray();
                List<char> ToCompare = new List<char>();
                foreach (char c in Eq) {
                    ToCompare.Add(c);
                }
                /* checks if the EastBank State is the same as at least on of the Specified invalid states */
                IsValid = !(EastBank.All(ToCompare.Contains) && ToCompare.Count == EastBank.Count);
                if(IsValid == true) {
                    break;
                }
            }

            /* return true if the EastBank state is one of the specified invalid states, false otherwise. */
            return IsValid;
        }
    }
}
