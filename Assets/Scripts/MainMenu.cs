using UnityEngine;

[ExecuteInEditMode]
public class MainMenu : MonoBehaviour
{
    bool saveAvailable;
    string lastLocation;
    void Start()
    {
        saveAvailable = GameState.SaveAvailable;
        lastLocation = PlayerPrefs.GetString("CurrentLocation", "Home");
    }

    void OnGUI()
    {

        GUILayout.BeginArea(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 100, 200, 200));
        if (GUILayout.Button("New Game"))
        {
            NavigationManager.NavigateTo("Home");
        }
        GUILayout.Space(50);
        if (saveAvailable)
        {
            if (GUILayout.Button("Load Game"))
            {
                GameState.LoadState(() =>
                    {
                        NavigationManager.NavigateTo(lastLocation);
                    });
            }
        }
        GUILayout.EndArea();
    }
}
