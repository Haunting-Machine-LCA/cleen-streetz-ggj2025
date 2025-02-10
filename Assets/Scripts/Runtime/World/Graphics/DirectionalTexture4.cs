using SerializeReferenceEditor;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World.Graphics
{
    [Serializable, SRName("Data/4-Directional Texture")]
    public class DirectionalTexture4 : DirectionalTextureContainer
    {
        private const int ALL_SIDES = 4;
        public override int Sides => ALL_SIDES;


        public DirectionalTexture4() : base() { }
    }
}
