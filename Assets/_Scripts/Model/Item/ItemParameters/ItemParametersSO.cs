using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "ItemParametersSO", menuName = "Scriptable Objects/ItemSO/ParametersSO/ItemParametersSO")]
    public class ItemParametersSO : ScriptableObject
    {
        [field: SerializeField]
        public string ParameterName { get; private set; }
    }

}