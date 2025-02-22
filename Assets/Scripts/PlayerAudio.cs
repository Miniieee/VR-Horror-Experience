using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private XRIDefaultInputActions inputActions;
    [SerializeField] private FMODUnity.EventReference audioEvent;
    private Vector2 movementInputValue;

    private bool isMoving = false;

    
    [SerializeField] private float movementSpeed;


    private void Awake() {
        inputActions = new XRIDefaultInputActions();
    }

    private void Start() 
    {
        
        InvokeRepeating("PlayAudioWhileMoving", 0f, movementSpeed);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update() {

        movementInputValue = inputActions.XRILeftHandLocomotion.Move.ReadValue<Vector2>();

        if (movementInputValue != Vector2.zero)
        {
            isMoving = true;
            
        }
        else
        {
            isMoving = false;
        }
    }

    private void PlayAudioWhileMoving()
    {
        if (isMoving)
        {
            RuntimeManager.PlayOneShot(audioEvent);

        }
    }
}
