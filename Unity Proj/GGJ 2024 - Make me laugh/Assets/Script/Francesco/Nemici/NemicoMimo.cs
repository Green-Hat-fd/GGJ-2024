using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemicoMimo : MonoBehaviour
{
    Transform giocatore;
    [SerializeField] GameObject proiettilePrefab;

    [Space(10)]
    [SerializeField] float cooldown = 1f;
    [SerializeField] Transform puntoSparo;
    [SerializeField] float maxDistSparo = 10f;

    bool possoSparare = true;
    bool sonoArrabbiato = true;
    bool sonoGirato = true;

    [Header("—— Quando ride (\"viene sconfitto\") ——")]
    [SerializeField] Transform posizQuandoRide;
    [SerializeField] float velQuandoRide = 6.5f;

    [Header("—— Feedback ——")]
    [SerializeField] GameObject modello;
    Transform modelloTr;
    [SerializeField] Collider collDaDisattivare;
    [SerializeField] Animator nemicoAnim;


    private void Awake()
    {
        giocatore = FindObjectOfType<MovimGiocatRb>().transform;
        modelloTr = modello.transform;
        possoSparare = true;
    }

    void Update()
    {
        // Calcola la distanza tra il nemico e il giocatore
        float distGiocatore = Vector3.Distance(transform.position, giocatore.position);

        if (distGiocatore <= maxDistSparo && sonoArrabbiato)    //Se il giocatore è abbastanza vicino...
        {
            // Calcola la direzione verso il giocatore
            Vector3 direz = (giocatore.position - transform.position).normalized;

            // Calcola l'angolo in radianti tra la direzione e il vettore destro (1,0)
            float angolo = Mathf.Atan2(direz.y, direz.x) * Mathf.Rad2Deg;

            // Forza l'angolo a essere 0 o 180 gradi
            angolo = Mathf.Abs(angolo) > 90f
                       ? -90f
                       : 90f;

            // Imposta la rotazione dell'oggetto in base all'angolo calcolato
            Quaternion rotFinale = Quaternion.Euler(Vector3.up * angolo);
            modelloTr.rotation = Quaternion.RotateTowards(modelloTr.rotation,
                                                          rotFinale,
                                                          Time.deltaTime * 700);

            sonoGirato = modelloTr.rotation == rotFinale;

            if (possoSparare && sonoGirato)
            {
                Spara();

                possoSparare = false;
                Invoke(nameof(AbilitaPossoSparare), cooldown);
            }
        }

        if (!sonoArrabbiato)
        {
            //Si mette dietro nello sfondo
            modelloTr.position = Vector3.MoveTowards(modelloTr.position,
                                                     posizQuandoRide.position,
                                                     Time.deltaTime * velQuandoRide);

            modelloTr.rotation = Quaternion.RotateTowards(modelloTr.rotation,
                                                          posizQuandoRide.rotation,
                                                          Time.deltaTime * velQuandoRide * 20);
        }
    }

    void Spara()
    {
        Instantiate(proiettilePrefab, puntoSparo.position, puntoSparo.rotation);


        //Feedback
        //nemicoAnim.SetTrigger("Attacca");
    }

    void AbilitaPossoSparare()
    {
        possoSparare = true;
    }

    public void Danno()
    {
        //Disattiva la possibilita' di sparare
        sonoArrabbiato = false;

        //Disattiva il collider
        collDaDisattivare.enabled = false;

        //Feedback della risata in loop
        //nemicoAnim.SetTrigger("Ridi");
    }



    #region EXTRA - Gizmos

    private void OnDrawGizmos()
    {
        //Disegna l'area di azione
        Gizmos.color = new Color(0, 0.75f, 1, 1);
        Gizmos.DrawWireSphere(transform.position, maxDistSparo);

        //Disegna un pallino dove si deve mettere
        //quando viene sconfitto e ride
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(posizQuandoRide.position, 0.15f);
    }

    #endregion
}
