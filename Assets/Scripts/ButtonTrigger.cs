using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject[] targetsToActivate;
    public KeyCode interactionKey = KeyCode.E;

    private bool playerInRange;
    private bool isOn = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            isOn = !isOn;

            foreach (GameObject target in targetsToActivate)
            {
                IActivatable activatable = target.GetComponent<IActivatable>();
                if (activatable != null)
                {
                    activatable.Activate(isOn);
                }
            }

            animator.Play(isOn ? "Button_On" : "Button_Off");
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