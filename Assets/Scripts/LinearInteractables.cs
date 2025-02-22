using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LinearInteractables : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable handle = null;
    [SerializeField] private PhysicsMover mover = null;

    [Header("Direction")]
    [SerializeField] private Transform startPos = null;
    [SerializeField] private Transform endPos = null;

    private Vector3 grabPosition = Vector3.zero;
    private float startPercent = 0.0f;
    private float currentPercent = 0.0f;

    protected virtual void OnEnable() 
    {
        handle.selectEntered.AddListener(StoreGrabInfo);
    }

    protected virtual void OnDisable() 
    {
        handle.selectEntered.RemoveListener(StoreGrabInfo);
    }

    private void StoreGrabInfo(SelectEnterEventArgs args)
    {
        startPercent = currentPercent;
        grabPosition = args.interactorObject.transform.position;
    }

    void Update()
    {
        if (handle.isSelected)
        {
            UpdateDrawer();
        }

    }

    private void UpdateDrawer()
    {
        float newPersentage = startPercent + FindPercentageDifference();

        mover.MoveTo(Vector3.Lerp(startPos.position, endPos.position, newPersentage));
        currentPercent = Mathf.Clamp01(newPersentage);
    }

    private float FindPercentageDifference()
    {
        Vector3 handPosition = handle.transform.position;
        Vector3 pullDirection = handPosition - grabPosition;
        Vector3 targetDirection = endPos.position - startPos.position;

        float length = targetDirection.magnitude;
        targetDirection.Normalize();


        return Vector3.Dot(pullDirection,targetDirection) / length;
    }
}
