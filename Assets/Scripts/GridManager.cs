using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hmlca.Untitled
{
    public class GridManager : Singleton<GridManager>
    {
        
        public int width = 5, depth = 5;
        private int height = 3;
        private float cellSize = 1f;
        public GameObject groundPrefab;
        public Material sidewalkMat;
        public Material[] concreteMats;
        public Material[] buildingMats;
        public int num1x1Blockers = 3;
        public GameObject[] blockerPrefabs_1x1;
        public int num2x1Blockers = 2;
        public GameObject blockerPrefab_2x1;
        public int num1x2Blockers = 3;
        public GameObject blockerPrefab_1x2;
        public int num1x3Blockers = 3;
        public GameObject blockerPrefab_1x3;
        private Transform gridPiecesParent;
        private Transform groundParent;
        private Transform blockersParent;
        private Grid<GridNode> grid;
        private List<GameObject> blockerGameObjects = new List<GameObject>();


        public Grid<GridNode> Grid => grid;

    
        public bool this[Vector2Int pos]
        {
            get
            {
                int x = pos.x;
                int y = pos.y;
                if (x >= 0 && y >= 0 && x < width && y < depth)
                    return grid.GetValue(x, 0, y).isOccupied;
                return true;
            }
            set
            {
                int x = pos.x;
                int y = pos.y;
                if (x >= 0 && y >= 0 && x < width && y < depth)
                {
                    var val = grid.GetValue(x, 0, y);
                    if (val.isOccupied)
                    {

                    }
                }
            }
        }


        protected override void Awake()
        {
            base.Awake();
            grid = new Grid<GridNode>(width, height, depth, cellSize, Vector3.zero, (g, x, y, z) => new GridNode(x, y, z));
            gridPiecesParent = transform.Find("GridPieces");
            groundParent = gridPiecesParent.Find("Ground");
            blockersParent = gridPiecesParent.Find("Blockers");
        }


        // Start is called before the first frame update
        void Start()
        {   
            PopulateGrid();
            DrawGrid();
        }

    
        void PopulateGrid()
        {
            // Make ground
            for (int x=0; x<width; x++)
            {
                for (int z=0; z<depth; z++)
                {
                    Vector3 worldPos = grid.GetWorldPosition(x, 0, z);

                    if (x == 0 || x == width - 1 || z == 0 || z == depth - 1)
                    {
                        groundPrefab.transform.Find("Main").GetComponent<Renderer>().material = sidewalkMat; // Edge blocks are sidewalks
                    }
                    else
                    {
                        Material randomConcreteMat = concreteMats[Random.Range(0, concreteMats.Length)]; // Choose a random concrete texture
                        groundPrefab.transform.Find("Main").GetComponent<Renderer>().material = randomConcreteMat;
                    }
                    
                    var thisObj = PlaceObject(groundPrefab, Vector3Int.FloorToInt(worldPos));
                    thisObj.transform.parent = groundParent;
                }
            }
            
            // Make blockers
            // 1x1 blockers
            Debug.Log(blockerPrefabs_1x1.Length + " " + num1x1Blockers);
            MakeRandObjBlockers(blockerPrefabs_1x1, num1x1Blockers);
            // for (int i=0; i<num1x1Blockers; i++)
            // {
            //     // Choose random object
            //     GameObject obj = blockerPrefabs_1x1[Random.Range(0, blockerPrefabs_1x1.Length)];
            //     // Choose random position on ground
            //     Vector3 pos = new Vector3(Random.Range(1, width-1), 1, Random.Range(1, depth-1));
            //     Grid.GetGridPosition(pos, out var x, out var y, out var z);
            //     var gridPos = new Vector3Int(x, 1, z);
            //     // Place
            //     var thisObj = PlaceObject(obj, gridPos);
            //     if (thisObj != null)
            //     {
            //         thisObj.transform.parent = blockersParent;
            //     }
                
            // }

            // 2x1 blockers
            for (int i=0; i<num2x1Blockers; i++)
            {
                // Choose random object
                GameObject obj = blockerPrefab_2x1;
                
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
                    pos1 = new Vector3(Random.Range(1, width-1), 1, Random.Range(1, depth-1));

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
                // Grid.GetGridPosition(pos1, out Vector3Int gridPos1);
                // Grid.GetGridPosition(pos2, out Vector3Int gridPos2);
                var thisObj = PlaceObject(obj, Vector3Int.FloorToInt(pos1));
                if (thisObj != null)
                {
                    thisObj.transform.parent = blockersParent;
                    thisObj.transform.Rotate(0, rotation, 0);
                    var secondHalf = PlaceObject(obj, Vector3Int.FloorToInt(pos2));
                    secondHalf.transform.parent = blockersParent;
                }
            }

            // 1x2 blockers
            MakeRandMatBlockers(blockerPrefab_1x2, buildingMats, num1x2Blockers);
            // for (int i=0; i<num1x2Blockers; i++)
            // {
            //     // Choose random object
            //     GameObject obj = blockerPrefabs_1x2[Random.Range(0, blockerPrefabs_1x2.Length)];
            //     // Choose random position on ground
            //     Vector3 pos = new Vector3(Random.Range(1, width-1), 1, Random.Range(1, depth-1));
            //     // Place
            //     Grid.GetGridPosition(pos, out Vector3Int gridPos);
            //     var thisObj = PlaceObject(obj, gridPos);
            //     if (thisObj != null)
            //     {
            //         thisObj.transform.parent = blockersParent;
            //         Material randomBuildingMat = buildingMats[Random.Range(0, buildingMats.Length)]; // Choose a random building texture
            //         thisObj.transform.Find("Cube").GetComponent<Renderer>().material = randomBuildingMat;
            //     }
                
            // }

            // 1x3 blockers
            MakeRandMatBlockers(blockerPrefab_1x3, buildingMats, num1x1Blockers);
            // for (int i=0; i<num1x3Blockers; i++)
            // {
            //     // Choose random object
            //     GameObject obj = blockerPrefabs_1x3[Random.Range(0, blockerPrefabs_1x3.Length)];
            //     // Choose random position on ground
            //     Vector3 pos = new Vector3(Random.Range(1, width-1), 1, Random.Range(1, depth-1));
            //     // Place
            //     Grid.GetGridPosition(pos, out Vector3Int gridPos);
            //     var thisObj = PlaceObject(obj, gridPos);
            //     if (thisObj != null)
            //     {
            //         thisObj.transform.parent = blockersParent;
            //         Material randomBuildingMat = buildingMats[Random.Range(0, buildingMats.Length)]; // Choose a random building texture
            //         thisObj.transform.Find("Cube").GetComponent<Renderer>().material = randomBuildingMat;
            //     }
                
            // }
        }

        private void MakeRandMatBlockers(GameObject blockerPrefab, Material[] randomizedMats, int amount)
        {
            for (int i=0; i<amount; i++)
            {
                Debug.Log(i);
                GameObject obj = blockerPrefab;

                // Choose random position on ground
                Vector3 pos = FindPositionOnGround();
                Debug.Log(pos);
                if (pos == Vector3.zero) return; // No more room for blocks

                // Place in grid
                Grid.GetGridPosition(pos, out Vector3Int gridPos);
                var thisObj = PlaceObject(obj, gridPos);
                if (thisObj != null)
                {
                    // Apply randomized materials
                    if (randomizedMats.Length > 0)
                    {
                        Material mat = randomizedMats[Random.Range(0, randomizedMats.Length)];
                        thisObj.transform.Find("Main").GetComponent<Renderer>().material = mat;
                    }
                    blockerGameObjects.Add(thisObj); // Keep track of them
                    thisObj.transform.parent = blockersParent;
                }
            }
        }

        private void MakeRandObjBlockers(GameObject[] blockerPrefabs, int amount)
        {
            for (int i=0; i<amount; i++)
            {
                // Choose random object
                GameObject obj = blockerPrefabs[Random.Range(0, blockerPrefabs.Length)];

                // Choose random position on ground
                Vector3 pos = FindPositionOnGround();
                if (pos == Vector3.zero) return; // No more room for blocks

                // Place in grid
                Grid.GetGridPosition(pos, out Vector3Int gridPos);
                var thisObj = PlaceObject(obj, gridPos);
                if (thisObj != null)
                {
                    blockerGameObjects.Add(thisObj); // Keep track of them
                    thisObj.transform.parent = blockersParent;
                }
            }
        }

        private Vector3 FindPositionOnGround()
        {
            int MAX_ATTEMPTS = width * height * depth;
            int currAttempts = 0;
            while(true)
            {
                currAttempts++;
                int x = Random.Range(1, width-1);
                int y = 1;
                int z = Random.Range(1, depth-1);
                if (!grid.GetValue(x, y, z).isOccupied) return new Vector3(x, y, z);

                // Protect against infinite loops
                if (currAttempts >= MAX_ATTEMPTS) return Vector3.zero;
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

        public GameObject PlaceObject(GameObject obj = null, params Vector3Int[] gridPos)
        {
            bool setPos = false;
            foreach (var pos in gridPos)
            {
                int x, y, z;
                x = pos.x;
                y = pos.y;
                z = pos.z;
                var worldPosition = grid.GetWorldPosition(x, y, z);
                Debug.Log(obj.name + " " + worldPosition);

                GridNode node = grid.GetValue(x, y, z);
                if (node == null)
                    print("node null?");
                if (node != null && !node.isOccupied)
                {
                    node.isOccupied = true;
                    GameObject thisObj = Instantiate(obj, worldPosition, Quaternion.identity);


                    if (thisObj.TryGetComponent<GridEntity>(out var entity))
                    {
                        Vector3Int offset = pos - gridPos[0];
                        if (!setPos)
                        {
                            entity.GridPosition = pos;
                            setPos = true;
                        }
                        entity.occupiedGridPositions.Add(offset);
                    }

                    return thisObj;
                }
                else
                {
                    Debug.Log("Cell is already occupied!");
                    return null;
                }
            }
            return null;
        }

        public Vector3 GetCenterCoords()
        {
            float x = width * cellSize / 2;
            float y = cellSize; // Center will be on ground
            float z = depth * cellSize / 2;
            return new Vector3(x, y, z);
        }
    }
}
