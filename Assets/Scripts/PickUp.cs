using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;

    private void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            for (var i = 0; i < inventory.Slots.Length; i++)
            {
                if (inventory.IsSlotFull[i] == false)
                {
                    inventory.IsSlotFull[i] = true;
                    Instantiate(slotButton, inventory.Slots[i].transform);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
