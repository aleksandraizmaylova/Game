using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        var playerPos = new Vector2(Player.position.x + 2, Player.position.y);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
