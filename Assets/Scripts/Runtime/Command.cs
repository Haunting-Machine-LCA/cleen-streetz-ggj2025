using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    public enum CommandType
    {
        MOVE,
        ATTACK1,
        ATTACK2,
        ATTACK3,
    }


    public class Command
    {
        public GridEntity entity;
        public Vector2Int direction;
        public CommandType type;
    }
}
