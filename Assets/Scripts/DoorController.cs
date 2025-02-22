using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private AudioClip openingSound;
    [SerializeField] private AudioClip closingSound;

    private AudioSource audioSource;
    private float previousYRotation;
    private float tresholdValue = 0.5f;
    private float noSoundAngle = -5f;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        previousYRotation = transform.localEulerAngles.y;
    }

    private void Update()
    {
        float currentYRotation = transform.localEulerAngles.y;
        float rotationDelta = currentYRotation - previousYRotation;

        if (Mathf.Abs(rotationDelta) > tresholdValue)
        {
            if (!audioSource.isPlaying)
            {
                if (transform.localEulerAngles.y > noSoundAngle)
                {
                    if (rotationDelta > 0) // Opening
                    {
                        audioSource.clip = openingSound;
                    }
                    else // Closing
                    {
                        audioSource.clip = closingSound;
                    }

                    audioSource.Play();
                }
            }
        }

        previousYRotation = currentYRotation;
    }
}
