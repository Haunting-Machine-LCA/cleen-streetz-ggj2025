using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    [RequireComponent(typeof(Player))]
    public class PlayerAttack : Singleton<PlayerAttack>
    {
        public const KeyCode ATTACK = KeyCode.Space;


        public int ap = 3;
        public int maxAp = 3;
        public int baseDamage = 10;
        [SerializeField] private GridEntity entity;
        [SerializeField] private CharacterAnimations animator;


    }
}
