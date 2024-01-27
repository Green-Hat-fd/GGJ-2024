using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparoScript : MonoBehaviour
{
    [SerializeField] GameObject collDaAttivare;
    [SerializeField] float ricaricaSec = 0.75f;
    bool possoSparare = true;
    float ricaricaAttuale = 0;

    [Header("—— Feedback ——")]
    [SerializeField] Animator giocatAnim;
    [SerializeField] AudioSource sparoSfx;


    
    void Awake()
    {
        Collider coll = collDaAttivare.GetComponent<Collider>();
        coll.isTrigger = true;
        coll.enabled = false;

        ricaricaAttuale = 0;

        //Set del mouse al centro e nascosto
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        //Se preme il tasto e puo' sparare, spara
        if (Input.GetKeyDown(KeyCode.Mouse0) && possoSparare)
        {
            collDaAttivare.GetComponent<Collider>().enabled = true;
            possoSparare = false;

            //Feedback
            giocatAnim.SetTrigger("Sparo");
            sparoSfx.Play();

            print("SPARO");
        }
        else
        {
            if(ricaricaAttuale > 0.5f)
            {
                collDaAttivare.GetComponent<Collider>().enabled = false;
            }
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
