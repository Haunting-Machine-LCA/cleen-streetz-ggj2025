using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Hmlca.CS
{
    public class GridTargetGroup : MonoBehaviour
    {
        private CinemachineTargetGroup group;
        // Start is called before the first frame update
        void Start()
        {
            GridManager.GetSingleton().targetGroupObj = this.gameObject;
        }

        public void SetTargets(List<GameObject> objs)
        {
            var tg = GetComponent<CinemachineTargetGroup>();
            foreach (GameObject obj in objs)
            {
                tg.AddMember(obj.transform, 1.0f, 1.0f);
            }
        }
    }
}
