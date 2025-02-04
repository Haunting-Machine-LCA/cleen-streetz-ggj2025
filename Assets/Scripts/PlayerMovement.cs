using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    [RequireComponent(typeof(GridEntity))]
    [RequireComponent(typeof(Player), typeof(GridMover), typeof(CharacterAnimations))]
    public class PlayerMovement : Singleton<PlayerMovement>
    {
        public const KeyCode MOVE_UP = KeyCode.W;
        public const KeyCode MOVE_DOWN = KeyCode.S;
        public const KeyCode MOVE_LEFT = KeyCode.A;
        public const KeyCode MOVE_RIGHT = KeyCode.D;


        [SerializeField] private GridEntity entity;
        [SerializeField] private GridMover mover;
        [SerializeField] private PlayerAnimations animator;
        private Coroutine moveRoutine;


        protected override void Awake()
        {
            base.Awake();
            if (!entity)
                entity = GetComponent<GridEntity>();
            if (!mover)
                mover = GetComponent<GridMover>();
            if (!animator)
                animator = GetComponent<PlayerAnimations>();
        }


        private void Update()
        {
            Vector3Int dir = Vector3Int.zero;
            if (Input.GetKeyDown(MOVE_UP))
            {
                dir = new Vector3Int(0, 0, 1);
                animator.StartRunningAnim(2);
            }
            else if (Input.GetKeyDown(MOVE_DOWN))
            {
                dir = new Vector3Int(0, 0, -1);
                animator.StartRunningAnim(3);
            }
            else if (Input.GetKeyDown(MOVE_LEFT))
            {
                dir = new Vector3Int(-1, 0, 0);
                animator.StartRunningAnim(1);
            }
            else if (Input.GetKeyDown(MOVE_RIGHT))
            {
                dir = new Vector3Int(1, 0, 0);
                animator.StartRunningAnim(0);
            }
            if (dir != Vector3Int.zero)
            {
                if (mover.TryMove(dir))
                {
                    print($"Player moving {dir}");
                    if (moveRoutine != null)
                        StopCoroutine(moveRoutine);
                    StartCoroutine(
                        MoveTo(
                            mover.AnimateToNextTile(
                                entity.GridPosition + dir,
                                0.25f
                            )
                        )
                    );
                    
                }
            }
        }


        private IEnumerator MoveTo(IEnumerator routine)
        {
            moveRoutine = StartCoroutine(routine);
            var turnSystem = TurnSystem.GetSingleton();
            turnSystem.currentTurnRoutine = moveRoutine;
            yield return turnSystem.ExecuteTurn(TurnController.PLAYER);
            animator.StopRunningAnim();
        }
    }
}
