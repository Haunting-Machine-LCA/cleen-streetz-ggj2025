using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    [RequireComponent(typeof(Player), typeof(CharacterHp))]
    public class PlayerHp : Singleton<PlayerAttack>
    {
        protected override void Awake()
        {
            base.Awake();
            GetComponent<CharacterHp>().OnDamage.AddListener(() =>
            {
                
            });
        }
    }
}
