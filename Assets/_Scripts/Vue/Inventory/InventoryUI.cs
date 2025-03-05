using System.Collections.Generic;
using UnityEngine;
using Player.Model;
using TMPro;
using System;

namespace Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private ItemSlotsUI slotPrefab;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private TooltipUI itemDescriptionPanel;
        [SerializeField] private playerEquipmentUI playerEquipmentUI;
        [SerializeField] private TMP_Text weightTxt;
        [SerializeField] private TMP_Text goldTxt;
        [SerializeField] private MouseFollower mouseFollower;
        List<ItemSlotsUI> listOfUISlots = new List<ItemSlotsUI>();
        public bool isInventoryOpen { get; set; }

        private int currentlyDraggedItem = -1;

        public event Action<int> OndescriptionRequested, OnItemActionRequested,
            OnStartDragging;

        public event Action<int, int> OnSwapItems;

        void Start()
        {
            ShowAndHide();
            mouseFollower.Toggle(false);
            itemDescriptionPanel.resetInfoPanel();
        }

        public void InitInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                ItemSlotsUI slotsUI = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                slotsUI.name += "_"+ i ;
                slotsUI.transform.SetParent(contentPanel, false);
                listOfUISlots.Add(slotsUI);
                slotsUI.OnitemClicked += HandleClickSelection;
                slotsUI.OnItemBeginDrag += HandleBeginDrag;
                slotsUI.OnItemEndDrag += HandleEndDrag;
                slotsUI.OnItemDroppedOn += HandleSwap;
                slotsUI.OnRightMouseBtnClick += HandleShowItemActions;
            }
            
        }

        public void UpdateData(int itemIndex, Sprite itemSprite, int itemQuantity)
        {
            if (listOfUISlots.Count > itemIndex)
            {
                listOfUISlots[itemIndex].setData(itemSprite, itemQuantity);
            }
        }

        private void HandleShowItemActions(ItemSlotsUI item)
        {
            int index = listOfUISlots.IndexOf(item);
            if(index == -1)
            {   
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleSwap(ItemSlotsUI item)
        {
            int index = listOfUISlots.IndexOf(item);
            if(index == -1)
            {   
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItem, index);
        }

        private void resetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItem = -1;
            resetSelection();
        }

        public void createDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleEndDrag(ItemSlotsUI item)
        {
            resetDraggedItem();
        }

        private void HandleBeginDrag(ItemSlotsUI item)
        {
            int index = listOfUISlots.IndexOf(item);
            if(index == -1)
                return;
            currentlyDraggedItem = index;
            HandleClickSelection(item);
            OnStartDragging?.Invoke(index);
        }

        private void HandleClickSelection(ItemSlotsUI item)
        {
            resetSelection();
            int index = listOfUISlots.IndexOf(item);
            if(index == -1)
                return;
            OndescriptionRequested?.Invoke(index);
        }

        public void ShowAndHide()
        {
            Debug.Log(isInventoryOpen);

            gameObject.SetActive(isInventoryOpen);
            resetSelection();
            resetDraggedItem();
        }

        public void resetSelection()
        {
            itemDescriptionPanel.resetInfoPanel();
            deselectAllItems();
        }

        private void deselectAllItems()
        {
            foreach (ItemSlotsUI item in listOfUISlots)
            {
                item.deselect();
            }
        }

        public void setWeight(float maxWeight, float currentWeight)
        {
            weightTxt.text = currentWeight + " / " + maxWeight;
        }

        public void setGold(int gold)
        {
            goldTxt.text = gold + " ";    
        }

        public void setData(Sprite itemSprite, int quantity)
        {

        }

        public void updateDescription(int itemIndex, Sprite itemImage, string itemName, string itemDescription)
        {
            itemDescriptionPanel.setDescription(itemImage, itemName, itemDescription);
            deselectAllItems();
            listOfUISlots[itemIndex].select();
        }

        internal void resetAllItems()
        {
            foreach (var item in listOfUISlots)
            {
                item.resetData();
                item.deselect();
            }
        }
    }
    
}
