using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS
{
    public enum Direction : int
    {
        North = 0,
        Northeast = 1,
        East = 2,
        Southeast = 3,
        South = 4,
        Southwest = 5,
        West = 6,
        Northwest = 7,
        LEN = 8,
    }


    public static class DirectionExt
    {
        public static float ToAngle(this Direction dir) => (int)dir * 45f;
        public static Direction ToDirection(this float angle)
        {
            while (angle < 0f)
                angle += 360f;
            const float OFFSET = 45f / 2 - float.Epsilon;
            float direction = (((angle + OFFSET) % 360f) / 360f * (int)Direction.Northwest);
            return direction < 180f ? RoundUpToDirection(direction) : RoundDownToDirection(direction);
        }
        private static Direction RoundUpToDirection(float direction) =>
            (Direction)Mathf.CeilToInt(direction - 0.5f);
        private static Direction RoundDownToDirection(float direction) =>
            (Direction)Mathf.FloorToInt(direction + 0.5f);
    }
}
