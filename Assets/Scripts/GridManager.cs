using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    public class GridManager : MonoBehaviour
    {
        private Grid<GridNode> grid;
        public int width = 5, height = 3, depth = 5;
        public float cellSize = 1f;

        // Start is called before the first frame update
        void Start()
        {
            grid = new Grid<GridNode>(width, height, depth, cellSize, Vector3.zero, (g, x, y, z) => new GridNode(x, y, z));
            DrawGrid();
        }

        void DrawGrid()
        {
            for (int x=0; x<width; x++)
            {
                for (int y=0; y<height; y++)
                {
                    for(int z=0; z<depth; z++)
                    {
                        Vector3 worldPos = grid.GetWorldPosition(x, y, z);
                        Debug.DrawLine(worldPos, worldPos + Vector3.right * 0.1f, Color.white, 100f);
                        Debug.DrawLine(worldPos, worldPos + Vector3.up * 0.1f, Color.white, 100f);
                        Debug.DrawLine(worldPos, worldPos + Vector3.forward * 0.1f, Color.white, 100f);
                    }
                }
            }
        }

        public void PlaceObject(Vector3 worldPosition, GameObject obj)
        {
            int x, y, z;
            grid.GetGridPosition(worldPosition, out x, out y, out z);

            GridNode node = grid.GetValue(x, y, z);
            if (node != null && !node.isOccupied)
            {
                node.isOccupied = true;
                Instantiate(obj, grid.GetWorldPosition(x, y, z), Quaternion.identity);
            }
        }
    }
}
