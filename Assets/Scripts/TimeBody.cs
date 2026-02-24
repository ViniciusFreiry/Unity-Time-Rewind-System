using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    private class TimeState
    {
        public Vector3 position;
        public Quaternion rotation;
        public Sprite sprite;
        public Vector3 scale;
        public Vector3 velocity;
    }

    public bool isRewinding = false;
    public float recordTime = 4f; // Record 4 seconds of movement
    public GameObject playerRecord;

    private List<TimeState> timeStates;
    private Rigidbody2D rb;
    private GameObject playback;

    private void Start()
    {
        timeStates = new List<TimeState>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) StartRewind();
        if (Input.GetKeyUp(KeyCode.R)) StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding) Rewind();
        else
        {
            Record();
        }
    }

    private void PlaybackCreate()
    {
        playback = Instantiate(playerRecord, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.01f), transform.rotation);
        playback.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        playback.transform.localScale = transform.localScale;
    }

    private void Record()
    {
        if (!playback) PlaybackCreate();

        if (timeStates.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {          
            timeStates.RemoveAt(timeStates.Count - 1);

            int lastIndex = timeStates.Count - 1;

            playback.transform.position = new Vector3(timeStates[lastIndex].position.x, timeStates[lastIndex].position.y, timeStates[lastIndex].position.z + 0.01f);
            playback.transform.rotation = timeStates[lastIndex].rotation;
            playback.GetComponent<SpriteRenderer>().sprite = timeStates[lastIndex].sprite;
            playback.transform.localScale = timeStates[lastIndex].scale;
        }

        timeStates.Insert(0, new TimeState
        {
            position = transform.position,
            rotation = transform.rotation,
            sprite = GetComponent<SpriteRenderer>().sprite,
            scale = transform.localScale,
            velocity = rb.velocity
        });
    }

    private void Rewind()
    {
        if (timeStates.Count > 0)
        {
            TimeState state = timeStates[0];
            transform.position = state.position;
            transform.rotation = state.rotation;
            GetComponent<SpriteRenderer>().sprite = state.sprite;
            transform.localScale = state.scale;
            rb.velocity = state.velocity;
            timeStates.RemoveAt(0);
        }
        else
        {
            Destroy(playback);
            StopRewind();
        }
    }

    private void StartRewind()
    {
        isRewinding = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().isTrigger = true;

        Animator animator = GetComponent<Animator>();
        if (animator != null) animator.enabled = false;
    }

    private void StopRewind()
    {
        isRewinding = false;
        rb.gravityScale = GetComponent<PlayerMovement>().GetGravityScale();
        GetComponent<Collider2D>().isTrigger = false;

        Animator animator = GetComponent<Animator>();
        if (animator != null) animator.enabled = true;
    }

    public void ClearRecord()
    {
        timeStates.Clear();
        Destroy(playback);
    }
}