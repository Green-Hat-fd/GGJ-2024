using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GiocatDannoScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Se ha un nemico
        if (other.CompareTag("Nemico"))
        {
            other.GetComponentInParent<NemicoMimo>().Danno();

            print("Bang!  -  Danno al nemico arrecato");
        }
    }
}
