using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FlashLightAim : MonoBehaviour
{
    [SerializeField] private GameObject lightParent;
    [SerializeField] private GameObject objectToToggle;
    [SerializeField] private string triggerTag = "FlashlightTarget";
    [SerializeField] private float gizmoLineLength = 5f;
    [SerializeField] private Color gizmoLineColor = Color.red;
    [SerializeField] private float flickeringFrequency = 0.1f;

    [SerializeField] private FMODUnity.EventReference flashlightFlickering;
    [SerializeField] private FMODUnity.EventReference scream;

    private Light[] flashlightLights;
    private bool isFlickering;

    private GameObject eventTrigger;

    void Start()
    {
        flashlightLights = lightParent.GetComponentsInChildren<Light>();
        isFlickering = false;
    }

    void Update()
    {
        if (IsFlashlightAimingAtTrigger() && !isFlickering)
        {
            StartCoroutine(FlickerFlashlight());
        }
    }

    private bool IsFlashlightAimingAtTrigger()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.up);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag(triggerTag) && hit.collider.isTrigger)
            {
                eventTrigger = hit.collider.gameObject;
                return true;
            }
        }

        return false;
    }

     IEnumerator FlickerFlashlight()
    {
        isFlickering = true;
        float elapsedTime = 0f;
        float[] originalIntensities = new float[flashlightLights.Length];

        for (int i = 0; i < flashlightLights.Length; i++)
        {
            originalIntensities[i] = flashlightLights[i].intensity;
        }

        RuntimeManager.PlayOneShot(flashlightFlickering);

        float flickeringDuration = 3f/*aimingAudioClip.length*/;

        while (elapsedTime < flickeringDuration)
        {
            for (int i = 0; i < flashlightLights.Length; i++)
            {
                flashlightLights[i].intensity = originalIntensities[i] * Random.Range(0.5f, 1.1f);
            }
            elapsedTime += flickeringFrequency;
            yield return new WaitForSeconds(flickeringFrequency);
        }

        for (int i = 0; i < flashlightLights.Length; i++)
        {
            flashlightLights[i].intensity = originalIntensities[i];
        }

        //audioSource.Stop();
        

        if (IsFlashlightAimingAtTrigger())
        {
            objectToToggle.SetActive(true);
            RuntimeManager.PlayOneShot(scream);
            if (eventTrigger != null)
            {
                eventTrigger.SetActive(false);
            }
            
            yield return new WaitForSeconds(5f);
            objectToToggle.SetActive(false);
        }

        isFlickering = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoLineColor;
        Gizmos.DrawRay(transform.position, transform.up * gizmoLineLength);
    }


    /*public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    } */
}
