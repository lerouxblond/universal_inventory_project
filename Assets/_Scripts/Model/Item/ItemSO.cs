using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool isStackable { get; set; }
            
        public int ID => GetInstanceID();
        [field: SerializeField]
        public int MaxStackSize {get; set;} = 1;
        [field: SerializeField]
        public string itemName {get; set;}
        [field: SerializeField]
        [field: TextArea]
        public string itemDescription { get; set; }
        [field: SerializeField] 
        public Sprite itemImage {get; set;}
        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList {get; set;}
    }

    [Serializable]
    public struct ItemParameter: IEquatable<ItemParameter>
    {
        public ItemParametersSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}
