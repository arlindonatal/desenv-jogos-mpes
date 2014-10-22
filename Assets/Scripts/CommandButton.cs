using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class CommandButton : MonoBehaviour
{
	private CommandBar commandBar;
	public InventoryItem Item;
	bool selected;

	public void AddInventoryItem(InventoryItem item)
	{
		this.Item = item;
		var childGO = new GameObject("InventoryItemDisplayImage");
		var renderer = childGO.AddComponent<SpriteRenderer>();
		renderer.sprite = item.Sprite;
		renderer.sortingLayerName = "GUI";
		renderer.sortingOrder = 10;
		renderer.transform.parent = this.transform;
		renderer.transform.localScale *= 4;
	}

	public void Init(CommandBar commandBar)
	{
		this.commandBar = commandBar;
		gameObject.layer = commandBar.Layer;

		var collider = gameObject.GetComponent<BoxCollider2D>();
		collider.size = new Vector2(1f, 1f);

		var renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.sprite = commandBar.DefaultButtonImage;
		renderer.sortingLayerName = "GUI";
		renderer.sortingOrder = 5;
	}

	void OnMouseDown()
	{
		if (commandBar.CanSelectButton)
		{
			selected = !selected;
			UpdateSelection();

			commandBar.Selectbutton(selected ? this : null);
		}
	}

	void UpdateSelection()
	{
		var renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.sprite = selected ? commandBar.SelectedButtonImage : commandBar.DefaultButtonImage;        
	}

	public void ClearSelection()
	{
		selected = false;
		UpdateSelection();
	}

}
