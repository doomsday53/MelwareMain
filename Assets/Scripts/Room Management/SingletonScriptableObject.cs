using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if (results.Length == 0)
                {
                    //nothing found
                    return null;
                }

                if (results.Length > 1)
                {
                    // We have too many
                    return null;
                }
                instance = results[0];
                instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }

            return instance;
        }
    }
}
