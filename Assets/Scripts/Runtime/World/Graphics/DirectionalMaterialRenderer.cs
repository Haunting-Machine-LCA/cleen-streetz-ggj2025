using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    [RequireComponent(typeof(MeshRenderer))]
    public class DirectionalMaterialRenderer : EntityComponent
    {
        [SerializeField] private DirectionalTextureSheet directionalTextureSheet;
        [SerializeField] private Facing facing;
        [SerializeField] private Transform cameraRootTransform;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private int currentSlice;


        protected override void Awake()
        {
            base.Awake();
            TryGetEntityComponent(out Facing facingComponent);
            meshRenderer = GetComponent<MeshRenderer>();
        }


        private void LateUpdate()
        {
            int previousSlice = currentSlice;
            UpdateCameraFacing();
            if (previousSlice != currentSlice)
                meshRenderer.material.mainTexture = directionalTextureSheet[currentSlice];
        }


        private void UpdateCameraFacing()
        {
            int slice = (int)facing.FacingDirection;
            int sliceOffsetFromCamera = GetCameraOffset(slice);
            currentSlice = slice + sliceOffsetFromCamera;
            if (currentSlice < 0)
                currentSlice += (int)Direction.Northwest + 1;
            //currentSlice %= (int) Direction.Northwest;
        }


        private int GetCameraOffset(int dir)
        {
            int slice = (int) cameraRootTransform.eulerAngles.y.ToDirection();
            ////float delta = Mathf.DeltaAngle(facing.FacingAngle, cameraRootTransform.eulerAngles.y);
            //float delta = Mathf.Floor((cameraRootTransform.eulerAngles.y - facing.FacingAngle) / 45f);
            //int sliceOffsetFromCamera = (int)Mathf.Abs(delta).ToDirection();// (int)Mathf.Sign(delta);
            return -slice;
        }
    }
}
