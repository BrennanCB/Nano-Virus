using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus_BrennanBuitendag
{
    class Cell
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private CellType cellType;

        public CellType CellType
        {
            get { return cellType; }
            set { cellType = value; }
        }

        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private int z;

        public int Z
        {
            get { return z; }
            set { z = value; }
        }

        public Cell(int ID, CellType type, int x, int y, int z)
        {
            this.ID = ID;
            this.CellType = type;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override string ToString()
        {
            return string.Format("Type: {0}\tPosition:{1},{2},{3}",CellType, X, Y, Z);
        }

        //returns the distance between current cell and a given cell
        public int CalculateDistance(Cell cell)
        {
            double xDiff = Math.Pow(this.X - cell.X, 2);
            double yDiff = Math.Pow(this.Y - cell.Y, 2);
            double zDiff = Math.Pow(this.Z - cell.Z, 2);

            return (int)Math.Round(Math.Sqrt(Math.Abs((xDiff + yDiff + zDiff))));
        }
    }
}
