using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hmlca.Untitled.TurnSystem;

namespace Hmlca.Untitled
{
    public class BattleSystem : Singleton<BattleSystem>
    {
        public enum BattleState { SETUP, COMMAND, EXECUTE, WIN, LOSE }
        public class BattleCharacter : Object
        {
            public GameObject prefab;
            public GameObject gameObject;
            public int health;
            public int armor;
            public int attack;
        }


        [SerializeField] private Queue<BattleCharacter> battleCharacters = new Queue<BattleCharacter>();
        private BattleState state = BattleState.SETUP;


        public void ResetBattle()
        {
            state = BattleState.SETUP;
            battleCharacters.Enqueue(
                new BattleCharacter
                {
                    prefab = Resources.Load<GameObject>("Prefabs/Player"),
                    health = 100,
                    armor = 10,
                    attack = 10
                }
            );
        }
    }
}
