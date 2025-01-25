using Hmlca.Untitled;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    // State machine of the current battle
    public class TurnSystem : Singleton<TurnSystem>
    {
        public enum TurnController { PLAYER, ENEMY, TUTORIAL }
        public class Turn
        {
            public TurnController controller;
        }
        
        
        public List<Turn> turnQueue = new List<Turn>();


        public void PushTurn(Turn turn, bool toTopOfQueue = false)
        {
            if (toTopOfQueue)
                turnQueue.Insert(0, turn);
            else
                turnQueue.Add(turn);
        }
    }
}
