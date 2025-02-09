using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS.World
{
    public class Projectile : GridEntity
    {
        public int attackModifier;
        public int armorPenetration;
        public int range;
        public bool meander;
    }
}
