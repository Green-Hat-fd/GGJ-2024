using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProiettileScript : MonoBehaviour
{
    [SerializeField] float velProiettile = 6.5f;
    [SerializeField] float vitaProietSec = 15f;



    private void OnEnable()
    {
        Destroy(gameObject, vitaProietSec);
    }

    void Update()
    {
        //Sposta sempre in avanti il proiettile
        transform.position += transform.forward * Time.deltaTime * velProiettile;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Se ha un nemico
        if (other.CompareTag("Player"))
        {
            //

            print("Ahia!  -  Giocatore colpito");
        }

        Destroy(gameObject);
    }
}
