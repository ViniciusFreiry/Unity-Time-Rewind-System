using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public Transform cameraTransform;
    public float cameraMoveSpeed = 5f;

    private PlayerResetController player;
    private Transform currentSpawn;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerResetController>();
        currentSpawn = player.spawnPoint.parent;
    }

    private void Update()
    {
        if (cameraTransform != null && currentSpawn != null)
        {
            Vector3 targetPos = new Vector3(currentSpawn.position.x, currentSpawn.position.y, cameraTransform.position.z);
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPos, cameraMoveSpeed * Time.deltaTime);
        }
    }

    public void ChangeRoom(Transform newSpawn)
    {
        currentSpawn = newSpawn.parent;
        if (player != null)
        {
            player.spawnPoint = newSpawn;
        }
    }
}