using UnityEngine;
using Health.Model;

namespace Player.Model
{
    [CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
    public class PlayerSO : ScriptableObject
    {
        [field: Header("Player Identity")]
        [field: SerializeField]
        public string playerName { get; private set; }
        // private playerClass playerClass {get; private set;}

        [Header("Player Stats")]
            [field: SerializeField]
            private HealthDataConstructorSO healthData { get; set; }
            [field: SerializeField]
            public float attackDamage { get; set; }
            [field: SerializeField]
            public float magicDamage { get; set; }
            [field: SerializeField]
            public int amor { get; set; }
            [field: SerializeField]
            public int magicResistance { get; set; }

            [field: Header("Inventory Data Constructor")]
            [field: SerializeField]
            public inventoryHolderDataCreation inventoryDataCreator { get; set; }
    }

}