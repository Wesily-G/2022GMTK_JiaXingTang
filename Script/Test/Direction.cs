using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Direction
    {
        public int RIndex
        {
            get; set;
        }
        public int CIndex
        {
            get; set;
        }

        public Direction() { }

        public Direction(int rIndex,int cIndex)
        {
            this.RIndex = rIndex;
            this.CIndex = cIndex;
        }
        public static Direction Up
        {
            get
            {
                return new Direction(-1, 0);
            }
        }
        public static Direction Down
        {
            get
            {
                return new Direction(1, 0);
            }
        }
        public static Direction Left
        {
            get
            {
                return new Direction(0, -1);
            }
        }
        public static Direction Right
        {
            get
            {
                return new Direction(0, 1);
            }
        }
    }
}
