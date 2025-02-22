using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FirstFlashlightGrab : MonoBehaviour
{

    [SerializeField] private GameObject playerGO;


    public void SetSocketInteractorActive()
    {
        if (playerGO.activeSelf == false)
        {
            playerGO.SetActive(true);
        }
        
    }
}
