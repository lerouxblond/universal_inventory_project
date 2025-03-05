using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUi : MonoBehaviour
{
    [SerializeField] public Image itemSprite;
    [SerializeField] public Image backgroundImage;
    [SerializeField] private bool isEmpty = true;

    public void resetSlot()
    {
        itemSprite.enabled = false;
        backgroundImage.enabled = true;
        isEmpty = true;
    }

    public void updateSlotUI(Sprite itemImage)
    {
        itemSprite.sprite = itemImage;
        itemSprite.enabled = true;
        backgroundImage.enabled = false;
        isEmpty = false;
    }
}
