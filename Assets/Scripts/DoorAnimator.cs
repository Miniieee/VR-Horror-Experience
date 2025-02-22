using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private AudioSource audioSource;
    private bool doorOpen = false;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    public void StartAnimation()
    {
        if (doorOpen)
        {
            StartCoroutine(PlayAnimation("DoorClose", closeSound));
        }
        else
        {
            StartCoroutine(PlayAnimation("DoorOpen", openSound));
        }
    }

    private IEnumerator PlayAnimation(string animationName, AudioClip sound)
    {
        animator.Play(animationName);
        audioSource.PlayOneShot(sound);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        doorOpen = !doorOpen;
    }
}
