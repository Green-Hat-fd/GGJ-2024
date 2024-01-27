using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DannoAContatto : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collObj = collision.gameObject;

        if (collObj.CompareTag("Player"))
        {
            collObj.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
