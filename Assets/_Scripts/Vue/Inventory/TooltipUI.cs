using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class TooltipUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemDescription;
        [SerializeField] private Image itemIcon;

        public void Awake()
        {
            resetInfoPanel();
        }

        public void resetInfoPanel()
        {
            this.itemName.text = "";
            this.itemDescription.text = "";
            this.itemIcon.gameObject.SetActive(false);
        }

        public void setDescription(Sprite itemSprite, string itemName, string itemDescription)
        {
            this.itemIcon.gameObject.SetActive(true);
            this.itemIcon.sprite = itemSprite;
            this.itemName.text = itemName;
            this.itemDescription.text = itemDescription;
        }
    }
    
}
