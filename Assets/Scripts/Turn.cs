using Hmlca.Untitled;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hmlca.Untitled.TurnSystem;

namespace Hmlca.Untitled
{
    // State machine of the current battle
    public class Turn 
    {
        public TurnController controller;


        public static Turn Player()
        {
            var turn = new Turn { controller = TurnController.PLAYER };
            return turn;
        }


        public static Turn Enemy()
        {
            var turn = new Turn { controller = TurnController.ENEMY };
            return turn;
        }


        public static Turn Tutorial()
        {
            var turn = new Turn { controller = TurnController.TUTORIAL };
            return turn;
        }
    }


    public enum TurnController { PLAYER, ENEMY, TUTORIAL }
}
