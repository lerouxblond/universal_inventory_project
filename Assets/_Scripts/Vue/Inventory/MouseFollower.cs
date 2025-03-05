using UnityEngine;
using Inventory.UI;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private ItemSlotsUI item;

    private InputSystem_Actions inputActions;

    public void Awake()
    {
        canvas = transform.root.GetComponentInChildren<Canvas>();
        item = GetComponentInChildren<ItemSlotsUI>();
        inputActions = new InputSystem_Actions();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.setData(sprite, quantity);
    }

    void OnEnable()
    {
        inputActions.UI.Enable();
    }

    void OnDisable()
    {
        inputActions.UI.Disable();
    }

    void Update()
    {
        Vector2 mousePos = inputActions.UI.Point.ReadValue<Vector2>();
        Vector2 position;
        // Debug.Log(mousePos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            mousePos,
            canvas.worldCamera,
            out position
                );
        transform.position = canvas.transform.TransformPoint(position);
    }
    
    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
