using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FlashLightControl : MonoBehaviour
{
    private XRIDefaultInputActions inputActions;
    private bool isHeldByRightHand;
    private bool isHeldByLeftHand;

    [SerializeField] private GameObject lightSource;
    [SerializeField] private FMODUnity.EventReference flashlightClick;

    


    private void Awake() 
    {
        inputActions = new XRIDefaultInputActions();
    }

    private void Start()
    {
        isHeldByLeftHand = false;
        isHeldByRightHand = false;

        lightSource.SetActive(false);

        XRIselectState();

    }

    private void XRIselectState()
    {
        inputActions.XRILeftHandInteraction.Select.started += context => isHeldByLeftHand = true;
        inputActions.XRILeftHandInteraction.Select.performed += context => isHeldByLeftHand = true;
        inputActions.XRILeftHandInteraction.Select.canceled += context => isHeldByLeftHand = false;

        inputActions.XRIRightHandInteraction.Select.started += context => isHeldByRightHand = true;
        inputActions.XRIRightHandInteraction.Select.performed += context => isHeldByRightHand = true;
        inputActions.XRIRightHandInteraction.Select.canceled += context => isHeldByRightHand = false;
    }

    private void Update() 
    {
        if (inputActions.XRILeftHandInteraction.Activate.triggered && isHeldByLeftHand || inputActions.XRIRightHandInteraction.Activate.triggered && isHeldByRightHand)
        {
            lightSource.SetActive(!lightSource.activeSelf);
            RuntimeManager.PlayOneShot(flashlightClick);
        }
    }

    public void OnRelease()
    {
        lightSource.SetActive(false);
    }

    private void OnEnable() 
    {
        inputActions.Enable();
    }

    private void OnDisable() 
    {
        inputActions.Disable();
    }
}
