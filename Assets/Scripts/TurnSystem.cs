using Hmlca.Medstrat;
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
        
        
        public Queue<Turn> turnQueue = new Queue<Turn>();
    }
}
