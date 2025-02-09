using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    public class GridNode
    {
        public int x, y, z;
        public bool isOccupied;

        public GridNode(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            isOccupied = false;
        }
    }
}
