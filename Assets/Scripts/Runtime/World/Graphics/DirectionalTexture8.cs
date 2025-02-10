using SerializeReferenceEditor;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World.Graphics
{
    [Serializable, SRName("Data/8-Directional Texture")]
    public class DirectionalTexture8 : DirectionalTextureContainer
    {
        private const int ALL_SIDES = 8;
        public override int Sides => ALL_SIDES;


        public DirectionalTexture8() : base() { }
    }
}
