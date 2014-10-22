#define MyDirective
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public static class GameState {

    static string saveFilePath = Application.persistentDataPath + "/playerstate.dat";

    public static Player currentPlayer = ScriptableObject.CreateInstance<Player>();
    public static bool playerReturningHome;
    public static Dictionary<string, Vector3> LastScenePositions = new Dictionary<string, Vector3>();

    public static Vector3 GetLastScenePosition(string sceneName)
    {
        if (GameState.LastScenePositions.ContainsKey(sceneName))
        {
            var lastPos = GameState.LastScenePositions[sceneName];
            return lastPos;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public static void SetLastScenePosition(string sceneName, Vector3 position)
    {
        if (GameState.LastScenePositions.ContainsKey(sceneName))
        {
            GameState.LastScenePositions[sceneName] = position;
        }
        else
        {
            GameState.LastScenePositions.Add(sceneName, position);
        }
    }

    public static void SaveState()
    {
        try
        {
            PlayerPrefs.SetString("CurrentLocation", Application.loadedLevelName);
            var playerSerializedState = SerializerHelper.Serialise<PlayerSaveState>(currentPlayer.GetPlayerSaveState());
#if UNITY_METRO
            UnityEngine.Windows.File.WriteAllBytes(saveFilePath, playerSerializedState);
#else
            using (var file = File.Create(saveFilePath))
            {
                file.Write(playerSerializedState, 0, playerSerializedState.Length);
            }
#endif
        }
        catch
        {
            Debug.LogError("Saving data failed");
        }
    }

    public static bool SaveAvailable
    {
#if UNITY_METRO
        get { return UnityEngine.Windows.File.Exists(saveFilePath); }
#else
        get { return File.Exists(saveFilePath); }
#endif
    }

    public static void LoadState(Action LoadComplete)
    {
        PlayerSaveState LoadedPlayer;
        try
        {
            if (SaveAvailable)
            {
#if UNITY_METRO
                var playerSerializedState = UnityEngine.Windows.File.ReadAllBytes(saveFilePath);
                LoadedPlayer = SerializerHelper.DeSerialise<PlayerSaveState>(playerSerializedState);
#else
                //Get the file
                using (var stream = File.Open(saveFilePath, FileMode.Open))
                {
                    LoadedPlayer = SerializerHelper.DeSerialise<PlayerSaveState>(stream);
                }
#endif
                currentPlayer = LoadedPlayer.LoadPlayerSaveState(currentPlayer);
            }
        }
        catch
        {
            Debug.LogError("Loading data failed, file is corrupt");
        }
        LoadComplete();
    }

}

