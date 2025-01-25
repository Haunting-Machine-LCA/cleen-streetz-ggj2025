using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    public abstract class GridEntity : MonoBehaviour
    {
        protected virtual void Start()
        {
            BattleSystem.GetSingleton().RegisterGameObject(gameObject);
        }


        protected virtual void OnDestroy()
        {
            BattleSystem.GetSingleton().UnregisterGameObject(gameObject);
        }
    }
}
