using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Hmlca.Untitled
{
    public class GridManager : Singleton<GridManager>
    {
        
        public int width = 5, depth = 5;
        private int height = 3;
        private float cellSize = 1f;
        public GameObject groundPrefab;
        public Material[] concreteMats;
        public int num1x1Blockers = 3;
        public GameObject[] blockerPrefabs_1x1;
        public int num2x1Blockers = 2;
        public GameObject[] blockerPrefabs_2x1;
        public float num1x2Blockers = 3;
        public GameObject[] blockerPrefabs_1x2;
        public float num1x3Blockers = 3;
        public GameObject[] blockerPrefabs_1x3;
        private Transform gridPiecesParent;
        private Transform groundParent;
        private Transform blockersParent;
        private Grid<GridNode> grid;


        public Grid<GridNode> Grid => grid;


        protected override void Awake()
        {
            base.Awake();
            grid = new Grid<GridNode>(width, height, depth, cellSize, Vector3.zero, (g, x, y, z) => new GridNode(x, y, z));
        }


        // Start is called before the first frame update
        void Start()
        {
            gridPiecesParent = transform.Find("GridPieces");
            groundParent = gridPiecesParent.Find("Ground");
            blockersParent = gridPiecesParent.Find("Blockers");
            
            PopulateGrid();
            // DrawGrid();
        }

    
        void PopulateGrid()
        {
            // Make ground
            for (int x=0; x<width; x++)
            {
                for (int z=0; z<depth; z++)
                {
                    Vector3 worldPos = grid.GetWorldPosition(x, 0, z);
                    Material randomConcreteMat = concreteMats[Random.Range(0, concreteMats.Length)]; // Choose a random concrete texture
                    groundPrefab.transform.Find("Cube").GetComponent<Renderer>().material = randomConcreteMat;
                    var thisObj = PlaceObject(worldPos, groundPrefab);
                    thisObj.transform.parent = groundParent;
                }
            }
            
            // Make blockers
            // 1x1 blockers
            for (int i=0; i<num1x1Blockers; i++)
            {
                // Choose random object
                GameObject obj = blockerPrefabs_1x1[Random.Range(0, blockerPrefabs_1x1.Length)];
                // Choose random position on ground
                Vector3 pos = new Vector3(Random.Range(0, width), 1, Random.Range(0, depth));
                // Place
                var thisObj = PlaceObject(pos, obj);
                if (thisObj != null)
                {
                    thisObj.transform.parent = blockersParent;
                }
                
            }

            // 2x1 blockers
            for (int i=0; i<num2x1Blockers; i++)
            {
                // Choose random object
                GameObject obj = blockerPrefabs_2x1[Random.Range(0, blockerPrefabs_2x1.Length)];
                
                // Choose random rotation
                float[] directions = new float[]{0f, 90f, 180f, 270f};
                float rotation = directions[Random.Range(0, directions.Length)];

                // Place
                // Find appropriate position
                bool posFound = false;
                Vector3 pos1 = Vector3.zero;
                Vector3 pos2 = Vector3.zero;
                while (!posFound)
                {
                    // Choose random position on ground
                    pos1 = new Vector3(Random.Range(0, width), 1, Random.Range(0, depth));

                    // Get the secondary position based on rotation
                    switch (rotation)
                    {
                        case 90f:
                            // Secondary block is below
                            pos2 = pos1 + new Vector3(0, 0, -cellSize);
                            break;
                        case 180f:
                            // Secondary block is to the left
                            pos2 = pos1 + new Vector3(-cellSize, 0, 0);
                            break;
                        case 270f:
                            // Secondary block is above
                            pos2 = pos1 + new Vector3(0, 0, cellSize);
                            break;
                        default:
                            // Secondary block is to the right
                            pos2 = pos1 + new Vector3(cellSize, 0, 0);
                            break;
                    }

                    // Check if they are valid
                    int x1, y1, z1;
                    grid.GetGridPosition(pos1, out x1, out y1, out z1);
                    int x2, y2, z2;
                    grid.GetGridPosition(pos2, out x2, out y2, out z2);
                    if (grid.IsValidGridPosition(x1, y1, z1) && grid.IsValidGridPosition(x2, y2, z2) &&
                    !grid.GetValue(x1, y1, z1).isOccupied && !grid.GetValue(x2, y2, z2).isOccupied)
                    {
                        posFound = true;
                    }
                }
                // Debug.Log(pos1 + " " + pos2);
                var thisObj = PlaceObject(pos1, obj);
                if (thisObj != null)
                {
                    thisObj.transform.parent = blockersParent;
                    thisObj.transform.Rotate(0, rotation, 0);
                    // Mark its secondary block occupied too
                    PlaceObject(pos2, new GameObject());
                }
            }

            // 1x2 blockers
            for (int i=0; i<num1x2Blockers; i++)
            {
                // Choose random object
                GameObject obj = blockerPrefabs_1x2[Random.Range(0, blockerPrefabs_1x2.Length)];
                // Choose random position on ground
                Vector3 pos = new Vector3(Random.Range(0, width), 1, Random.Range(0, depth));
                // Place
                var thisObj = PlaceObject(pos, obj);
                if (thisObj != null)
                {
                    thisObj.transform.parent = blockersParent;
                }
            }

            // 1x3 blockers
            for (int i=0; i<num1x3Blockers; i++)
            {
                // Choose random object
                GameObject obj = blockerPrefabs_1x3[Random.Range(0, blockerPrefabs_1x3.Length)];
                // Choose random position on ground
                Vector3 pos = new Vector3(Random.Range(0, width), 1, Random.Range(0, depth));
                // Place
                var thisObj = PlaceObject(pos, obj);
                if (thisObj != null)
                {
                    thisObj.transform.parent = blockersParent;
                }
            }
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
                        Color color = Color.white;
                        if (grid.GetValue(x, y, z).isOccupied)
                        {
                            color = Color.green;
                        }
                        Debug.DrawLine(worldPos, worldPos + Vector3.right * 0.1f, color, float.PositiveInfinity);
                        Debug.DrawLine(worldPos, worldPos + Vector3.up * 0.1f, color, float.PositiveInfinity);
                        Debug.DrawLine(worldPos, worldPos + Vector3.forward * 0.1f, color, float.PositiveInfinity);

                        // PlaceObject(worldPos, gridPrefab);
                    }
                }
            }
        }

        public GameObject PlaceObject(Vector3 worldPosition, GameObject obj)
        {
            int x, y, z;
            grid.GetGridPosition(worldPosition, out x, out y, out z);

            GridNode node = grid.GetValue(x, y, z);
            if (node != null && !node.isOccupied)
            {
                node.isOccupied = true;
                GameObject thisObj = Instantiate(obj, grid.GetWorldPosition(x, y, z), Quaternion.identity);
                return thisObj;
            }
            else
            {
                Debug.Log("Cell is already occupied!");
                return null;
            }
        }
    }
}
