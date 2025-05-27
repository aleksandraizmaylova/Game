using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory Inventory;
    private BackPack backPack;
    public GameObject slotButton;
    public bool isPicked = false;
    private bool isPlayerNear;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        backPack = Inventory.openedBP.GetComponent<BackPack>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && isPlayerNear)
            PickObject();
    }

    private void PickObject()
    {
        for (var i = 0; i < backPack.Slots.Length; i++)
        {
            if (Inventory.isSlotFull[i])
                continue;
            Inventory.isSlotFull[i] = true;
            Instantiate(slotButton, backPack.Slots[i].transform);
            Destroy(gameObject);
            isPicked = true;
            break;
        }
    }
}