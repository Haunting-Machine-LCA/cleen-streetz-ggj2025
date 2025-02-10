using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hmlca.CS.App;
using Hmlca.CS.World.Battles;
using Hmlca.CS.Inputs;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;


namespace Hmlca.CS.World.Players
{
    [RequireComponent(typeof(GridEntity), typeof(Facing))]
    [RequireComponent(typeof(Player), typeof(GridMover), typeof(CharacterAnimations))]
    public class PlayerMovement : PlayerInput<PlayerMovement>
    {
        public const KeyCode MOVE_UP = KeyCode.W;
        public const KeyCode MOVE_DOWN = KeyCode.S;
        public const KeyCode MOVE_LEFT = KeyCode.A;
        public const KeyCode MOVE_RIGHT = KeyCode.D;


        [SerializeField] private GridEntity entity;
        [SerializeField] private GridMover mover;
        [SerializeField] private Facing facing;
        [SerializeField] private CharacterAnimations animator;
        [SerializeField] private Vector2 desiredMovement;
        private Transform cameraRootTransform;
        private Coroutine moveRoutine;
        private bool desiresToMove;
        private float currentMovementAngle;


        protected override void Awake()
        {
            base.Awake();
            if (!entity)
                entity = GetComponent<GridEntity>();
            if (!mover)
                mover = GetComponent<GridMover>();
            if (!animator)
                animator = GetComponent<CharacterAnimations>();
            if (!facing)
                facing = GetComponent<Facing>();
        }


        private void Start()
        {
            if (!cameraRootTransform)
                cameraRootTransform = Camera.main.transform;
        }


        private void Update()
        {
            if (moveRoutine != null)
                return;

            // Get move input vector
            Vector3Int dir = Vector3Int.zero;
            if (Input.GetKeyDown(MOVE_UP))
                dir = new Vector3Int(0, 0, 1);
            else if (Input.GetKeyDown(MOVE_DOWN))
                dir = new Vector3Int(0, 0, -1);
            else if (Input.GetKeyDown(MOVE_LEFT))
                dir = new Vector3Int(-1, 0, 0);
            else if (Input.GetKeyDown(MOVE_RIGHT))
                dir = new Vector3Int(1, 0, 0);
            var newDesiredMovement = new Vector2(dir.x, dir.z);

            // If move input accepted
            if (newDesiredMovement != desiredMovement)
            {
                desiredMovement = newDesiredMovement;
                desiresToMove = true;
            }
            
            UpdateRotation(currentMovementAngle);
        }


        private void LateUpdate()
        {
            if (desiresToMove && desiredMovement != Vector2.zero)
            {
                var cdir = cameraRootTransform.forward * desiredMovement.y + cameraRootTransform.right * desiredMovement.x;
                cdir.Normalize();
                var dir2 = new Vector2(cdir.x, cdir.z);
                var dir = new Vector3Int(Mathf.RoundToInt(dir2.x) % 360, 0, Mathf.RoundToInt(dir2.y) % 360);
                if (mover.TryMove(dir))
                {
                    // Update absolute movement angle and start running animation
                    dir2 = new Vector2(dir.x, dir.z);
                    var angle = dir2.GetAngle();
                    print($"Player facing set to {angle}"); //DEBUG
                    facing.SetFacing(angle, true);
                    animator.StartRunningAnim();

                    print($"Player moving {dir}"); //DEBUG

                    // Perform movement coroutine
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
                desiresToMove = false;
            }
        }


        private void UpdateRotation(float angle)
        {
            var eulerAngles = entity.transform.eulerAngles;
            eulerAngles.y = (cameraRootTransform.eulerAngles.y - 180f) % 360;
            entity.transform.eulerAngles = eulerAngles;
        }


        private IEnumerator MoveTo(IEnumerator routine)
        {
            moveRoutine = StartCoroutine(routine);
            var turnSystem = TurnSystem.GetSingleton();
            turnSystem.currentTurnRoutine = moveRoutine;
            yield return turnSystem.ExecuteTurn(TurnController.PLAYER);
            yield return moveRoutine;
            desiredMovement = Vector2.zero;
            animator.StopRunningAnim();
            moveRoutine = null;
        }
    }
}
