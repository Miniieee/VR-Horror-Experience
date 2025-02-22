using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLights : MonoBehaviour
{
    public void Execute()
    {
        DisableAllLights();
    }

    private void DisableAllLights()
    {
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            DisableLightsInChildren(obj.transform);
        }
    }

    private void DisableLightsInChildren(Transform parentTransform)
    {
        Light light = parentTransform.GetComponent<Light>();

        if (light != null)
        {
            light.enabled = false;
        }

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            DisableLightsInChildren(parentTransform.GetChild(i));
        }
    }
}
