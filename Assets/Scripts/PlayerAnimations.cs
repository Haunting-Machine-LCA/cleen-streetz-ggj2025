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
            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     StartRunningAnim();
            // }
            // if (Input.GetKeyDown(KeyCode.W))
            // {
            //     StopRunningAnim();
            // }
            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     AttackAnim();
            // }
            // if (Input.GetKeyDown(KeyCode.R))
            // {
            //     DamagedAnim();
            // }
        }

        public void StartRunningAnim()
        {
            anim.SetBool("isRunning", true);
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
