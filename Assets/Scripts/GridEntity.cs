using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hmlca.Untitled
{
    public abstract class GridEntity : MonoBehaviour
    {
        public static Dictionary<Vector3Int, GameObject> gridObjectsByPos = new Dictionary<Vector3Int, GameObject>();
        public static Dictionary<GameObject, List<Vector3Int>> gridPosByObject = new Dictionary<GameObject, List<Vector3Int>>();
        protected static string reasonForFail; // DEBUG


        public List<Vector3Int> occupiedGridPositions = new List<Vector3Int>();
        [Header("Callbacks")]
        public UnityEvent<Vector3Int> OnGridPositionChanged = new UnityEvent<Vector3Int>();
        public UnityEvent OnDestroyed = new UnityEvent();
        [Header("Movement")]
        public int speed;
        public int xVelocity;
        public int yVelocity;
        protected bool forceSpawn;
        protected bool failedSpawn;
        protected bool isQuitting;
        public GridManager gm;
        [Header("Transform")]
        [SerializeField] private Vector3Int gridPosition;

    
        public Vector3Int GridPosition
        {
            get => gridPosition;
            set
            {
                if (gridPosition != value)
                {
                    gridPosition = value;
                    OnGridPositionChanged?.Invoke(gridPosition);
                }
            }
        }


        public static bool RegisterGridObject(GridEntity entity, GridManager gm, params Vector3Int[] gridPositions)
        {
            GameObject go = entity?.gameObject;
            if (gridPositions == null || gridPositions.Length == 0)
                gridPositions = new Vector3Int[] { entity.GridPosition };
            if (!go)
            {
                reasonForFail = $"gameobject does not exist";
                return false;
            }

            if (gridObjectsByPos.TryGetValue(gridPositions[0], out var existingGo) && existingGo != entity.gameObject)
            {
                reasonForFail = $"grid object @{gridPositions[0]} already exists: {existingGo.name}";
                return false; 
            }

            UnregisterGridObject(entity, gm);

            if (!gridPosByObject.TryGetValue(go, out var gridPosSet))
            {
                gridPosSet = new List<Vector3Int>();
                gridPosByObject.Add(go, gridPosSet);
            }
            foreach (var pos in gridPositions)
            {
                int x = pos.x;
                int y = pos.y;
                int z = pos.z;

                gm.Grid
                    .GetValue(x, y, z)
                    .isOccupied = true;
                gridPosSet.Add(pos);
                if (!gridObjectsByPos.TryAdd(pos, go) && gridObjectsByPos[pos] != entity.gameObject)
                {
                    reasonForFail = $"grid object @{pos} already exists: {gridObjectsByPos[pos]?.name}";
                    return false;
                }
            }
            return true;
        }


        public static void UnregisterGridObject(GridEntity entity, GridManager gm)
        {
            GameObject go = entity?.gameObject;
            if (!go)
                return;
            if (gridPosByObject.TryGetValue(go, out var gridPosSet))
            {
                foreach (var pos in gridPosSet)
                {
                    int x = pos.x;
                    int y = pos.y;
                    int z = pos.z;

                    gm.Grid
                        .GetValue(x, y, z)
                        .isOccupied = false;
                    gridObjectsByPos.Remove(pos);
                }
                gridPosByObject.Remove(go);
            }
        }


        public static void DestroyGridObjectAt(Vector3Int gridPosition)
        {
            if (gridObjectsByPos.TryGetValue(gridPosition, out var go))
            {
                gridPosByObject[go].Remove(gridPosition);
                gridObjectsByPos.Remove(gridPosition);
                Destroy(go);
            }
        }


        protected virtual void Awake()
        {
            gm = GridManager.GetSingleton(true);
            Application.quitting += () => isQuitting = true;
        }


        protected virtual void Start()
        {
            if (!gm)
                gm = FindObjectOfType<GridManager>();
            BattleSystem.GetSingleton().RegisterGameObject(gameObject);

            SetGridPosition(gridPosition);

            if (!RegisterGridObject(this, gm, GetGridPositions()))
            {
                failedSpawn = true;
                if (forceSpawn)
                    return;
                Debug.LogWarning($"Failed to register grid object {gameObject.name} @{GridPosition}: {reasonForFail}");
                Destroy(gameObject);
            }
        }


        protected virtual void OnDisable()
        {
            if (isQuitting || !gm)
                return;
            var grid = gm.Grid;
            grid.GetGridPosition(transform.position, out int x, out int y, out int z);
            gridPosition = new Vector3Int(x, y, z);
            grid.GetValue(x, y, z)
                .isOccupied = false;
        }


        public Vector3Int[] GetGridPositions()
        {
            Vector3Int[] positions = new Vector3Int[occupiedGridPositions.Count];
            for (int i = 0; i < occupiedGridPositions.Count; i++)
            {
                var pos = occupiedGridPositions[i];
                pos = pos + gridPosition;
                positions[i] = pos;
            }
            return positions;
        }


        public virtual void SetGridPosition(Vector3Int gridPosition)
        {
            this.gridPosition = gridPosition;
            RegisterGridObject(this, gm, GetGridPositions());
            int x = gridPosition.x;
            int y = gridPosition.y;
            int z = gridPosition.z;
            var worldPosition = gm.Grid
                .GetWorldPosition(x, y, z);
            transform.position = worldPosition;
        }


        protected virtual void OnDestroy()
        {
            if (!isQuitting)
            {
                OnDestroyed?.Invoke();
                BattleSystem.GetSingleton().UnregisterGameObject(gameObject);
                UnregisterGridObject(this, gm);
            }
        }
    }
}
