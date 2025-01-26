using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    public class Battle
    {
        public List<BattleCharacter> battlers = new List<BattleCharacter>();
        public int width;
        public int height;


        public static Battle Tutorial()
        {
            Battle battle = new Battle();
            battle.width = 12;
            battle.height = 12;
            battle.battlers.Add(BattleCharacter.Enemy1());
            return battle;
        }


        public static Battle FinalBoss()
        {
            Battle battle = new Battle();
            battle.width = 16;
            battle.height = 16;
            battle.battlers.Add(BattleCharacter.Enemy3());
            return battle;
        }
    }
}
