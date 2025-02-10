using Hmlca.CS.World.Graphics;
using SerializeReferenceEditor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    [Serializable]
    [CreateAssetMenu(fileName = "DirectionalTextureSheet", menuName = "Hmlca/Gfx/DirectionalTextureSheet", order = 0)]
    public class DirectionalTextureSheet : ScriptableObject
    {
        private const int ALL_SIDES = 8;


        [
            SerializeReference, 
            SR(new Type[] {
                typeof(DirectionalTexture8),
                typeof(DirectionalTexture4),
            })
        ] private DirectionalTextureContainer directionalTextures;


        public Texture2D this[int index] => directionalTextures[index];
        public Texture2D this[Direction direction] => this[GetIndexFromDirection(direction)];
        public virtual int Sides => directionalTextures.Sides;
        public int IndexScale => Mathf.FloorToInt((float) ALL_SIDES / (float) Sides);


        private void Awake()
        {
            FillEmptyIndicesWithPlaceholder();
        }


        private void OnValidate()
        {
            FillEmptyIndicesWithPlaceholder();
            var directionalTextures = this.directionalTextures.DirectionalTextures;
            if (directionalTextures.Count > Sides)
            {
                directionalTextures.RemoveRange(Sides, directionalTextures.Count - Sides);
                Debug.LogError($"{GetType().Name} has too many textures in List. It should have {Sides} textures.");
            }
        }

        public int GetIndexFromDirection(Direction direction) => Mathf.FloorToInt((float)(int)direction / IndexScale);


        private void FillEmptyIndicesWithPlaceholder()
        {
            var directionalTextures = this.directionalTextures.DirectionalTextures;
            while (directionalTextures.Count < Sides)
                directionalTextures.Add(null);
            for (int i = 0; i < Sides; i++)
            {
                if (directionalTextures[i] == null)
                    directionalTextures[i] = Resources.Load<Texture2D>("placeholder_000");
            }
        }
    }
}
