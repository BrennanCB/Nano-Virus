using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus_BrennanBuitendag
{
    class CellArea
    {
        private static CellArea instance = null;
        private static readonly object lockerObj = new object();

        private int cycleNumber = 0;

        private Random rng = new Random();

        private List<Cell> redBloodCells;
        private List<Cell> whiteBloodCells;
        private List<Cell> tumorousCells;
        private NanoVirus nanoVirus;
        private int numberOfDestroyedCells;

        private CellArea()
        {
            redBloodCells = new List<Cell>();
            whiteBloodCells = new List<Cell>();
            tumorousCells = new List<Cell>();

            numberOfDestroyedCells = 0;

            for (int i = 0; i < 100; i++)
            {
                Cell cell = GenerateCell(i);

                switch (cell.CellType)
                {
                    case CellType.RedBloodCell:
                        redBloodCells.Add(cell);
                        break;
                    case CellType.WhiteBloodCell:
                        whiteBloodCells.Add(cell);
                        break;
                    case CellType.TumorousCell:
                        tumorousCells.Add(cell);
                        break;
                    default:
                        break;
                }
            }

            int startIndex = rng.Next(0, RedBloodCells.Count);

            nanoVirus = new NanoVirus(RedBloodCells[startIndex]);
        }

        public static CellArea GetInstance
        {
            get 
            {
                if (instance == null)
                    instance = new CellArea();

                return instance;
            }
        }

        public List<Cell> RedBloodCells
        {
            get { return redBloodCells; }
            set { redBloodCells = value; }
        }

        public List<Cell> WhiteBloodCells
        {
            get { return whiteBloodCells; }
            set { whiteBloodCells = value; }
        }

        public List<Cell> TumorousCells
        {
            get { return tumorousCells; }
            set { tumorousCells = value; }
        }

        public Cell NanoVirusCell
        {
            get
            {
                return nanoVirus.Cell;
            }
        }

        public int NumberOfDestoryedCells
        {
            get
            {
                return numberOfDestroyedCells;
            }
        }

        public int CycleNumber
        {
            get
            {
                return cycleNumber;
            }
            set
            {
                cycleNumber = value;
            }
        }

        public Cell GenerateCell(int id)
        {
            int cellTypeProb = rng.Next(1, 101);

            CellType cellType;

            if (cellTypeProb <= 5)
                cellType = CellType.TumorousCell;
            else if (cellTypeProb > 5 && cellTypeProb <= 30)
                cellType = CellType.WhiteBloodCell;
            else
                cellType = CellType.RedBloodCell;

            int x = rng.Next(1, 5001);
            int y = rng.Next(1, 5001);
            int z = rng.Next(1, 5001);

            return new Cell(id, cellType, x, y, z);
        }

        public void MoveTumourCells()
        {
            int tumorCount = TumorousCells.Count;

            for (int i = 0; i < tumorCount; i++)
            {
                Cell tumorousCell = TumorousCells[i];
                Cell infectedCell = null;

                if (RedBloodCells.Count > 0)
                {
                    int smallestDistance = tumorousCell.CalculateDistance(RedBloodCells[0]);
                    int smallestCell = 0;

                    for (int j = 1; j < RedBloodCells.Count; j++)
                    {
                        int distance = tumorousCell.CalculateDistance(RedBloodCells[j]);

                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                            smallestCell = j;
                         }
                    }

                    infectedCell = RedBloodCells[smallestCell];

                    RedBloodCells.RemoveAt(smallestCell);

                    
                }
                else if (WhiteBloodCells.Count > 0)
                {
                    int smallestDistance = tumorousCell.CalculateDistance(WhiteBloodCells[0]);
                    int smallestCell = 0;

                    for (int j = 1; j < WhiteBloodCells.Count; j++)
                    {
                        int distance = tumorousCell.CalculateDistance(WhiteBloodCells[j]);

                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                            smallestCell = j;
                        }
                    }

                    infectedCell = WhiteBloodCells[smallestCell];

                    WhiteBloodCells.RemoveAt(smallestCell);
                }

                if (infectedCell != null)
                {
                    infectedCell.CellType = CellType.TumorousCell;

                    tumorousCells.Add(infectedCell);
                }
            }
        }

        public void MoveVirus()
        {
            if(nanoVirus.CellID == -1)
            {
                List<Cell> cellsInRange = new List<Cell>(); 

                for (int i = 0; i < TumorousCells.Count; i++)
                {
                    if (nanoVirus.Cell.CalculateDistance(TumorousCells[i]) <= 5000)
                    {
                        cellsInRange.Add(TumorousCells[i]);
                    }
                }

                for (int i = 0; i < RedBloodCells.Count; i++)
                {
                    if (nanoVirus.Cell.CalculateDistance(RedBloodCells[i]) <= 5000)
                    {
                        cellsInRange.Add(RedBloodCells[i]);
                    }
                }

                for (int i = 0; i < WhiteBloodCells.Count; i++)
                {
                    if (nanoVirus.Cell.CalculateDistance(WhiteBloodCells[i]) <= 5000)
                    {
                        cellsInRange.Add(WhiteBloodCells[i]);
                    }
                }

                if (cellsInRange.Count > 0)
                {
                    int moveToIndex = rng.Next(0, cellsInRange.Count);

                    Cell moveToCell = cellsInRange[moveToIndex];

                    switch (moveToCell.CellType)
                    {
                        case CellType.RedBloodCell: nanoVirus.Cell = RedBloodCells[RedBloodCells.IndexOf(moveToCell)];
                            break;
                        case CellType.WhiteBloodCell: nanoVirus.Cell = WhiteBloodCells[WhiteBloodCells.IndexOf(moveToCell)];
                            break;
                        case CellType.TumorousCell:  nanoVirus.Cell = TumorousCells[TumorousCells.IndexOf(moveToCell)];
                            break;
                        default:
                            break;
                    }
                }
            }
            else if(nanoVirus.Cell.CellType == CellType.TumorousCell)
            {
                int index = TumorousCells.IndexOf(nanoVirus.Cell);

                TumorousCells.RemoveAt(index);
                numberOfDestroyedCells++;

                nanoVirus.CellID = -1;
            }
        }

        private class NanoVirus
        {
            private int cellID;
            private Cell cell;

            public int CellID
            {
                get { return cellID; }
                set { cellID = value; }
            }

            public Cell Cell
            {
                get { return cell; }
                set
                {
                    cell = value;
                    cellID = value.ID;
                }
            }

            public NanoVirus(Cell cell)
            {
                this.Cell = cell;
            }
        }

        public override string ToString()
        {
            int redBloodCellsRemaining = RedBloodCells.Count;
            int whiteBloodCellsRemaining = WhiteBloodCells.Count;
            int tumorousCellsRemaining = TumorousCells.Count;
            int cellsRemaining = redBloodCellsRemaining + whiteBloodCellsRemaining + tumorousCellsRemaining;

            string line = String.Format("Cycle Number: {1}{0}" +
                "Number of Cells Remaining: {2}{0}" +
                "Number of Red Blood Cells {3}{0}" +
                "Number of White Blood Cells: {4}{0}" +
                "Number of Tumor Cells: {5}{0}" +
                "Number of Tumor Cells Destroyed: {6}{0}" +
                "--------------------------------------",
                Environment.NewLine, CycleNumber, cellsRemaining, redBloodCellsRemaining, whiteBloodCellsRemaining, tumorousCellsRemaining, NumberOfDestoryedCells);

            return line;
        }
    }
}