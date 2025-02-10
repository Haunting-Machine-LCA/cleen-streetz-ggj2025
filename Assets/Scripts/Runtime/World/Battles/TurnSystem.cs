using Hmlca.CS;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hmlca.CS.App;
using Hmlca.CS.World.Players;


namespace Hmlca.CS.World.Battles
{
    // State machine of the current battle
    public class TurnSystem : Singleton<TurnSystem>
    {
        public List<Turn> turnQueue = new List<Turn>();
        public Coroutine currentTurnRoutine;
        public bool isDone;
        private int currentTurnIndex;


        public Turn CurrentTurn
        {
            get
            {
                if (currentTurnIndex >= turnQueue.Count)
                    return Turn.Player();
                return turnQueue[currentTurnIndex];
            }
        }


        private void Update()
        {

        }


        public void ResetTurnQueue()
        {
            currentTurnIndex = 0;
            turnQueue.Clear();
            GatherTurns();
            NextTurn();
        }


        private void GatherTurns()
        {
            turnQueue.Add(Turn.Player());
            turnQueue.Add(Turn.Enemy());
        }


        public void PushTurn(Turn turn, bool toTopOfQueue = false)
        {
            if (toTopOfQueue)
                turnQueue.Insert(0, turn);
            else
                turnQueue.Add(turn);
        }


        public void NextTurn()
        {
            BattleSystem.GetSingleton().GoToState(BattleSystem.BattleState.COMMAND);
            Debug.Log($"{CurrentTurn.controller} TURN START!");
            if (CurrentTurn.controller == TurnController.PLAYER)
            {
                PlayerMovement.GetSingleton().enabled = true;
                PlayerAttack.GetSingleton().enabled = true;
            }
            else
            {
                PlayerMovement.GetSingleton().enabled = false;
                PlayerAttack.GetSingleton().enabled = false;
                currentTurnRoutine = StartCoroutine(ExecuteTurn(CurrentTurn.controller));
            }
        }


        public IEnumerator ExecuteTurn(TurnController turn)
        {
            BattleSystem.GetSingleton().GoToState(BattleSystem.BattleState.EXECUTE);
            if (turn == TurnController.ENEMY)
            {
                //EnemyAI.currentTurnPlans.Clear();
                var enemies = FindObjectsByType<EnemyAI>(
                    FindObjectsInactive.Exclude,
                    FindObjectsSortMode.None
                );
                foreach (var enemy in enemies)                      //DEBUG
                    yield return new WaitForSecondsRealtime(0.15f); //DEBUG
                //foreach (var enemy in enemies)
                //{
                //    var plan = enemy.ReadyPlan();
                //}
                //while (EnemyAI.currentTurnPlans.Count > 0)
                //{
                //    var plan = EnemyAI.currentTurnPlans.Dequeue();
                //    yield return plan.ExecutePlan();
                //}

            }
            else
            {
                if (currentTurnRoutine != null)
                    yield return currentTurnRoutine;
            }
            OnTurnEnd();
        }


        private void OnTurnEnd()
        {
            Debug.Log($"{CurrentTurn.controller} TURN START!");
            var player = Player.GetSingleton();
            if (player && player.TryGetComponent<CharacterHp>(out var hp))
            {
                if (hp.isDead)
                {
                    BattleSystem.GetSingleton().GoToState(BattleSystem.BattleState.LOSE);
                    return;
                }
            }
            if (currentTurnIndex + 1 >= turnQueue.Count)
                currentTurnIndex = 0;
            else
                currentTurnIndex++;
            NextTurn();
        }
    }
}
