using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    public class BubbleHeart : MonoBehaviour
    {
        private Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // TESTING
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PopHeart();
            }
        }

        void PopHeart()
        {
            anim.SetTrigger("Pop");
        }
    }
}
