using UnityEngine;
using System.IO;

public class SaveJSON : MonoBehaviour
{
    static public void SaveJSONFile(object obj)
    {
        File.WriteAllText(JsonScript.Path, JsonUtility.ToJson(obj));
    }
}
