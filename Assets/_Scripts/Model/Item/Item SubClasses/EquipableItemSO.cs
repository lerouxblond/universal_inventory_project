using System;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "EquipableItemSO", menuName = "Scriptable Objects/ItemSO/EquipableItemSO")]
    public class EquipableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public EquipmentType equipmentType;
        public string ActionName => "Equip";

        public bool performAction(GameObject character, List<ItemParameter> itemState = null)
        {
            EquipItem equipSystem = character.GetComponent<EquipItem>();
            if(equipSystem != null)
            {
                if(this.equipmentType == EquipmentType.Sword)
                    equipSystem.setWeapon(this, itemState == null ? DefaultParametersList : itemState);
                if(this.equipmentType == EquipmentType.Shield)
                    equipSystem.setShield(this, itemState == null ? DefaultParametersList : itemState);
                if(this.equipmentType == EquipmentType.Helmet)
                    equipSystem.setHelmet(this, itemState == null ? DefaultParametersList : itemState);
                if(this.equipmentType == EquipmentType.Chestplate)
                    equipSystem.setChestplate(this, itemState == null ? DefaultParametersList : itemState);
                if(this.equipmentType == EquipmentType.Belt)
                    equipSystem.setBelt(this, itemState == null ? DefaultParametersList : itemState);
                if(this.equipmentType == EquipmentType.Boots)
                    equipSystem.setBoots(this, itemState == null ? DefaultParametersList : itemState);
                return true;
            }
            return false;
        }
    }

    [Serializable]
    public enum EquipmentType
    {
        Sword,
        Shield,
        Helmet,
        Chestplate,
        Belt,
        Boots
    }
}