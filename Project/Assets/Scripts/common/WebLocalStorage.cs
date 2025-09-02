
using UnityEngine;
using System.Runtime.InteropServices;

public class WebLocalStorage {
    // Importa as funções JavaScript
    [DllImport("__Internal")]
    private static extern void SaveToLocalStorage(string key, string value);

    [DllImport("__Internal")]
    private static extern string LoadFromLocalStorage(string key);

    public static void SaveData(string key, string data)
    {
        // Verifica se a plataforma é WebGL antes de chamar a função
#if UNITY_WEBGL
        SaveToLocalStorage(key, data);
#else
        Debug.LogWarning("localStorage só está disponível em Web.");
#endif
    }

    public static string LoadData(string key)
    {
#if UNITY_WEBGL
        return LoadFromLocalStorage(key);
#else
        return "";
#endif
    }
}