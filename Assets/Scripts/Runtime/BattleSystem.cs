using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Hmlca.CS.TurnSystem;

namespace Hmlca.CS
{
    public class BattleSystem : Singleton<BattleSystem>
    {
        public enum BattleState { PAUSED, COMMAND, EXECUTE, WIN, LOSE }


        public UnityEvent OnBattleStart = new UnityEvent();
        public UnityEvent OnEnterCommand = new UnityEvent();
        public UnityEvent OnEnterExecute = new UnityEvent();
        public UnityEvent OnEnterWin = new UnityEvent();
        public UnityEvent OnEnterLose = new UnityEvent();
        [SerializeField] private List<Battle> battles;
        [SerializeField] private BattleState state;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform gridParent;
        [SerializeField] private GameObject gridPrefab;
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<BattleCharacterEntity> battlers = new List<BattleCharacterEntity>();
        private int battleIndex = 0;


        protected override void Awake()
        {
            base.Awake();
            battles = new List<Battle>()
            {
                Battle.Tutorial(),
                Battle.FinalBoss()
            };
        }


        public void NewGame()
        {
            battleIndex = 0;
            ResetBattle(true);
        }


        public void NextBattle()
        {
            battleIndex++;
            if (battleIndex >= battles.Count)
            {
                state = BattleState.WIN;
                return;
            }
            else
                ResetBattle();
            print($"starting battle: {battleIndex}");
        }


        public void Win()
        {
            print("you won!");
            GoToState(BattleState.WIN);
        }


        public void Lose()
        {
            print("you lost!");
            GoToState(BattleState.LOSE);
        }   


        public void ResetBattle(bool resetPlayer = false)
        {
            print("resetting battle");
            GoToState(BattleState.PAUSED);
            ResetGrid();
            if (resetPlayer)
                ResetPlayer();
            ResetBattleObjects();
            CameraControl.GetSingleton().FollowAndLookAt(player.transform);
            GoToState(BattleState.COMMAND);
            OnBattleStart?.Invoke();
        }


        private void ResetPlayer()
        {
            if (player)
                Destroy(player);
        }


        private void ResetBattleObjects()
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
                Destroy(gameObjects[i]);
            var objects = battles[battleIndex].Spawn();
            gameObjects.AddRange(objects);
        }

        
        private void ResetGrid()
        {
            var gridManager = FindObjectOfType<GridManager>();
            if (gridManager)
                Destroy(gridManager.gameObject);
            Instantiate(gridPrefab, gridParent);
        }


        public void RegisterGameObject(GameObject go)
        {
            gameObjects.Add(go);
        }


        public void UnregisterGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }


        public void RegisterBattler(BattleCharacterEntity battler)
        {
            battlers.Add(battler);
        }


        public void UnregisterBattler(BattleCharacterEntity battler)
        {
            battlers.Remove(battler);
        }


        public void GoToState(BattleState state)
        {
            this.state = state;
            switch (state)
            {
                case BattleState.PAUSED:
                    break;
                case BattleState.COMMAND:
                    OnEnterCommand?.Invoke();
                    break;
                case BattleState.EXECUTE:
                    OnEnterExecute?.Invoke();
                    break;
                case BattleState.WIN:
                    OnEnterWin?.Invoke();
                    break;
                case BattleState.LOSE:
                    OnEnterLose?.Invoke();
                    break;
            }
        }
    }
}
