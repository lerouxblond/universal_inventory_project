using System;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class playerEquipmentUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private List<EquipmentSlotUi> equipmentSlotList;

    void Start()
    {
        getSlots();
    }
    public void getSlots()
    {
        equipmentSlotList = new List<EquipmentSlotUi>(6); 
        equipmentSlotList.AddRange(content.GetComponentsInChildren<EquipmentSlotUi>());
    }
    
    public void updateUI(EquipableItemSO weapon)
    {
        EquipmentSlotUi concernedSlot = findConcernedSlot((int)weapon.equipmentType);
        concernedSlot.updateSlotUI(weapon.itemImage);
    }

    private EquipmentSlotUi findConcernedSlot(int equipmentType)
    {
        return equipmentSlotList[equipmentType];
    }

    internal void resetUI()
    {
        foreach (EquipmentSlotUi slotUi in equipmentSlotList)
        {
            slotUi.resetSlot();
        }
    }
}
