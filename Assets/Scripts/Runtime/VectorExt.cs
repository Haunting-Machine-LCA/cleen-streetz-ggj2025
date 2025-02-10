using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Hmlca.CS
{
    public static class HexVectorExtensions
    {
        public static float GetAngle(this Vector2 vector)
        {
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }


        public static Vector2 WorldToPlanar(this Vector3 world)
        {
            return new Vector2(world.x, world.z);
        }

        public static Vector3 PlanarToWorld(this Vector2 planar, float y = 0f)
        {
            return new Vector3(planar.x, y, planar.y);
        }
    }
}