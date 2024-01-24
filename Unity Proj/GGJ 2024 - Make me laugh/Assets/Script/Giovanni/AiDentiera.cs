using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDentiera : MonoBehaviour
{
    public GameObject PuntoA;
    public GameObject PuntoB;

    private Rigidbody rb;

    private Transform CurrentPoint;

    public float Velocita;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CurrentPoint = PuntoB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = CurrentPoint.position - transform.position;
        if(CurrentPoint == PuntoB.transform)
        {
            rb.velocity = new Vector2(Velocita, 0);
        }
        else
        {
            rb.velocity = new Vector2(-Velocita, 0);
        }

        if(Vector2.Distance(transform.position, CurrentPoint.position) < 0.5f && CurrentPoint == PuntoB.transform)
        {
            CurrentPoint = PuntoA.transform;
        }

        if(Vector2.Distance(transform.position, CurrentPoint.position) < 0.5f && CurrentPoint == PuntoA.transform)
        {
            CurrentPoint = PuntoB.transform;
        }
    }
}
