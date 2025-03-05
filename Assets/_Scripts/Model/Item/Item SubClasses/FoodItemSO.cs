using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "FoodItemSO", menuName = "Scriptable Objects/ItemSO/FoodItemSO")]
    public class FoodItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<modifierData> modifiersData = new List<modifierData>();
        public string ActionName => "Consume";

        public bool performAction(GameObject character, List<ItemParameter> itemState)
        {
            foreach (modifierData data in modifiersData)
            {
                data.statsModifier.affectCharacter(character, data.value);
            }
            return true;
        }
        
        // void Awake()
        // {
        //     itemClass = "Food, Potion & Ingredient";
        // }
    }

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string ActionName { get;}
        
        // public AudioClip actionSFX {get; private set;}

        bool performAction(GameObject character, List<ItemParameter> itemState);
    }

    [Serializable]
    public class modifierData
    {
        public itemPlayersStatsModifierSO statsModifier;
        public float value;
    }
}
