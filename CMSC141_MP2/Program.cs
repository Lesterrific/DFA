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
            /* loads all lines of the input file to a string array */
            string[] PossibleSolutions = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "mp2.in"));
            List<string> Results = new List<string>();
            int i = 1;
            /* loop through all the lines of the string array to test each combination of moves*/
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
            /* write the results of all the combination of moves to an external file */
            File.AppendAllLines(Path.Combine(Directory.GetCurrentDirectory(), "mp2.out"), Results);
        }
    }

    class Puzzle {

        /*a list of characters to represent the state of East Bank */
        /* the state of the opposite bank is inferred*/
        /* for example, if the East Bank contains RM, the we can infer that the opposite bank contains LC */
        List<char> EastBank;
        /*
        a comma-separated collection of all possible invalid states for the East Bank. 
        The invalid state of the opposite bank is inferred
        This list only contains the states in which one eats the other, not including the screnarios where the
        solution has ended but not all was able to cross. This scenario is handled by the TrySolve() function
        */
        string InvalidStates = "RC,CR,LR,RL,LRC,CRL,RCL,LCR,RLC,CLR,MC,CM,ML,M,LM";

        public Puzzle() {
            EastBank = new List<char>();
            Reset();
        }

        void Reset() {
            EastBank.Clear();
        }
        
        /* function to print the current state of each river banks, WestBank first, then a comma, then the East Bank (i.e., LC_RM) */
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
            /* breaks down the candidate solution into a character array */
            char[] Steps = Solution.ToCharArray();

            /* looping through the char array of steps */
            foreach (var Step in Steps) {
                if(Step == 'N') {
                    /* man crosses with nothing */
                    if (!EastBank.Contains('M')) {
                        /* if a character is to move and it is on the East Bank, remove it from the East Bank 
                        (which will imply that it will be on the West Bank) */
                        EastBank.Add('M');
                    }
                    else {
                        /* if it is not on the East Bank, add it to the east bank state. (which will imply it will no be anymore on the
                        opposite river bank.)*
                        This applies to all other characters.
                        */
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
                    /* if the current state is one of the specified invalid states, then stop the loop and return false meaning NOT GOOD*/
                    return false;
                }
            }
            
            if(EastBank.LongCount() != 4) {
                //means not all of the animals + man was able to cross
                return false;
            }

            return IsValidState();
        }

        /* checks if the current state of the East Bank is a valid state based on the list of invalid states specified*/
        public bool IsValidState() {
            /* splits the comma separated invalid states string into an array of individual states */
            string[] IStates = InvalidStates.Split(',');
            foreach(var s in IStates) {
                if(ArraysEqual(s.ToCharArray(), EastBank.ToArray())) {
                    /* if one of the invalid states matches the current state of the East Bank, return false */
                    return false;
                }
            }
            return true;
        }

        /* checks if two arrays are equal
        - used to check whether the current state is one of the stated invalid 
        states (converted to a char array) */

        bool ArraysEqual<T>(T[] a1, T[] a2) {
            if (ReferenceEquals(a1, a2)) {
                return true;
            }
            if (a1 == null || a2 == null) {
                return false;
            }
            if (a1.Length != a2.Length) {
                return false;
            }

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++) {
                if (!comparer.Equals(a1[i], a2[i])) {
                    return false;
                }
            }
            return true;
        }
    }
}
