using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Hmlca.CS.App;
using Hmlca.CS.World.Battles;
using Hmlca.CS.Inputs;


namespace Hmlca.CS.World.Players
{
    [RequireComponent(typeof(Player))]
    public class PlayerAttack : PlayerInput<PlayerAttack>
    {
        public const KeyCode ATTACK = KeyCode.Space;


        public UnityEvent<float> OnAttackCharge = new UnityEvent<float>();
        public int ap = 3;
        public int maxAp = 3;
        public int baseDamage = 10;
        [SerializeField] private GridEntity entity;
        [SerializeField] private PlayerAnimations animator;
        [SerializeField] private float chargeTimeSeconds;
        private float charge;
        private Coroutine attackRoutine;



        private void Update()
        {
            if (Input.GetKey(ATTACK))
                IncreaseCharge(Time.deltaTime);
            else if (charge > 0f)
                ReleaseAttack();
        }


        private void IncreaseCharge(float amount)
        {
            charge += amount;
            OnAttackCharge?.Invoke(charge);
        }


        private IEnumerator ReleaseAttack()
        {
            charge = 0f;
            attackRoutine = StartCoroutine(PerformAttack());
            var turnSystem = TurnSystem.GetSingleton();
            turnSystem.currentTurnRoutine = attackRoutine;
            yield return turnSystem.ExecuteTurn(TurnController.PLAYER);
        }


        private IEnumerator PerformAttack()
        {
            animator.AttackAnim();
            //AudioManager.GetSingleton()?("PlayerAttack");
            yield return new WaitForSeconds(0.4f);
        }
    }
}
