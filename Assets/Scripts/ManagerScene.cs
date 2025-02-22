using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CountToLoadNextScene());
    }


    IEnumerator CountToLoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }
   
}
