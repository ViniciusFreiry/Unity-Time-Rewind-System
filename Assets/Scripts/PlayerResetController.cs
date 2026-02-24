using UnityEngine;

public class PlayerResetController : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject deathEffect;
    private TimeBody timeBody;
    private Animator animator;

    private void Start()
    {
        timeBody = GetComponent<TimeBody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Manual Reset
        {
            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {
        StartCoroutine(DeathAndRespawn());
    }

    private System.Collections.IEnumerator DeathAndRespawn()
    {
        // 1. Death
        if (deathEffect) Instantiate(deathEffect, transform.position, Quaternion.identity);

        GetComponent<TimeBody>().ClearRecord();

        // GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        timeBody.enabled = false;
        animator.enabled = false;

        yield return new WaitForSeconds(0.7f); // Delay of death

        // 2. Respawn
        transform.position = spawnPoint.position;
        // GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        timeBody.enabled = true;
        animator.enabled = true;
    }
}