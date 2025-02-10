using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTween;
using UnityEngine.UIElements.Experimental;


namespace Hmlca.CS.World
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class Facing : EntityComponent
    {
        private const int DEFAULT_SIDES = 8;


        [SerializeField] private float fullTurnSeconds;
        [SerializeField, Range(0f, 360f)] private float facingAngle;
        [SerializeField] private FloatTween tween = new FloatTween();
        [SerializeField, Range(1, 8)] private int sides = DEFAULT_SIDES;
        private Transform cameraRootTransform;


        public float FacingAngle => facingAngle;
        public Direction FacingDirection => facingAngle.ToDirection();


        private void Update()
        {
            if (tween.State == TweenState.Running && facingAngle != tween.EndValue)
            {
                tween.Update(Time.deltaTime);
                float targetAngle = tween.CurrentValue;
                float delta = Mathf.DeltaAngle(facingAngle, targetAngle);
                float newAngle = facingAngle + delta;
                facingAngle = newAngle % 360;
                if (delta < float.Epsilon)
                    tween.Stop(StopBehavior.ForceComplete);
                SendMessage(IFacingAngleMessageReciever.MESSAGE, facingAngle);
            }
        }


        public void SetFacing(Direction direction)
        {
            float angle = direction.ToAngle();
            SetFacing(angle);
        }


        public void SetFacing(float angle, bool instant = false)
        {
            if (instant)
            {
                facingAngle = angle;
                return;
            }
            float delta = Mathf.DeltaAngle(facingAngle, angle);
            if (delta != 0f)
                Rotate(delta);
        }


        public void Rotate(float delta)
        {
            float startAngle = facingAngle % 360;
            float targetAngle = (facingAngle + delta) % 360f;
            float timeSeconds = Mathf.Abs(delta / 360f) * fullTurnSeconds;
            tween.Start(startAngle, targetAngle, timeSeconds, ScaleFuncs.Linear);
            if (timeSeconds <= 0f && targetAngle == startAngle)
                tween.Stop(StopBehavior.ForceComplete);
        }


        public int CameraFacing()
        {
            if (!cameraRootTransform)
                cameraRootTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
            int slice = (int) FacingDirection;
            int sliceOffsetFromCamera = GetCameraOffset(slice, cameraRootTransform);
            var currentSlice = slice + sliceOffsetFromCamera;
            if (currentSlice < 0)
                currentSlice += (int)Direction.Northwest + 1;
            return currentSlice;
        }


        protected int GetCameraOffset(int dir, Transform camera)
        {
            if (!camera)
                return 0;
            int slice = (int)camera.eulerAngles.y.ToDirection(sides);
            ////float delta = Mathf.DeltaAngle(facing.FacingAngle, cameraRootTransform.eulerAngles.y);
            //float delta = Mathf.Floor((cameraRootTransform.eulerAngles.y - facing.FacingAngle) / 45f);
            //int sliceOffsetFromCamera = (int)Mathf.Abs(delta).ToDirection();// (int)Mathf.Sign(delta);
            return -slice;
        }
    }
}
