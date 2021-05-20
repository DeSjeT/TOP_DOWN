using System.IO;
using UnityEngine;
    public class LoadJSON : MonoBehaviour
    {
        static public T LoadingJSON<T>()
        {
            if (File.Exists(JsonScript.Path))
            {
                return JsonUtility.FromJson<T>(File.ReadAllText(JsonScript.Path));
            }
            else
            {
                return default(T);
            }
        }
    }