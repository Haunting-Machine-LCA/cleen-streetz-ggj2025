using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    // State machine of the current battle
    public enum BattleState
    {
        START,
        PLAYER_TURN,
        ENEMY_TURN,
        WIN,
        LOSE
    }
    public class BattleSystem : MonoBehaviour
    {
        public BattleState state;
        // Start is called before the first frame update
        void Start()
        {
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }

        IEnumerator SetupBattle()
        {
            Debug.Log("Setting up battle");
            // FIX ME: Set up the battle
            yield return new WaitForSeconds(2f);

            state = BattleState.PLAYER_TURN;
            PlayerTurn();
        }

        void PlayerTurn()
        {
            Debug.Log("Player turn");
            // FIX ME: Get player input for actions -> OnPlayerAttack()
        }

        // Player's action
        void OnPlayerAttack()
        {
            if(state != BattleState.PLAYER_TURN) return;
            StartCoroutine(PlayerAttack());
        }

        IEnumerator PlayerAttack()
        {
            Debug.Log("Player attacks!");
            yield return new WaitForSeconds(1f);

            bool enemyDefeated = CheckEnemyDefeated();
            if (enemyDefeated)
            {
                state = BattleState.WIN;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMY_TURN;
                StartCoroutine(EnemyTurn());
            }
        }

        IEnumerator EnemyTurn()
        {
            Debug.Log("Enemy's turn");
            yield return new WaitForSeconds(1f);

            bool playerDefeated = CheckPlayerDefeated();
            if (playerDefeated)
            {
                state = BattleState.LOSE;
                EndBattle();
            }
            else 
            {
                state = BattleState.PLAYER_TURN;
                PlayerTurn();
            }
        }

        void EndBattle()
        {
            if (state == BattleState.WIN)
            {
                Debug.Log("You win!");
            }
            else if (state == BattleState.LOSE)
            {
                Debug.Log("You lost...");
            }
        }

        bool CheckEnemyDefeated()
        {
            // FIX ME: Check if enemy is out of health
            return false;
        }

        bool CheckPlayerDefeated()
        {
            // FIX ME: Check if player is out of health
            return false;
        }
    }
}
