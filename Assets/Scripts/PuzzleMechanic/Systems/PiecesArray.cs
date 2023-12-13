using System;
using UnityEngine;

namespace PuzzleMechanic.Systems
{
    public class PiecesArray
    {
        public static int GetArrayIndex(GameObject obj, object[] array)
        {
            int index = Array.IndexOf(array,obj);
        
            return index;
        }
    }
}