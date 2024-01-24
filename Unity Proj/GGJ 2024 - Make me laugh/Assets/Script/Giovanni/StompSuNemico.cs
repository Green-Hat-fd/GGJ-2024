using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompSuNemico : MonoBehaviour
{
    public float RimbalzoSuNemico;
    public float RimbalzoSuTrampolino;
    public Rigidbody rb;
    
    void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Dentiera"))
        {
            Destroy(other.gameObject);
            rb.velocity = new Vector2(rb.velocity.x, RimbalzoSuNemico);
        }

         if(other.CompareTag("Trampolino"))
        {
            rb.velocity = new Vector2(rb.velocity.x, RimbalzoSuTrampolino);
        }
    }
}
