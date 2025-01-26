using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hmlca.Untitled
{
    public class HealthbarUI : MonoBehaviour
    {
        private Animator anim;
        private int health = 5;
        public 

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            // // TESTING
            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     DecreaseHealthUI();
            // }
            // if (Input.GetKeyDown(KeyCode.W))
            // {
            //     IncreaseHealthUI();
            // }
        }

        public void SetHealthUI(int val)
        {
            if (0 <= val && val <= 5)
            {
                health = val;
                anim.SetInteger("Health", health);
            }
        }

        public void DecreaseHealthUI()
        {
            SetHealthUI(health - 1);
        }

        public void IncreaseHealthUI()
        {
            SetHealthUI(health + 1);
        }
    }
}
