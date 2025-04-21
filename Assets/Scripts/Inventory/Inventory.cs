using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isSlotFull;
    public int selectedSlot;
    public GameObject[] Slots;
    public GameObject openedBP;
    public GameObject closedBP;
    private bool InventoryOpen;

    private void Start()
    {
        selectedSlot = 0;
        InventoryOpen = false;
        openedBP.SetActive(false);
    }

    private void Update()
    {
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
                Slots[selectedSlot].GetComponent<Slot>().Unselect();
                selectedSlot = 0;
                Slots[selectedSlot].GetComponent<Slot>().Select();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Slots[selectedSlot].GetComponent<Slot>().Unselect();
                selectedSlot = 1;
                Slots[selectedSlot].GetComponent<Slot>().Select();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Slots[selectedSlot].GetComponent<Slot>().Unselect();
                selectedSlot = 2;
                Slots[selectedSlot].GetComponent<Slot>().Select();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Slots[selectedSlot].GetComponent<Slot>().Unselect();
                selectedSlot = 3;
                Slots[selectedSlot].GetComponent<Slot>().Select();
            }
        }
        
    }
}
