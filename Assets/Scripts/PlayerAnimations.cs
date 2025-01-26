using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            // TESTING
            if (Input.GetKeyDown(KeyCode.W))
            {
                StopRunningAnim();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                AttackAnim();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                DamagedAnim();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartRunningAnim(0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartRunningAnim(1);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartRunningAnim(2);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartRunningAnim(3);
            }
        }

        public void StartRunningAnim(int direction) // 0 = right; 1 = left; 2 = up; 3 = down
        {
            if (!anim.GetBool("isRunning"))
                anim.SetBool("isRunning", true);
            anim.SetInteger("runDirection", direction);
        }

        public void StopRunningAnim()
        {
            anim.SetBool("isRunning", false);
        }

        public void AttackAnim()
        {
            anim.SetTrigger("Attack");
        }

        public void DamagedAnim()
        {
            anim.SetTrigger("Damaged");
        }
    }
}
