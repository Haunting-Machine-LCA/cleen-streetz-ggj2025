using System;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World.Graphics
{
    [Serializable]
    public abstract class DirectionalDataContainer
    {
        private List<object> directionalObjects = new List<object>();


        protected List<object> DirectionalObjects => directionalObjects;
        public abstract int Sides { get; }
        

        protected T Get<T>(int index) => (T)DirectionalObjects[index];


        public DirectionalDataContainer() { }
    }
}
