using UnityEngine;
using UnityEngine.UI;

public class ItemTextDisplay : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 2f;
    public Text displayText;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= activationDistance)
        {
            displayText.gameObject.SetActive(true);

            Vector3 textPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1f);
            displayText.transform.position = textPosition;
        }
        else
        {
            displayText.gameObject.SetActive(false);
        }
    }
}