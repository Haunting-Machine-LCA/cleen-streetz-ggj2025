using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;

namespace Hmlca.Untitled
{
    public class CameraControl : MonoBehaviour
    {
        public Vector3 offset = new Vector3(10, 10, 10);
        public Transform target;
        public float duration = 0.5f;
        private bool isMoving = false;
        public bool IsMoving => isMoving;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void LateUpdate()
        {
            if (target == null || isMoving) return;

            // Move
            isMoving = true;
            Vector3 targetPosition = target.position + offset;
            transform.DOMove(targetPosition, duration)
                .SetEase(Ease.OutQuad)
                .OnStart(() => isMoving = true)
                .OnComplete(() => isMoving = false);

            // Rotation
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.DORotateQuaternion(targetRotation, duration).SetEase(Ease.OutExpo);
        }

        public void SetFocus(Transform t)
        {
            target = t;
        }
    }
}
