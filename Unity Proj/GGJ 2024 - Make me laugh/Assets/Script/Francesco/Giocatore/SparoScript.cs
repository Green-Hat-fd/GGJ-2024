using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparoScript : MonoBehaviour
{
    [SerializeField] Collider collDaAttivare;
    [SerializeField] float ricaricaSec = 0.75f,
                           boxAttivoSec = 0.5f;
    bool possoSparare = true;
    float ricaricaAttuale = 0,
          boxAttivoAttuale = 0;

    [Header("—— Feedback ——")]
    [SerializeField] Animator giocatAnim;
    [SerializeField] AudioSource sparoSfx;
    [SerializeField] ParticleSystem bangPart;


    
    void Awake()
    {
        collDaAttivare.isTrigger = true;
        collDaAttivare.enabled = false;

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
            collDaAttivare.enabled = true;
            possoSparare = false;

            //Feedback
            giocatAnim.SetTrigger("Sparo");
            sparoSfx.Play();
            bangPart.Play();

            print("SPARO");
        }
        else
        {
            if (boxAttivoAttuale < boxAttivoSec
                &&
                collDaAttivare.enabled)
            {
                //Aumenta il tempo quando
                //il box deve rimanere attivo
                boxAttivoAttuale += Time.deltaTime;
            }
            else
            {
                //Disattiva il collider dopo tot
                collDaAttivare.enabled = false;
                boxAttivoAttuale = 0;
            }
        }


        //Quando non puo' sparare, c'e' un tempo di ricarica
        //dove il giocatore non puo' sparare
        if (!possoSparare)
        {
            if (ricaricaAttuale >= ricaricaSec)   //Se supera il tempo max
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



    #region EXTRA - Cambiare l'Inspector
    
    private void OnValidate()
    {
        ricaricaSec = Mathf.Clamp(ricaricaSec, 0, ricaricaSec);
        boxAttivoSec = Mathf.Clamp(boxAttivoSec, 0, ricaricaSec);
    }

    #endregion
}
