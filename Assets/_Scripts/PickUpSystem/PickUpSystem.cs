using System;
using Inventory.SO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private Item itemCollided;
    private InputSystem_Actions inputActions;
    // [SerializeField] private Collider2D playerCollider;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        // playerCollider = GetComponentInChildren<Collider2D>();
    }

    private void OnEnable()
    {
        inputActions.Player.Pickup.performed += PickUpItem;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Pickup.performed -= PickUpItem;
        inputActions.Player.Disable();
    }

    private void PickUpItem(InputAction.CallbackContext context)
    {
        if(itemCollided != null)
        {
            int reminder = inventoryData.addItem(itemCollided.inventoryItem, itemCollided.quantity);
            if(reminder == 0)
                itemCollided.destroyItem();
            else
                itemCollided.quantity = reminder;

            itemCollided = null;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if(item != null)
        {
            itemCollided = item;
            Debug.Log("Item in range: "+ item.inventoryItem.itemName + item.quantity );
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if(item != null && item == itemCollided)
        {
            itemCollided = null;
            Debug.Log("Item out of range: "+ item.inventoryItem.itemName + item.quantity );
        }
    }
}
