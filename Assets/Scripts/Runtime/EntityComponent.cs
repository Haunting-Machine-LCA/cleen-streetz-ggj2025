using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS
{
    public abstract class EntityComponent : MonoBehaviour
    {
        [Header("EntityComponent Cache", order = int.MaxValue)]
        [SerializeField] protected GameObject root;


        protected virtual void Awake()
        {
            if (!root)
            {
                if (TryGetComponent(out BattleCharacter entityRoot))
                {
                    root = entityRoot.gameObject;
                }
                else
                {
                    Debug.LogWarning($"EntityComponent {name} is missing a reference to its root EntityRoot.");
                    Destroy(this);
                    return;
                }
            }
        }


        public new void SendMessage(string methodName, object value) =>
            root.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);


        public T GetEntityComponent<T>() where T : EntityComponent =>
            root.GetComponentInChildren<T>();


        public bool TryGetEntityComponent<T>(out T component) where T : EntityComponent
        {
            component = GetEntityComponent<T>();
            return component != null;
        }
    }
}
