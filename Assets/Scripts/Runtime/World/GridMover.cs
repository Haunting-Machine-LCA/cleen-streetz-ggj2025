using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    [RequireComponent(typeof(GridEntity))]
    public class GridMover : MonoBehaviour
    {
        public Vector2Int facingDirection;
        [SerializeField] private GridEntity entity;


        private void Awake()
        {
            if (!entity)
                entity = GetComponent<GridEntity>();
        }


        public bool TryMove(Vector3Int direction)
        {
            facingDirection = new Vector2Int(direction.x, direction.z);
            Vector3Int endPos = entity.GridPosition + direction;
            return (!entity.gm.Grid.GetValue(endPos.x, endPos.y, endPos.z)?.isOccupied) ?? false;
        }


        public IEnumerator AnimateToNextTile(Vector3Int newGridPos, float secs)
        {
            Vector3Int startGridPos = entity.GridPosition;
            float time = 0;
            Vector3 startPos = entity.transform.position;
            Vector3 endPos = entity.gm.Grid.GetWorldPosition(newGridPos.x, newGridPos.y, newGridPos.z);
            while (time < secs)
            {
                entity.transform.position = Vector3.Lerp(startPos, endPos, time / secs);
                time += Time.deltaTime;
                yield return null;
            }
            entity.transform.position = endPos;
            entity.GridPosition = newGridPos;
        }
    }
}
