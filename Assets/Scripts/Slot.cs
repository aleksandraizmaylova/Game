using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject selection;
    public int slotNumber;
    private Inventory Inventory;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if (slotNumber != 0)
            selection.SetActive(false);
    }

    private void Update()
    {
        if (transform.childCount <= 0)
            Inventory.isSlotFull[slotNumber] = false;

        if (Input.GetKeyDown(KeyCode.E) && Inventory.selectedSlot == slotNumber)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Spawn>().SpawnDroppedItem();
                Destroy(child.gameObject);
            }
        }
    }

    public void Select() => selection.SetActive(true);
    public void Unselect() => selection.SetActive(false);
}
