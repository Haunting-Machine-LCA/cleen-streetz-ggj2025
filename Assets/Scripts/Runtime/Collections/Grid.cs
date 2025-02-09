using System.Collections;
using System.Collections.Generic;
using Codice.CM.Client.Differences.Merge;
using UnityEngine;


// Class that defines a grid
namespace Hmlca.CS
{
    public class Grid<TGridObject>
    {
        private int width, height, depth;
        private float cellSize;
        private TGridObject[,,] gridArray;
        private Vector3 originPosition;

        // Definition
        public Grid(int width, int height, int depth, float cellSize, Vector3 origin, 
        System.Func<Grid<TGridObject>, int, int, int, TGridObject> createGridObj)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.cellSize = cellSize;
            this.originPosition = origin;

            gridArray = new TGridObject[width, height, depth];

            for (int x=0; x<width; x++)
            {
                for (int y=0; y<height; y++)
                {
                    for (int z=0; z<depth; z++)
                    {
                        gridArray[x, y, z] = createGridObj(this, x, y, z);
                    }
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int y, int z)
        {
            return new Vector3(x, y, z) * cellSize + originPosition;
        }

        public void GetGridPosition(Vector3 worldPosition, out int x, out int y, out int z)
        {
            x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellSize);
            y = Mathf.FloorToInt((worldPosition.y - originPosition.y) / cellSize);
            z = Mathf.FloorToInt((worldPosition.z - originPosition.z) / cellSize);
        }


        public void GetGridPosition(Vector3 worldPosition, out Vector3Int pos)
        {
            GetGridPosition(worldPosition, out var x, out var y, out var z);
            pos = new Vector3Int(x, y, z);
        }


        public void SetValue(int x, int y, int z, TGridObject value)
        {
            if (IsValidGridPosition(x, y, z))
            {
                gridArray[x, y, z] = value;
            }
        }

        public TGridObject GetValue(int x, int y, int z)
        {
            if (IsValidGridPosition(x, y, z))
            {
                return gridArray[x, y, z];
            }
            return default;
        }

        public bool IsValidGridPosition(int x, int y, int z)
        {
            return x >= 0 && x < width && y >= 0 && y < height && z >= 0 && z < depth;
        }
    }
}
