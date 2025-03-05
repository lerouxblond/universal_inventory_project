using UnityEngine;

[CreateAssetMenu(fileName = "inventoryHolderDataCreation", menuName = "Scriptable Objects/inventoryHolderDataCreation")]
public class inventoryHolderDataCreation : ScriptableObject
{
        [field: Header("Inventory Stats")]
        [field: SerializeField]
        public int slotsCount {get; set;}
        [field: SerializeField]
        public float maxWeight {get; set;} = 150;
        [field: SerializeField]
        public float currentWeight {get; set;}
        [field: SerializeField]
        public int gold {get; set;}
}
