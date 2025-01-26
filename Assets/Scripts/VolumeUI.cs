using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hmlca.Untitled
{
    public class VolumeUI : MonoBehaviour
    {
        private int volumeLevel = 0;
        public Sprite[] volumes;
        private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // // TESTING
            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     DecreaseVolumeUI();
            // }
            // if (Input.GetKeyDown(KeyCode.W))
            // {
            //     IncreaseVolumeUI();
            // }
        }

        public void SetVolumeUI(int val)
        {
            if (0 <= val && val < volumes.Length)
            {
                volumeLevel = val;
                anim.SetInteger("Volume", volumeLevel);
                SetImage(volumes[volumeLevel]);     
            }
        }

        public void IncreaseVolumeUI()
        {
            SetVolumeUI(volumeLevel + 1);     
        }
        public void DecreaseVolumeUI()
        {
            SetVolumeUI(volumeLevel - 1);     
        }

        private void SetImage(Sprite img)
        {
            GetComponent<Image>().sprite = img;
        }
    }
}
