using Hmlca.CS.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    [RequireComponent(typeof(Animator), typeof(Facing))]
    public class CharacterAnimations : MonoBehaviour
    {
        [SerializeField] private bool useFacing;
        [SerializeField] private Animator animator;
        [SerializeField] private Facing facing;


        // Start is called before the first frame update
        void Awake()
        {
            if (!animator)
                animator = GetComponent<Animator>();
            if (useFacing && !facing)
                facing = GetComponent<Facing>();
        }


        //private void Start()
        //{
        //}


        //void Update()
        //{
        //    //TESTING
        //     if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        StartRunningAnim();
        //    }
        //    if (Input.GetKeyDown(KeyCode.W))
        //    {
        //        StopRunningAnim();
        //    }
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        AttackAnim();
        //    }
        //    if (Input.GetKeyDown(KeyCode.R))
        //    {
        //        DamagedAnim();
        //    }
        //}

        private void LateUpdate()
        {
            if (!useFacing)
                return;
            var relFacing = facing.GetCameraRelativeFacing();
            float relFacingFloat = relFacing * 45f;
            var dir = relFacingFloat.ToDirection();
            var runDirection = Mathf.RoundToInt((int) dir / 2f) % 4;
            animator.SetInteger("runDirection", runDirection);
        }


        public void StartRunningAnim()
        {
            animator.SetBool("isRunning", true);
        }

        public void StopRunningAnim()
        {
            animator.SetBool("isRunning", false);
        }

        public void AttackAnim()
        {
            animator.SetTrigger("Attack");
        }

        public void DamagedAnim()
        {
            animator.SetTrigger("Damaged");
        }
    }
}
