using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using Inventory.SO;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private EquipedItemSO equipedItemList;
    [SerializeField] private InventorySO inventoryData;
    // [SerializeField] private InventoryController inventoryController;
    [SerializeField] private List<ItemParameter> parametersToModify, itemCurrentState;
    [SerializeField] private playerEquipmentUI playerEquipmentUI;

    void Start()
    {
        // inventoryController.GetComponentInParent<InventoryController>();
        // playerEquipmentUI = 
    }

    public void setWeapon(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if(equipedItemList.weapon != null)
        {
            inventoryData.addItem(equipedItemList.weapon, 1, itemCurrentState);
            playerEquipmentUI.updateUI(equipedItemList.weapon);
        }
        this.equipedItemList.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        this.playerEquipmentUI.updateUI(equipedItemList.weapon);
        modifyParameters();
    }

        public void setShield(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if(equipedItemList.shield != null)
        {
            inventoryData.addItem(equipedItemList.shield, 1, itemCurrentState);
            playerEquipmentUI.updateUI(equipedItemList.shield);
        }
        this.equipedItemList.shield = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        this.playerEquipmentUI.updateUI(equipedItemList.shield);
        modifyParameters();
    }

        public void setHelmet(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if(equipedItemList.helmet != null)
        {
            inventoryData.addItem(equipedItemList.helmet, 1, itemCurrentState);
            playerEquipmentUI.updateUI(equipedItemList.helmet);
        }
        this.equipedItemList.helmet = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        this.playerEquipmentUI.updateUI(equipedItemList.helmet);
        modifyParameters();
    }

        public void setChestplate(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if(equipedItemList.chestplate != null)
        {
            inventoryData.addItem(equipedItemList.chestplate, 1, itemCurrentState);
            playerEquipmentUI.updateUI(equipedItemList.chestplate);
        }
        this.equipedItemList.chestplate = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        this.playerEquipmentUI.updateUI(equipedItemList.chestplate);
        modifyParameters();
    }

        public void setBelt(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if(equipedItemList.belt != null)
        {
            inventoryData.addItem(equipedItemList.belt, 1, itemCurrentState);
            playerEquipmentUI.updateUI(equipedItemList.belt);
        }
        this.equipedItemList.belt = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        this.playerEquipmentUI.updateUI(equipedItemList.belt);
        modifyParameters();
    }

        public void setBoots(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if(equipedItemList.boots != null)
        {
            inventoryData.addItem(equipedItemList.boots, 1, itemCurrentState);
            playerEquipmentUI.updateUI(equipedItemList.boots);
        }
        this.equipedItemList.boots = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        this.playerEquipmentUI.updateUI(equipedItemList.boots);
        modifyParameters();
    }


    private void modifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if(itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }
}
