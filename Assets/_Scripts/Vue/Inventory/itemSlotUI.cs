using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class ItemSlotsUI : MonoBehaviour, 
    IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        public bool emptySlot = true;

        [SerializeField] private Image border;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text quantityText;

        public event Action<ItemSlotsUI> OnitemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;


        public void Awake()
        {
            resetData();
            deselect();
        }

        public void resetData()
        {
            this.itemIcon.gameObject.SetActive(false);
            emptySlot = true;
        }

        public void deselect()
        {
            border.enabled = false;
        }

        public void select()
        {
            border.enabled = true;
        }

        public void setData(Sprite itemSprite, int quantity)
        {
            this.itemIcon.gameObject.SetActive(true);
            this.itemIcon.sprite = itemSprite;
            if(quantity > 1)
                this.quantityText.text = quantity.ToString();
            else
                this.quantityText.text = "";
            emptySlot = false;
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(emptySlot)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnitemClicked?.Invoke(this);
            }

        }

        public void OnDrag(PointerEventData eventData) {}
    }

}