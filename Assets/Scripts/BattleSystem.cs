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
        [SerializeField] private BattleState state;
        [SerializeField] private GameObject player;


        public void ResetBattle()
        {
            print("resetting battle");
            state = BattleState.SETUP;
            //player = BattleCharacter.Player().Spawn();
            //battleCharacters.Enqueue();
        }
    }
}
