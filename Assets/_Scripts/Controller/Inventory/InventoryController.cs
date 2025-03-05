using UnityEngine;
using Player.Model;
using Inventory.UI; 
using Inventory.Model;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Inventory.SO;
using System;
using Player;
using System.Text;
using Unity.VisualScripting;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryUI inventoryUIPrefab;
        [SerializeField] private Transform inventoryPanelParent;
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private InventorySO inventoryData;
        [SerializeField] private ItemSlotsUI slotsUI;
        [SerializeField] private PlayerSO playerData;
        private InputSystem_Actions inputActions;
        [SerializeField] private InputController inputController;

        // public List<InventoryItem> debugInitialItems = new List<InventoryItem>();
        
        
        public void Awake()
        {
            InstantiateInventoryUI();
            prepareInventoryData();
            inputActions = new InputSystem_Actions();
        }

        private void prepareInventoryData()
        {
            if (!inventoryData.isInit)
                inventoryData.initInventory();
            inventoryData.OnInventoryUpdated += UpdateUI;
        }

        private void PrepareUI()
        {
            inventoryUI.InitInventoryUI(playerData.inventoryDataCreator.slotsCount);
            inventoryUI.setWeight(playerData.inventoryDataCreator.maxWeight, playerData.inventoryDataCreator.currentWeight);
            inventoryUI.setGold(playerData.inventoryDataCreator.gold);

            this.inventoryUI.OndescriptionRequested += HandleDescriptionRequest;
            this.inventoryUI.OnSwapItems += HandleSwapItems;
            this.inventoryUI.OnStartDragging += HandleDragging;
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        public void EnableUIControls() => inputActions.UI.Enable();
        public void DisableUIControls() => inputActions.UI.Disable();

        private void InstantiateInventoryUI()
        {
            if(inventoryUIPrefab != null)
                inventoryUI = Instantiate(inventoryUIPrefab, inventoryPanelParent);
            PrepareUI();
        }

        private void UpdateUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.resetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.itemImage, item.Value.quantity);
            }
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.getItemAt(itemIndex);
            if(inventoryItem.isEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if(destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }
            
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if(itemAction != null)
            {
                itemAction.performAction(gameObject, inventoryItem.itemState);
            }
            
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.getItemAt(itemIndex);
            if(inventoryItem.isEmpty)
                return;
            inventoryUI.createDraggedItem(inventoryItem.item.itemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemDragged, int itemSwapped)
        {
            inventoryData.swapItem(itemDragged, itemSwapped);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.getItemAt(itemIndex);
            if(inventoryItem.isEmpty)
            {
                inventoryUI.resetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = prepareDescription(inventoryItem);
            inventoryUI.updateDescription(itemIndex, item.itemImage, item.itemName, description);
        }

        public string prepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.itemDescription);
            sb.AppendLine();
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName}" + $": {inventoryItem.itemState[i].value} /" + $" {inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        // public void Update()
        // {
        //     if(inventoryUI.isActiveAndEnabled)
        //     {
        //         foreach (var item in inventoryData.getCurrentInventoryState())
        //         {
        //             inventoryUI.UpdateData(item.Key, item.Value.item.itemImage, item.Value.quantity);
        //         }
        //     }
        // }

        void OnEnable()
        {
            inputActions.UI.Inventory.performed += OnInventoryToggle;
            inputActions.UI.Click.performed += LeftButtonClicked;
            inputActions.UI.RightClick.performed += RightButtonClicked; 
            EnableUIControls();
        }

        private void OnDisable()
        {
            inputActions.UI.Inventory.performed -= OnInventoryToggle;
            inputActions.UI.Click.performed -= LeftButtonClicked;
            inputActions.UI.RightClick.performed -= RightButtonClicked;
            EnableUIControls();
        }

        public void OnInventoryToggle(InputAction.CallbackContext context)
        {
            inventoryUI.isInventoryOpen = inventoryUI.isInventoryOpen? false : true;
            foreach (var item in inventoryData.getCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.itemImage, item.Value.quantity);
                }
            handleActionMaps(inventoryUI.isInventoryOpen);
            inventoryUI.ShowAndHide();
        }

        private void handleActionMaps(bool isInventoryOpen)
        {
            if (isInventoryOpen)
            {
                inputController.DisablePlayerControls(); // Disable player movement
                inputActions.Player.Disable(); // Disable Player Map
                inputActions.UI.Enable(); // Enable UI Map
            }
            else
            {
                inputController.EnablePlayerControls(); // Enable player movement
                inputActions.UI.Disable(); // Disable UI Map
                inputActions.Player.Enable(); // Re-enable Player Map
            }
        }

        private void RightButtonClicked(InputAction.CallbackContext context)
        {
            
            // slotsUI.OnPointerClick();
        }

        private void LeftButtonClicked(InputAction.CallbackContext context)
        {
            // slotsUI.OnPointerClick();
        }

    }
}
