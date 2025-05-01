using UnityEngine;

public class Fragment : MonoBehaviour
{
    private Mirror Mirror;
    public int fragmentNumber;

    private void Start()
    {
        Mirror = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().mirror;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Mirror.ActivateFragment(fragmentNumber);
            Destroy(gameObject);
        }
    }
}
