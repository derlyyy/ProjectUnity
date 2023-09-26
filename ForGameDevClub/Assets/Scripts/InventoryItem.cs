using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private InventoryManager inventoryManager;

    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;

    public GameObject deleteButtonPrefab;
    private GameObject deleteButton;
    

    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        
        deleteButton = Instantiate(deleteButtonPrefab, transform);
        deleteButton.SetActive(false); // Изначально скрываем кнопку

        Button deleteButtonComponent = deleteButton.GetComponent<Button>();
        deleteButtonComponent.onClick.AddListener(OnDeleteButtonClick);
    }

    public void InitialiseItem(Item newitem)
    {
        item = newitem;
        image.sprite = newitem.img;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        deleteButton.SetActive(!deleteButton.activeSelf);
    }
    
    void OnDeleteButtonClick()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryManager.DeleteItem();
        Destroy(gameObject);
    }
}
