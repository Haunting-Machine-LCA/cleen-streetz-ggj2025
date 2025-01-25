using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    public class BattleCharacter
    {
        public GameObject prefab;
        public GameObject gameObject;
        public int health;
        public int armor;
        public int attack;
        public TurnController turnController;


        public BattleCharacter(GameObject prefab, int health, int armor, int attack, TurnController turnController)
        {
            this.prefab = prefab;
            this.health = health;
            this.armor = armor;
            this.attack = attack;
            this.turnController = turnController;
        }


        public static BattleCharacter Player()
        {
            const int HEALTH = 100;
            const int ARMOR = 1;
            const int ATTACK = 10;
            return new BattleCharacter(
                Resources.Load<GameObject>("Player"), 
                HEALTH, 
                ARMOR, 
                ATTACK,
                TurnController.PLAYER
            );
        }


        public static BattleCharacter Enemy1()
        {
            const int HEALTH = 100;
            const int ARMOR = 1;
            const int ATTACK = 10;
            return new BattleCharacter(
                Resources.Load<GameObject>("Player"),
                HEALTH,
                ARMOR,
                ATTACK,
                TurnController.ENEMY
            );
        }


        public static BattleCharacter Enemy2()
        {
            const int HEALTH = 100;
            const int ARMOR = 1;
            const int ATTACK = 10;
            return new BattleCharacter(
                Resources.Load<GameObject>("Player"),
                HEALTH,
                ARMOR,
                ATTACK,
                TurnController.ENEMY
            );
        }


        public static BattleCharacter Enemy3()
        {
            const int HEALTH = 100;
            const int ARMOR = 1;
            const int ATTACK = 10;
            return new BattleCharacter(
                Resources.Load<GameObject>("Player"),
                HEALTH,
                ARMOR,
                ATTACK,
                TurnController.ENEMY
            );
        }


        public GameObject Spawn()
        {
            gameObject = GameObject.Instantiate(prefab);
            return gameObject;
        }
    }
}
