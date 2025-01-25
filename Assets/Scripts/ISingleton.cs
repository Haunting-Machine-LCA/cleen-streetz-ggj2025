using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled.App
{
    public interface ISingleton<T> where T : ISingleton<T>
    {
        T GetSingleton();
    }
}
