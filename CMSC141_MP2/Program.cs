using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CMSC141_MP2 {

    class Program {
        static void Main(string[] args) {
            PuzzleState PS = new PuzzleState();
        }
    }

    class PuzzleState {
        List<char> LeftBank;
        List<char> RightBank;

        public PuzzleState() {
            LeftBank = new List<char>();
            RightBank = new List<char>();
            Reset();
        }

        public void Reset() {
            LeftBank.Clear();
            LeftBank.Add('M');
            LeftBank.Add('L');
            LeftBank.Add('R');
            LeftBank.Add('C');
            RightBank.Clear();
        }

    }
}
