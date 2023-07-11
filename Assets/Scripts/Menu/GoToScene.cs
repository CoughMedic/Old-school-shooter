using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().win_text.gameObject.SetActive(true);
            other.GetComponent<PlayerController>().StartFadeToBlack();
        }
    }
}
