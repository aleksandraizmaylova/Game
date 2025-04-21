using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemTextDisplay : MonoBehaviour
{
    public GameObject displayText;
    private void Start()
    {
        displayText.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(true);
            var textPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.5f + Vector3.right);
            displayText.transform.position = textPosition;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            displayText.gameObject.SetActive(false);
    }
}