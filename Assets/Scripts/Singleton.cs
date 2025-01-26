using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Optional;


namespace Hmlca.Untitled
{
    public class Singleton : MonoBehaviour
    {
        protected static Dictionary<Type, Singleton> cache = new Dictionary<Type, Singleton>();
        protected static Option<T> GetSingleton<T>() where T : Singleton
        {
            if (!cache.TryGetValue(typeof(T), out Singleton instance))
            {
                instance = FindObjectOfType<T>(true);
                cache[typeof(T)] = instance;
            }
            if (instance == null)
                return Option.None<T>();
            return Option.Some(instance as T);
        }
    }


    public class Singleton<T> : Singleton where T : Singleton
    {
        protected virtual void Awake()
        {
            if (!cache.TryGetValue(typeof(T), out Singleton instance))
                cache[typeof(T)] = this;
        }


        public static T GetSingleton(bool supressFail = false)
        {
            Option<T> op = GetSingleton<T>();
            if (op.HasValue)
            {
                T instance = op.ValueOr(() => { return null; });
                print($"got singleton of {typeof(T).Name}: {instance?.name}");
                return instance;
            }
            else
            {
                if (!supressFail)
                    print($"failed to find singleton of {typeof(T).Name}");
                return null;
            }
        }


        protected virtual void OnDestroy()
        {
            cache.Remove(typeof(T));
        }
    }
}
