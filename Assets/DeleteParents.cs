using UnityEngine;

public class DeactivateParents : MonoBehaviour
{
    [Header("Настройки")]
    public float interactDistance = 3f; // Дистанция для взаимодействия
    public KeyCode interactKey = KeyCode.E; // Клавиша активации
    public int requiredPresses = 22; // Нужное количество нажатий

    [Header("Ссылки")]
    public Transform player; // Перетащи сюда игрока
    public GameObject Mum;   // Перетащи объект "Mom"
    public GameObject Dad;   // Перетащи объект "Dad"

    private int ePressCount = 0;
    private bool isInRange = false;


    void Update()
    {
        if (player == null || Mum == null || Dad == null)
        {
            Debug.LogError("Не назначены player, mom или dad!");
            return;
        }

        // Проверяем дистанцию до игрока
        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance <= interactDistance;

        // Если игрок в радиусе и нажал E
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            ePressCount++;
            Debug.Log($"Нажатий: {ePressCount}/{requiredPresses}");

            if (ePressCount >= requiredPresses)
            {
                DeactivateParentsObjects();
            }
        }
    }

    void DeactivateParentsObjects()
    {
        if (Mum != null) Mum.SetActive(false);
        if (Dad != null) Dad.SetActive(false);
        Debug.Log("Родители деактивированы!");
        enabled = false; // Отключаем скрипт
    }

    // Опционально: визуализация радиуса в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}