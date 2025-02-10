using System;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World.Graphics
{
    [Serializable]
    public abstract class DirectionalTextureContainer : DirectionalDataContainer
    {
        [Tooltip("List of textures for each direction, starting from North and going clockwise.")]
        [SerializeField] protected List<Texture2D> directionalTextures = new List<Texture2D>();


        public List<Texture2D> DirectionalTextures => directionalTextures;
        public Texture2D this[int index] => Get<Texture2D>(index);


        public DirectionalTextureContainer() : base() { }
    }
}
