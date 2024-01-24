using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Win");
            SceneManager.LoadScene(3);
        }

    }
    public void Vittoria()
    {
        SceneManager.LoadScene(0);
    }
}
