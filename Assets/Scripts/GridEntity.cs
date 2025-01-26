using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hmlca.Untitled
{
    public abstract class GridEntity : MonoBehaviour
    {
        public UnityEvent<Vector3Int> OnGridPositionChanged = new UnityEvent<Vector3Int>();
        public int speed;
        public int xVelocity;
        public int yVelocity;
        [SerializeField] private Vector3Int gridPosition;


        protected virtual void Start()
        {
            BattleSystem.GetSingleton().RegisterGameObject(gameObject);
            SetGridPosition(gridPosition);
            int x = gridPosition.x;
            int y = gridPosition.y;
            int z = gridPosition.z;
            GridManager.GetSingleton()
                .Grid
                .GetValue(x, y, z)
                .isOccupied = true;
        }


        protected virtual void OnDisable()
        {
            var grid = GridManager.GetSingleton().Grid;
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
            var worldPosition = GridManager.GetSingleton().Grid.GetWorldPosition(x, y, z);
            transform.position = worldPosition;
        }


        protected virtual void OnDestroy()
        {
            BattleSystem.GetSingleton().UnregisterGameObject(gameObject);
        }
    }
}
