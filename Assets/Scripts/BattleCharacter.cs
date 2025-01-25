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
    
    
        public BattleCharacter(GameObject prefab, int health, int armor, int attack)
        {
            this.prefab = prefab;
            this.health = health;
            this.armor = armor;
            this.attack = attack;
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
                ATTACK
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
                ATTACK
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
                ATTACK
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
                ATTACK
            );
        }


        public GameObject Spawn()
        {
            gameObject = GameObject.Instantiate(prefab);
            return gameObject;
        }
    }
}
