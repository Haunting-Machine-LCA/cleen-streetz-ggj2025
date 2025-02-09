using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;
using Hmlca.CS.App;
using Hmlca.CS.Collections;


namespace Hmlca.CS.World
{
    public class GridManager : Singleton<GridManager>
    {
        
        public int width = 5, depth = 5;
        private int height = 4;
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
        public GameObject targetGroupObj;
        private Transform gridPiecesParent;
        private Transform groundParent;
        private Transform blockersParent;
        private Grid<GridNode> grid;
        private CinemachineTargetGroup targetGroup;

        // public void SetTargets()
        // {
        //     targetGroupObj.GetComponent<GridTargetGroup>().SetTargets(blockerGameObjects);
        // }



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
            targetGroup = FindObjectOfType<CinemachineTargetGroup>();
        }


        // Start is called before the first frame update
        void Start()
        {   
            PopulateGrid();
            DrawGrid();

            // SetTargets();
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
                    targetGroup.AddMember(thisObj.transform, 1, 1);
                    thisObj.transform.parent = groundParent;
                }
            }
            
            // Make blockers
            // 1x1 blockers
            MakeBlockers(blockerPrefabs_1x1, 1, 1, num1x1Blockers);

            // 2x1 blockers
            MakeBlockers(blockerPrefab_2x1, 2, 1, num2x1Blockers);

            // 1x2 blockers
            MakeBlockers(blockerPrefab_1x2, buildingMats, 1, 2, num1x2Blockers);

            // 1x3 blockers
            MakeBlockers(blockerPrefab_1x3, buildingMats, 1, 3, num1x1Blockers);
        }
        

        private void MakeBlockers(GameObject[] blockerPrefabs, Material[] variantMats, int countWidth, int countHeight, int amount)
        {
            for (int i=0; i<amount; i++)
            {
                // Choose random object
                GameObject obj = blockerPrefabs[Random.Range(0, blockerPrefabs.Length)];

                // Choose random position on ground
                if (!ValidateGridSegments(countWidth, out Vector3Int[] positions, out Vector3 direction)) return;

                // Place real position in grid
                Grid.GetGridPosition(positions[0], out Vector3Int gridPos);
                // Debug.Log($"{obj.name} will be at {gridPos} facing {direction}");
                var thisObj = PlaceObject(obj, gridPos);
                //print($"var thisObj {thisObj == null}");
                if (thisObj == null) 
                {
                    // print("placeobject is null");
                    return;
                }
                else
                {
                    // print("we got it on lock");
                    // Apply randomized materials if applicable
                    if (variantMats.Length > 0)
                    {
                        Material mat = variantMats[Random.Range(0, variantMats.Length)];
                        thisObj.transform.Find("Main").GetComponent<Renderer>().material = mat;
                    }
                    thisObj.transform.parent = blockersParent;

                    // Place ghost placeholder objects
                    // Debug.Log("time for ghost placeholders");
                    if (countWidth > 1)
                    {
                        // Debug.Log("width");
                        for (int j=1; j<positions.Length; j++)
                        {
                            Vector3Int pos = positions[j];
                            var placeholderObj = PlaceObject(new GameObject(), pos);
                            if (placeholderObj != null) placeholderObj.transform.parent = thisObj.transform;
                        }
                    }
                    if (countHeight > 1)
                    {
                        // Debug.Log("height");
                        for (int j=0; j<positions.Length; j++)
                        {
                            for (int k=2; k<=countHeight; k++)
                            {
                                Vector3Int pos = new Vector3Int(positions[j].x, k, positions[j].z);
                                var placeholderObj = PlaceObject(new GameObject(), pos);
                                if (placeholderObj != null) placeholderObj.transform.parent = thisObj.transform;
                            }
                        }
                    }

                    // Turn mesh in direction
                    if (direction == Vector3.up) thisObj.transform.Rotate(Vector3.up, -90);
                    else if (direction == Vector3.down) thisObj.transform.Rotate(Vector3.up, 90);
                    else if (direction == Vector3.left) thisObj.transform.Rotate(Vector3.up, 180);
                    // Else don't turn
                
                    thisObj.transform.parent = blockersParent.transform;
                }
            }
        }
        // Overloads
        private void MakeBlockers(GameObject blockerPrefab, Material[] variantMats, int countWidth, int countHeight, int amount)
        {
            MakeBlockers(new GameObject[] {blockerPrefab}, variantMats, countWidth, countHeight, amount);
        }
        private void MakeBlockers(GameObject blockerPrefab, int countWidth, int countHeight, int amount)
        {
            MakeBlockers(new GameObject[] {blockerPrefab}, new Material[]{}, countWidth, countHeight, amount);
        }
        private void MakeBlockers(GameObject[] blockerPrefabs, int countWidth, int countHeight, int amount)
        {
            MakeBlockers(blockerPrefabs, new Material[]{}, countWidth, countHeight, amount);
        }

        // Checks if a line of blocks will fit
        private bool ValidateGridSegments(int countWidth, out Vector3Int[] positions, out Vector3 direction)
        {
            positions = new Vector3Int[countWidth];
            Vector3[] directions = new Vector3[]{Vector3.right, Vector3.left, Vector3.up, Vector3.down};
            direction = Vector3.zero;
            int MAX_ATTEMPTS = width * height * depth;
            int currAttempts = 0;
            while (true)
            {   
                // Randomly choose an initial grid position
                int x = Random.Range(1, width-1);
                int y = 1;
                int z = Random.Range(1, depth-1);

                // Check which orientation it should be in
                Vector3Int[] temp = new Vector3Int[countWidth];
                if (countWidth > 1)
                {
                    foreach (Vector3 gridDir in directions)
                    {
                        direction = gridDir;
                        for (int i=0; i<countWidth; i++)
                        {
                            Vector3Int gridPos = Vector3Int.FloorToInt(new Vector3(x, y, z) + (gridDir * i));
                            if (IsPosOccupied(gridPos.x, gridPos.y, gridPos.z)) continue; // Abort this direction if you run into an occupied position
                            else temp[i] = gridPos;
                        }
                        // All segments fit here
                        positions = temp;
                        print("epic. returning");
                        return true;
                    }
                }
                else
                {
                    Vector3Int gridPos = Vector3Int.FloorToInt(new Vector3(x, y, z));
                    positions[0] = gridPos;
                    if (IsPosOccupied(gridPos.x, gridPos.y, gridPos.z)) continue;
                    else direction = directions[Random.Range(0, directions.Length)]; return true;
                }
                    
                currAttempts++;
                // Protect against infinite loops
                if (currAttempts >= MAX_ATTEMPTS) print("im giving up i guess"); return false;
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

                // print($"node {node} is occupied?: {node.isOccupied}");
                if (!IsPosOccupied(x, y, z))
                {
                    GridNode node = grid.GetValue(x, y, z);
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

        private bool IsPosOccupied(int x, int y, int z)
        {
            GridNode node = grid.GetValue(x, y, z);
            return node.isOccupied;
        }
    }
}
