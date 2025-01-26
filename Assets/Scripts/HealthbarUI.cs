using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hmlca.Untitled
{
    public class HealthbarUI : MonoBehaviour
    {
        private Animator healthbarAnim;
        private int health = 5;
        public 

        // Start is called before the first frame update
        void Start()
        {
            healthbarAnim = GetComponent<Animator>();
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

        public void DecreaseHealthUI()
        {
            if (health > 0)
            {
                health -= 1;
                healthbarAnim.SetInteger("Health", health);
            }
        }

        public void IncreaseHealthUI()
        {
            if (health < 5)
            {
                health += 1;
                healthbarAnim.SetInteger("Health", health);
            }
        }
    }
}
