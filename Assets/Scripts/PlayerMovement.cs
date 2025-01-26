using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    [RequireComponent(typeof(Player), typeof(GridMover))]
    public class PlayerMovement : Singleton<PlayerMovement>
    {
        public const KeyCode MOVE_UP = KeyCode.W;
        public const KeyCode MOVE_DOWN = KeyCode.S;
        public const KeyCode MOVE_LEFT = KeyCode.A;
        public const KeyCode MOVE_RIGHT = KeyCode.D;


        [SerializeField] private GridMover mover;


        protected override void Awake()
        {
            base.Awake();
            if (mover)
                mover = GetComponent<GridMover>();
        }


        private void Update()
        {
            Vector3Int dir = Vector3Int.zero;
            if (Input.GetKeyDown(MOVE_UP))
                dir = new Vector3Int(0, 0, 1);
            else if (Input.GetKeyDown(MOVE_DOWN))
                dir = new Vector3Int(0, 0, -1);
            else if (Input.GetKeyDown(MOVE_LEFT))
                dir = new Vector3Int(-1, 0, 0);
            else if (Input.GetKeyDown(MOVE_RIGHT))
                dir = new Vector3Int(1, 0, 0);
            if (dir != Vector3Int.zero)
            {
                if (mover.TryMove(dir))
                {

                }
            }
        }
    }
}
