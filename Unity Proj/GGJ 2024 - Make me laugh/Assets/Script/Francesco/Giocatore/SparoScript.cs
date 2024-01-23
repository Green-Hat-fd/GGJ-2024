using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparoScript : MonoBehaviour
{
    [SerializeField] GameObject collDaAttivare;
    [SerializeField] float ricaricaSec = 0.75f;
    bool possoSparare = true;
    float ricaricaAttuale = 0;


    
    void Awake()
    {
        Collider coll = collDaAttivare.GetComponent<Collider>();
        coll.isTrigger = true;
        coll.enabled = true;

        ricaricaAttuale = 0;

        //Set del mouse al centro e nascosto
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
    
    void Update()
    {
        //Se preme il tasto e puo' sparare, spara
        if (Input.GetKeyDown(KeyCode.Mouse0) && possoSparare)
        {
            collDaAttivare.SetActive(true);
            possoSparare = false;
        }
        

        //Quando non puo' sparare, c'e' un tempo di ricarica
        //dove il giocatore non puo' sparare
        if (!possoSparare)
        {
            if (ricaricaAttuale > ricaricaSec)   //Se supera il tempo max
            {
                //Ripristina i valori di default
                possoSparare = true;
                ricaricaAttuale = 0;
            }
            else
            {
                ricaricaAttuale += Time.deltaTime;    //Aumenta la ricarica "attuale"
            }
        }
    }
}
