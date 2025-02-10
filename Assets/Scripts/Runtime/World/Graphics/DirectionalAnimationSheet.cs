using Hmlca.CS.World.Graphics;
using SerializeReferenceEditor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    [Serializable]
    [CreateAssetMenu(fileName = "DirectionalAnimationSheet", menuName = "Hmlca/Gfx/DirectionalAnimationSheet", order = 1)]
    public class DirectionalAnimationSheet : ScriptableObject
    {
        private const int ALL_SIDES = 8;


        [
            SerializeReference, 
            SR(new Type[] {
                typeof(DirectionalAnimation4),
                typeof(DirectionalAnimation8),
            })
        ] private DirectionalAnimationContainer directionalAnimations;


        public AnimationClip this[int index] => directionalAnimations[index];
        public AnimationClip this[Direction direction] => this[GetIndexFromDirection(direction)];
        public virtual int Sides => directionalAnimations.Sides;
        public int IndexScale => Mathf.FloorToInt((float) ALL_SIDES / (float) Sides);


        private void Awake()
        {
            FillEmptyIndicesWithPlaceholder();
        }


        private void OnValidate()
        {
            FillEmptyIndicesWithPlaceholder();
            var directionalAnimations = this.directionalAnimations.DirectionalAnimations;
            if (directionalAnimations.Count > Sides)
            {
                directionalAnimations.RemoveRange(Sides, directionalAnimations.Count - Sides);
                Debug.LogError($"{GetType().Name} has too many animation clips in List. It should have {Sides} animation clips.");
            }
        }

        public int GetIndexFromDirection(Direction direction) => Mathf.FloorToInt((float)(int)direction / IndexScale);


        private void FillEmptyIndicesWithPlaceholder()
        {
            var directionalTextures = this.directionalAnimations.DirectionalAnimations;
            while (directionalTextures.Count < Sides)
                directionalTextures.Add(null);
            for (int i = 0; i < Sides; i++)
            {
                if (directionalTextures[i] == null)
                    directionalTextures[i] = Resources.Load<AnimationClip>("placeholder_000");
            }
        }
    }
}
