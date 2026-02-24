using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Transform newSpawnPoint;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RoomManager.Instance.ChangeRoom(newSpawnPoint);
        }
    }
}