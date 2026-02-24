using System.Runtime.InteropServices;
using UnityEngine;

public class DoorController : MonoBehaviour, IActivatable
{
    private Animator animator;
    // private Collider2D col;
    private bool isOpen = false;
    [SerializeField] private bool reverse = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // col = GetComponent<Collider2D>();

        if (reverse)
        {
            isOpen = true;
            animator.Play("Door_Open");
        }
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        
        if (isOpen && animationState.IsName("Door_Closed"))
        {
            animator.Play("Door_Open");
        }
        else if (!isOpen && animationState.IsName("Door_Open"))
        {
            animator.Play("Door_Closed");
        }
    }

    public void Activate(bool state)
    {
        if (reverse) state = (state ? false : true);
        if (state == isOpen) return;

        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        float currentNormalizedTime = currentState.normalizedTime % 1f;

        if (state) // OPEN
        {
            // col.enabled = false;
            if (currentState.IsName("Door_Closing"))
            {
                if (currentNormalizedTime >= 0.3f) animator.Play("Door_Opening", 0, 1f - currentNormalizedTime);
                else animator.Play("Door_Open");
            }
            else
            {
                animator.Play("Door_Opening", 0, 0f);
            }
        }
        else // CLOSE
        {
            // col.enabled = true;
            if (currentState.IsName("Door_Opening"))
            {
                if (currentNormalizedTime >= 0.3f) animator.Play("Door_Closing", 0, 1f - currentNormalizedTime);
                else animator.Play("Door_Closed");
            }
            else
            {
                animator.Play("Door_Closing", 0, 0f);
            }
        }

        isOpen = state;
    }
}