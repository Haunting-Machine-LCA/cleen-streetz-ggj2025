using SerializeReferenceEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World.Graphics
{
    public abstract class DirectionalRenderer<T> : EntityComponent where T : Renderer
    {
        [SerializeField] protected DirectionalTextureSheet directionalTextureSheet;
        [SerializeField] protected Facing facing;
        [SerializeField] protected Transform cameraRootTransform;
        [SerializeField] protected T rendererComponent;
        [SerializeField] protected int currentSlice;


        public T Renderer => rendererComponent;


        protected override void Awake()
        {
            base.Awake();
            TryGetEntityComponent(out Facing facingComponent);
            rendererComponent = GetComponent<T>();
        }


        protected virtual void LateUpdate() => UpdateRenderer();


        public void UpdateRenderer()
        {
            int previousSlice = currentSlice;
            UpdateCameraFacing();
            if (previousSlice != currentSlice)
                UpdateRenderer(currentSlice);
        }


        protected abstract void UpdateRenderer(int slice);


        protected virtual void UpdateCameraFacing()
        {
            int slice = (int)facing.FacingDirection;
            int sliceOffsetFromCamera = GetCameraOffset(slice);
            currentSlice = slice + sliceOffsetFromCamera;
            if (currentSlice < 0)
                currentSlice += (int)Direction.Northwest + 1;
            //currentSlice %= (int) Direction.Northwest;
        }


        protected virtual int GetCameraOffset(int dir)
        {
            if (!cameraRootTransform)
                return 0;
            var sides = directionalTextureSheet.Sides;
            int slice = (int) cameraRootTransform.eulerAngles.y.ToDirection(sides);
            ////float delta = Mathf.DeltaAngle(facing.FacingAngle, cameraRootTransform.eulerAngles.y);
            //float delta = Mathf.Floor((cameraRootTransform.eulerAngles.y - facing.FacingAngle) / 45f);
            //int sliceOffsetFromCamera = (int)Mathf.Abs(delta).ToDirection();// (int)Mathf.Sign(delta);
            return -slice;
        }
    }
}
