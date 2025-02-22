using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraYRotation : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    private Vector3 cameraRotation;
    private Vector3 snapRotationEuler;

    private Quaternion rotation;

    void Start()
    {
        transform.position = mainCamera.transform.position;
    }

    void Update()
    {
        transform.position = mainCamera.transform.position;

        
        cameraRotation = mainCamera.transform.rotation.eulerAngles;
        snapRotationEuler = new Vector3(0f, cameraRotation.y, 0f);

        transform.rotation = Quaternion.Euler(snapRotationEuler);
    }
}
