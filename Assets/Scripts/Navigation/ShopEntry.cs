using UnityEngine;

public class ShopEntry : MonoBehaviour {

    bool canEnterShop;

    void OnTriggerEnter2D(Collider2D col)
    {
        DialogVisible(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        DialogVisible(false);
    }

    void Update()
    {
        if (canEnterShop && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (NavigationManager.CanNavigate(this.tag))
            {
                NavigationManager.NavigateTo(this.tag);
            }
        }
    }

    void OnGUI()
    {
        if (canEnterShop)
        {
            //layout start
            GUI.BeginGroup(
                new Rect(
                    Screen.width / 2 - 150, 
                    50, 
                    300, 
                    50));

            //the menu background box
            GUI.Box(new Rect(0, 0, 300, 250), "");

            //Dialog detail - updated to get better detail
            GUI.Label(
                new Rect(15, 10, 300, 68), 
                "Do you want to Enter the Shop?    (Press up)");

            //layout end
            GUI.EndGroup();
        }
    }

    void DialogVisible(bool visibility)
    {
        canEnterShop = visibility;
        MessagingManager.Instance.BroadcastUIEvent(visibility);
    }
}
