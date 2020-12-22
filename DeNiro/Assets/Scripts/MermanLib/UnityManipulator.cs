using System.Collections.Generic;
using UnityEngine;

namespace MermanLib
{
    public class UnityManipulator : MonoBehaviour
    {
        public static void DestroyAndClearList<T>(ref List<T> list)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                Destroy((list[i] as MonoBehaviour)?.gameObject);
            }

            list.Clear();
        }
    }
}