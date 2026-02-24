using UnityEngine;

public class TimedButtonTrigger : MonoBehaviour
{
    [SerializeField] private float timerDuration = 3f;
    [SerializeField] private GameObject[] targetsToActivate;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    private bool playerInRange;
    private float timer = 0f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            timer = 0f;

            foreach (GameObject target in targetsToActivate)
            {
                IActivatable activatable = target.GetComponent<IActivatable>();
                if (activatable != null)
                {
                    activatable.Activate(true);
                }
            }

            animator.Play("Button_On");
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= timerDuration)
        {
            foreach (GameObject target in targetsToActivate)
            {
                IActivatable activatable = target.GetComponent<IActivatable>();
                if (activatable != null)
                {
                    activatable.Activate(false);
                }
            }

            animator.Play("Button_Off");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}