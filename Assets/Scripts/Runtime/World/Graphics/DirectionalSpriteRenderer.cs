using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World.Graphics
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    [RequireComponent(typeof(SpriteRenderer))]
    public class DirectionalSpriteRenderer : DirectionalRenderer<SpriteRenderer>
    {
        private List<Sprite> directionalSprites = new List<Sprite>();


        protected override void UpdateRenderer(int slice) =>
            Renderer.sprite = GetSprite(slice);
    

        private Sprite GetSprite(int slice)
        {
            var sprite = directionalSprites[slice];
            if (sprite == null)
            {
                var texture = directionalTextureSheet[slice];
                directionalSprites[slice] = Sprite.Create(
                    texture, 
                    new Rect(Vector2.zero, Renderer.size),
                    Vector2.zero
                    );
            }
            return directionalSprites[slice];
        }
    }
}
