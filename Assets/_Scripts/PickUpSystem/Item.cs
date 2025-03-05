using Inventory.Model;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO inventoryItem { get; private set; }

    [field: SerializeField]
    public int quantity { get; set; } = 1;

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = inventoryItem.itemImage;
    }

    public void destroyItem()
    {
        GetComponentInChildren<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
