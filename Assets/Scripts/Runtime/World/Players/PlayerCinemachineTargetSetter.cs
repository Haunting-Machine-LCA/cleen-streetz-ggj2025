using Cinemachine;
using Hmlca.CS.App;
using Hmlca.CS.Inputs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Hmlca.CS.World.Players
{
    [RequireComponent(typeof(CinemachineTargetGroup))]
    public class PlayerCinemachineTargetSetter : Singleton<PlayerCinemachineTargetSetter>
    {
        [SerializeField] private CinemachineTargetGroup targetGroup;
        [SerializeField] private Transform playerTransform;


        protected override void Awake()
        {
            base.Awake();
            if (!targetGroup)
                targetGroup = GetComponent<CinemachineTargetGroup>();
        }


        private void LateUpdate()
        {
            if (!playerTransform)
            {
                if (FindPlayerTransform(out playerTransform))
                    CameraControl.GetSingleton()
                        .FollowAndLookAt(playerTransform);
            }
        }


        private bool FindPlayerTransform(out Transform playerTransform)
        {
            playerTransform = Player.GetSingleton(true)?.transform;
            if (playerTransform)
                return SetTarget(playerTransform);
            return playerTransform;
        }


        private bool SetTarget(Transform playerTransform)
        {
            var targets = targetGroup.m_Targets.ToList();
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                var target = targets[i].target;
                if (target)
                    targets.RemoveAt(i);
            }
            targetGroup.m_Targets = targets.ToArray();
            if (playerTransform)
            {
                targetGroup.AddMember(playerTransform, 1, 1);
                return true;
            }
            return false;
        }
    }
}
