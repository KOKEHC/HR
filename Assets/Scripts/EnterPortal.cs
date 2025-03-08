using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPortal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collission)
    {
        if (collission.gameObject.tag == "Player")
            SceneManager.LoadScene(0);
    }
}