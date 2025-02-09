using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Hmlca.CS.App;

namespace Hmlca.CS.Inputs
{
    public class CameraControl : Singleton<CameraControl>
    {
        private CinemachineBrain brain;
        public CinemachineFreeLook closeCam;
        public CinemachineVirtualCamera overviewCam;
        private CameraDirection camDir = CameraDirection.NORTH;
        private float targetAngle;
        private Transform followTarget = null;
        private Transform lookTarget = null;
        private bool overviewing = false;
        [SerializeField] Dictionary<CameraDirection,float> directionDict = new Dictionary<CameraDirection,float>{
            {CameraDirection.NORTH, 0f},
            {CameraDirection.SOUTH, 180f},
            {CameraDirection.EAST, 90f},
            {CameraDirection.WEST, -90f}
        };
        
        // Start is called before the first frame update
        void Start()
        {
            if (overviewCam == null) Debug.LogError("Missing overview virtual camera!");
            if (closeCam == null) Debug.LogError("Missing close virtual camera!");
            brain = gameObject.GetComponent<CinemachineBrain>();
            UseCloseCam(); // Close cam by default
            // UseOverviewCam();
            SetCameraDirection(camDir); // Default direction
        }

        void Update()
        {
            // FIX ME - use Input instead of hardcoding
            if (Input.GetMouseButtonDown(1) && !overviewing)
            {
                UseOverviewCam();
            }
            if (Input.GetMouseButtonUp(1) && overviewing)
            {
                UseCloseCam();
            }
            // if (Input.GetMouseButtonUp(1))
            // {
            //     closeCam.m_XAxis.Value += 90;
            //     overviewCam.GetComponent<CinemachineOrbitalTransposer>().m_Heading.m_Bias += 90;
            // }
        }

        public Transform GetFollowTarget() {
            return followTarget;
        }

        public Transform GetLookTarget() {
            return lookTarget;
        }

        public void FollowAndLookAt(Transform t) {
            SetFollowOnly(t);
            SetLookOnly(t);
        }

        public void SetFollowOnly(Transform t) {
            followTarget = t;
            closeCam.Follow = followTarget;
        }

        public void SetLookOnly(Transform t) {
            lookTarget = t;
            closeCam.LookAt = lookTarget;
        }

        public enum CameraDirection { NORTH, SOUTH, EAST, WEST }

        public void SetCameraDirection(CameraDirection newDir) {
            camDir = newDir;
            targetAngle = directionDict[camDir];
            closeCam.m_XAxis.Value = targetAngle;
        }

        private void UseOverviewCam()
        {
            overviewing = true;
            closeCam.gameObject.SetActive(false);
            overviewCam.gameObject.SetActive(true);
        }

        private void UseCloseCam()
        {
            overviewing = false;
            overviewCam.gameObject.SetActive(false);
            closeCam.gameObject.SetActive(true);
        }
    }
}
