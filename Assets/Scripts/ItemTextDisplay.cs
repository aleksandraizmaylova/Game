using UnityEngine;
using UnityEngine.UI;

public class ItemTextDisplay : MonoBehaviour
{
    public GameObject displayText;
    public string text;
    private Text textComponent;
    
    private void Start()
    {
        displayText.gameObject.SetActive(false);
        textComponent = displayText.GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textComponent.text = text;
            displayText.gameObject.SetActive(true);
            var textPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up + Vector3.right);
            displayText.transform.position = textPosition;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            displayText.gameObject.SetActive(false);
    }
}