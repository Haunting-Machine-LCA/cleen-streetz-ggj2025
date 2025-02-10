using Hmlca.CS.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    [RequireComponent(typeof(Animator), typeof(Facing))]
    public class CharacterAnimations : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Facing facing;


        // Start is called before the first frame update
        void Awake()
        {
            if (!animator)
                animator = GetComponent<Animator>();
            if (!facing)
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
            animator.SetInteger("runDirection", facing.CameraFacing());
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
