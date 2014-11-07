using UnityEngine;

public class BuyButton : MonoBehaviour {

    void OnMouseDown()
    {
        ShopManager.PurchaseSelectedItem();
    }
}
