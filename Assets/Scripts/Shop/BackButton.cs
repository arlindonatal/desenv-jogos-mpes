using UnityEngine;

public class BackButton : MonoBehaviour {

    void OnMouseDown()
    {
        NavigationManager.GoBack();
    }
}
