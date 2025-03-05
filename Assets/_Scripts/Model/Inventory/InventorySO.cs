using System;
using UnityEngine;
using System.Collections.Generic;
using Inventory.Model;
using System.Linq;

namespace Inventory.SO
{
    [CreateAssetMenu(fileName = "InventorySO", menuName = "Scriptable Objects/InventorySO")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField]
        public inventoryHolderDataCreation holderCreationData { get; set; }
        [field: SerializeField]
        public int inventorySize { get; set; }
        [field: SerializeField] public bool isInit { get; set; }

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        
        public void getInventorysize()
        {
            inventorySize = holderCreationData.slotsCount;
        }

        public void initInventory()
        {
            getInventorysize();
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < inventorySize; i++)
            {
                inventoryItems.Add(InventoryItem.getEmptyItem());
            }
            isInit = true;
        }

        public Dictionary<int, InventoryItem> getCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i].isEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public int addItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if(item.isStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while(quantity > 0 && isInventoryFull() == false)
                    {
                       quantity -= addItemToFirstFreeSlot(item, 1, itemState);
                    }
                    
                    informAboutChange();
                    return quantity;
                }
            }
            quantity = addStackableItem(item, quantity);
            informAboutChange();
            return quantity;
        }

        //public int addItem(ItemSO item, int quantity)
        //{
        //    if (!item.isStackable)
        //    {
        //        for (int i = 0; i < inventoryItems.Count; i++)
        //        {
        //            while (quantity > 0 && !isInventoryFull())
        //            {
        //                quantity -= addItemToFirstFreeSlot(item, 1);
        //            }
        //        }
        //        informAboutChange();
        //    }
        //    else
        //    {
        //        quantity = addStackableItem(item, quantity);
        //    }

        //    informAboutChange();
        //    return quantity;
        //}


        private int addItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i].isEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private int addStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i].isEmpty)
                    continue;
                if(inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if(quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].changeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].changeQuantity(inventoryItems[i].quantity + quantity);
                        informAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && isInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                addItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        private bool isInventoryFull()
            => inventoryItems.Where(item => item.isEmpty).Any() == false;

        public InventoryItem getItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void addItem(InventoryItem item)
        {
            addItem(item.item, item.quantity);
        }

        public void swapItem(int itemDragged, int itemSwapped)
        {
            InventoryItem item1 = inventoryItems[itemDragged];
            inventoryItems[itemDragged] = inventoryItems[itemSwapped];
            inventoryItems[itemSwapped] = item1;
            informAboutChange();
        }

        private void informAboutChange()
        {
            OnInventoryUpdated?.Invoke(getCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if(inventoryItems.Count > itemIndex)
            {
                if(inventoryItems[itemIndex].isEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if(reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.getEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].changeQuantity(reminder);
                
                informAboutChange();
            }
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool isEmpty => item == null;

        public InventoryItem changeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState),
            };
        }

        public static InventoryItem getEmptyItem() => new InventoryItem
        {
            item = null,
            quantity = 0,
            itemState = new List<ItemParameter>()
        };
    }    
}

