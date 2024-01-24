using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompSuNemico : MonoBehaviour
{
    public float Rimbalzo;
    public Rigidbody rb;
    
    void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Nemico"))
        {
            Destroy(other.gameObject);
            rb.velocity = new Vector2(rb.velocity.x, Rimbalzo);
        }
    }
}
