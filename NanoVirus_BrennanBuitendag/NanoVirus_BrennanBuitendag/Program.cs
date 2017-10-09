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

            //List<Cell> cells = ch.Cells;

            bool simulationOver = false;

            do
            {
                cellArea.CycleNumber++;

                cellArea.MoveVirus();

                if (cellArea.CycleNumber % 5 == 0)
                {
                    cellArea.MoveTumourCells();
                }

                if (cellArea.TumorousCells.Count == 0 || (cellArea.RedBloodCells.Count + cellArea.WhiteBloodCells.Count) == 0)
                    simulationOver = true;

                Console.WriteLine(cellArea.ToString());

                FileWriter.Write(cellArea.CycleNumber);
                Thread.Sleep(1000);
            } while (!simulationOver);

            Console.WriteLine("\nSimulation Completed");

            Console.ReadLine();
        }
    }
}
