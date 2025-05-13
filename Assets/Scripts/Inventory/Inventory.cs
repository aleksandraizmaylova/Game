using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject openedBP;
    public GameObject closedBP;
    public Mirror mirror;
    public bool[] isSlotFull;
    public int selectedSlot;
    private Slot[] Slots; // review: почему приватные поля с большой буквы?
    private bool InventoryOpen;

    private void Start()
    {
        selectedSlot = 0;
        InventoryOpen = false;
        closedBP.SetActive(true);
        openedBP.SetActive(false);
        Slots = openedBP.GetComponent<BackPack>().Slots;
    }

    private void Update()
    {
        // review: лучше выделять методы, чем оставлять комментарии

        // открытие/закрытие инвентаря
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryOpen = !InventoryOpen;
            openedBP.SetActive(InventoryOpen);
            closedBP.SetActive(!InventoryOpen);
        }
        
        // смена активного слота
        if (InventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeSlot(0);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeSlot(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeSlot(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeSlot(3);
            }
        }
        
    }

    private void ChangeSlot(int newSlot)
    {
        Slots[selectedSlot].Unselect();
        selectedSlot = newSlot;
        Slots[selectedSlot].Select();
    }
}
