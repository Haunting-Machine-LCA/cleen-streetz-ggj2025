using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Hmlca.Untitled.TurnSystem;

namespace Hmlca.Untitled
{
    public class BattleSystem : Singleton<BattleSystem>
    {
        public enum BattleState { SETUP, COMMAND, EXECUTE, WIN, LOSE }


        public UnityEvent OnBattleStart = new UnityEvent();
        [SerializeField] private Queue<BattleCharacter> battles = new Queue<BattleCharacter>();
        [SerializeField] private BattleState state;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform gridParent;
        [SerializeField] private GameObject gridPrefab;
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<BattleCharacterEntity> battlers = new List<BattleCharacterEntity>();


        public void ResetBattle()
        {
            print("resetting battle");
            state = BattleState.SETUP;
            ResetPlayer();
            ResetBattleObjects();
            ResetGrid();
            battles.Enqueue(
                BattleCharacter.Enemy1());
            battles.Enqueue(
                    BattleCharacter.Enemy2());
            battles.Enqueue(
                BattleCharacter.Enemy3());
            OnBattleStart?.Invoke();
        }


        private void ResetPlayer()
        {
            if (player)
                Destroy(player);
            player = BattleCharacter.Player()
                .Spawn();
        }


        private void ResetBattleObjects()
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
                Destroy(gameObjects[i]);
        }

        
        private void ResetGrid()
        {
            var gridManager = GridManager.GetSingleton();
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
    }
}
