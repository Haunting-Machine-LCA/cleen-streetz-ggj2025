using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hmlca.Untitled
{
    [RequireComponent(typeof(CharacterAnimations))]
    public class CharacterHp : MonoBehaviour
    {
        public UnityEvent OnDamage = new UnityEvent();
        public UnityEvent OnDeath = new UnityEvent();
        public int hp = 100;
        public int maxHp = 100;
        public bool isDead;
        [SerializeField] private CharacterAnimations animator;


        public static void Damage(Attack attack, params Vector3Int[] positions)
        {
            foreach (var pos in positions)
            {
                if (GridEntity.gridObjectsByPos.TryGetValue(pos, out var entity))
                    entity.GetComponent<CharacterHp>()?.Damage(attack.damage);
            }
        }


        private void Awake()
        {
            if (!animator)
                animator = GetComponentInChildren<CharacterAnimations>();
        }


        public void FullHeal() => hp = maxHp;


        public void SetHealth(int newHp)
        {
            if (newHp > 0 && newHp < hp)
                Damage(hp - newHp);
            hp = newHp;
            if (hp <= 0)
                Die();
        }


        public void Damage(int damage)
        {
            animator.DamagedAnim();
            OnDamage?.Invoke();
        }


        private void Die()
        {
            if (!isDead)
            {
                isDead = true;
                OnDeath?.Invoke();
            }
        }
    }
}
