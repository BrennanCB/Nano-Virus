using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NanoVirus_BrennanBuitendag
{
    class Program
    {
        static void Main(string[] args)
        {
            CellArea cellArea = CellArea.GetInstance;

            bool simulationOver = false;

            do
            {
                cellArea.CycleNumber++;

                cellArea.VirusPerfromAction();

                if (cellArea.CycleNumber % 5 == 0)
                    cellArea.InfectNewCells();

                if (cellArea.TumorousCells.Count == 0 || (cellArea.RedBloodCells.Count + cellArea.WhiteBloodCells.Count) == 0)
                    simulationOver = true;

                Console.WriteLine(cellArea.ToString());

                FileWriter.WriteToFile(cellArea.CycleNumber);
            } while (!simulationOver);

            Console.WriteLine("\nSimulation Completed");
            Console.ReadKey();
        }
    }
}
