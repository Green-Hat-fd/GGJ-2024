using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DannoTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject objEntrato = other.gameObject;

        if (objEntrato.CompareTag("Player"))
        {
            objEntrato.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
