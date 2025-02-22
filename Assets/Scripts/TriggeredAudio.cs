using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredAudio : MonoBehaviour
{
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public GameObject audioPlayer; // Reference to the target GameObject with the AudioSource component
    private AudioSource audioSource;
    private bool isPlaying;

    private void Start()
    {
        audioSource = audioPlayer.GetComponent<AudioSource>(); // Get the AudioSource component from the target GameObject
        isPlaying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaying)
        {
            StartCoroutine(PlayAudioInOrder());
        }
    }

    private IEnumerator PlayAudioInOrder()
    {
        isPlaying = true;

        // Play first audio clip
        audioSource.clip = audioClip1;
        audioSource.Play();
        yield return new WaitForSeconds(audioClip1.length);

        // Play second audio clip
        audioSource.clip = audioClip2;
        audioSource.Play();
        audioSource.loop = true;
        yield return new WaitForSeconds(audioClip2.length * 3f);
        audioSource.Stop();

        isPlaying = false;
    }
}
