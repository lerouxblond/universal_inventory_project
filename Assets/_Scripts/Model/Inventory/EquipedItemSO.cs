using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipedItemSO", menuName = "Scriptable Objects/EquipedItemSO")]
public class EquipedItemSO : ScriptableObject
{
    [field: SerializeField] public EquipableItemSO weapon { get; set; }
    [field: SerializeField] public EquipableItemSO shield { get; set; }
    [field: SerializeField] public EquipableItemSO helmet { get; set; }
    [field: SerializeField] public EquipableItemSO chestplate { get; set; }
    [field: SerializeField] public EquipableItemSO belt { get; set; }
    [field: SerializeField] public EquipableItemSO boots { get; set; }

    
}
