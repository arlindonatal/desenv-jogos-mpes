using UnityEngine;

public class ShopSlot : MonoBehaviour
{

    public InventoryItem Item;
    public ShopManager Manager;

    void OnMouseDown()
    {
        if (Item != null)
        {
            Manager.SetShopSelectedItem(this);
        }
    }

    public void AddShopItem(InventoryItem item)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.Sprite;
        spriteRenderer.transform.localScale = item.Scale;
        Item = item;
    }

    public void PurchaseItem()
    {
        GameState.currentPlayer.AddinventoryItem(Item);
        Item = null;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
        Manager.ClearSelectedItem();
    }

}
