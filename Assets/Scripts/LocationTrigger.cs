using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public string locationName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MusicManager.Instance.ChangeMusic(locationName);
        }
    }
}