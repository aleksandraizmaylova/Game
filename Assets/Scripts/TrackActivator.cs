using System;
using UnityEngine;

public class TrackActivator : MonoBehaviour
{
    public Track[] Tracks;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            foreach (var track in Tracks)
                track.StartMoving();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            foreach (var track in Tracks)
                Destroy(track);
    }
}
