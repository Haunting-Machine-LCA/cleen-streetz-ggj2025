using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    [RequireComponent(typeof(Player))]
    public class PlayerAttack : Singleton<PlayerAttack>
    {
        public int ap = 3;
        public int maxAp = 3;
        public int baseDamage = 10;
    }
}
