using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoVirus_BrennanBuitendag
{
    class NanoVirus
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
}
