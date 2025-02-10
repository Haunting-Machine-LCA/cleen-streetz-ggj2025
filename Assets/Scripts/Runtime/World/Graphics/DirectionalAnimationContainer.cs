using System;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World.Graphics
{
    [Serializable]
    public abstract class DirectionalAnimationContainer : DirectionalDataContainer
    {
        [Tooltip("List of animations for each direction, starting from North and going clockwise.")]
        [SerializeField] protected List<AnimationClip> directionalAnimations = new List<AnimationClip>();


        public List<AnimationClip> DirectionalAnimations => directionalAnimations;
        public AnimationClip this[int index] => Get<AnimationClip>(index);


        public DirectionalAnimationContainer() : base() { }
    }
}
