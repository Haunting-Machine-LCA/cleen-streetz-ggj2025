using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hmlca.Untitled
{
    public abstract class GridEntity : MonoBehaviour
    {
        [Header("Callbacks")]
        public UnityEvent<Vector3Int> OnGridPositionChanged = new UnityEvent<Vector3Int>();
        public UnityEvent OnDestroyed = new UnityEvent();
        [Header("Movement")]
        public int speed;
        public int xVelocity;
        public int yVelocity;
        protected bool isQuitting;
        [Header("Transform")]
        [SerializeField] private int yOffset;
        [SerializeField] private Vector3Int gridPosition;
        private GridManager gm;


        protected virtual void Awake()
        {
            gm = GridManager.GetSingleton();
            Application.quitting += () => isQuitting = true;
        }


        protected virtual void Start()
        {
            if (!gm)
                gm = FindObjectOfType<GridManager>();
            BattleSystem.GetSingleton().RegisterGameObject(gameObject);
            SetGridPosition(gridPosition);
            int x = gridPosition.x;
            int y = gridPosition.y;
            int z = gridPosition.z;

            gm.Grid
                .GetValue(x, y, z)
                .isOccupied = true;
        }


        protected virtual void OnDisable()
        {
            var grid = gm.Grid;
            grid.GetGridPosition(transform.position, out int x, out int y, out int z);
            gridPosition = new Vector3Int(x, y, z);
            grid.GetValue(x, y, z)
                .isOccupied = false;
        }


        public virtual void SetGridPosition(Vector3Int gridPosition)
        {
            this.gridPosition = gridPosition;
            int x = gridPosition.x;
            int y = gridPosition.y;
            int z = gridPosition.z;
            var worldPosition = gm.Grid
                .GetWorldPosition(x, y, z);
            worldPosition.y += yOffset;
            transform.position = worldPosition;
        }


        protected virtual void OnDestroy()
        {
            OnDestroyed?.Invoke();
            if (!isQuitting)
                BattleSystem.GetSingleton().UnregisterGameObject(gameObject);
        }
    }
}
