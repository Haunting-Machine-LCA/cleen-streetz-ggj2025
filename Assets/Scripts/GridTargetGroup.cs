using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Hmlca.Untitled
{
    public class GridTargetGroup : MonoBehaviour
    {
        private CinemachineTargetGroup group;
        // Start is called before the first frame update
        void Start()
        {
            group = GetComponent<CinemachineTargetGroup>();
            var gridManager = GridManager.GetSingleton();
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
