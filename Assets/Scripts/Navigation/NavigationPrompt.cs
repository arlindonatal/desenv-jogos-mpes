using UnityEngine;
using System.Collections;

public class NavigationPrompt : MonoBehaviour {

    bool showDialog;

    void OnCollisionEnter2D(Collision2D col)
    {
        //Only allow the player to travel if allowed
        if (NavigationManager.CanNavigate(this.tag))
        {
            DialogVisible(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Only allow the player to travel if allowed
        if (NavigationManager.CanNavigate(this.tag))
        {
            DialogVisible(true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        DialogVisible(false);
        MessagingManager.Instance.BroadcastUIEvent(showDialog);
    }

    void OnGUI()
    {
        if (showDialog)
        {
            //layout start
            GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 50, 300, 250));

            //the menu background box
            GUI.Box(new Rect(0, 0, 300, 250), "");

            //Dialog detail - updated to get better detail
            GUI.Label(new Rect(15, 10, 300, 68), "Do you want to travel to " + NavigationManager.GetRouteInfo(this.tag) + "?");

            //Player wants to leave this location
            if (GUI.Button(new Rect(55, 100, 180, 40), "Travel"))
            {
                DialogVisible(false);
                NavigationManager.NavigateTo(this.tag);
            }

            //Player wants to stay at this location
            if (GUI.Button(new Rect(55, 150, 180, 40), "Stay"))
            {
                DialogVisible(false);
            }

            //layout end
            GUI.EndGroup(); 
        }
    }

    void DialogVisible(bool visibility)
    {
        showDialog = visibility;
        MessagingManager.Instance.BroadcastUIEvent(visibility);
    }
}
