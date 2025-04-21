using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory Inventory;
    public GameObject slotButton;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.Q))
        {
            for (var i = 0; i < Inventory.Slots.Length; i++)
            {
                if (Inventory.isSlotFull[i])
                    continue;
                Inventory.isSlotFull[i] = true;
                Instantiate(slotButton, Inventory.Slots[i].transform);
                Destroy(gameObject);
                break;
            }
        }
    }
}
