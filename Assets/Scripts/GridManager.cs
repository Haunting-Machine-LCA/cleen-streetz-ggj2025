using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Hmlca.Untitled
{
    public class GridManager : MonoBehaviour
    {
        private Grid<GridNode> grid;
        public int width = 5, height = 3, depth = 5;
        public float cellSize = 1f;
        public GameObject groundPrefab;
        public Material[] concreteMats;
        public int num1x1Blockers = 3;
        public GameObject[] blockerPrefabs_1x1;
        public int num2x1Blockers = 2;
        public GameObject[] blockerPrefabs_2x1;
        private Transform gridPiecesParent;
        private Transform groundParent;
        private Transform blockersParent;


        // Start is called before the first frame update
        void Start()
        {
            grid = new Grid<GridNode>(width, height, depth, cellSize, Vector3.zero, (g, x, y, z) => new GridNode(x, y, z));
            gridPiecesParent = transform.Find("GridPieces");
            groundParent = gridPiecesParent.Find("Ground");
            blockersParent = gridPiecesParent.Find("Blockers");
            // DrawGrid();
            PopulateGrid();
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
                thisObj.transform.parent = blockersParent;
            }

            // 2x1 blockers
            // for (int i=0; i<num2x1Blockers; i++)
            // {
            //     // Choose random object
            //     GameObject obj = blockerPrefabs_2x1[Random.Range(0, blockerPrefabs_2x1.Length)];
                
            //     // Choose random rotation
            //     float[] directions = new float[]{0f, 90f, 180f, 270f};
            //     float rotation = directions[Random.Range(0, directions.Length)];

            //     // Place
            //     // Find appropriate position
            //     bool posFound = false;
            //     Vector3 pos1 = new Vector3(Random.Range(0, width), 1, Random.Range(0, depth));
            //     while (!posFound)
            //     {
            //         // Choose random position on ground
            //         pos1 = new Vector3(Random.Range(0, width), 1, Random.Range(0, depth));

            //         // Get the secondary position based on rotation
            //         Vector3 pos2 =

            //         // Check if they are valid
            //         if ()
            //         {
            //             posFound = true;
            //         }
            //     }
            //     var thisObj = PlaceObject(pos1, obj);
            //     thisObj.transform.parent = blockersParent;
            //     thisObj.transform.Rotate(0, rotation, 0);
            //     // Mark its secondary block occupied too
                
                
            // }

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
