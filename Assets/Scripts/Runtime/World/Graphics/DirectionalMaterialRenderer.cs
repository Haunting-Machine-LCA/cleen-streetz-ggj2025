using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World.Graphics
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    [RequireComponent(typeof(MeshRenderer))]
    public class DirectionalMaterialRenderer : DirectionalRenderer<MeshRenderer>
    {
        protected override void UpdateRenderer(int slice) =>
            Renderer.sharedMaterial.mainTexture = directionalTextureSheet[slice];
    }
}
