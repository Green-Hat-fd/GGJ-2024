
/* Appena si attiva --> attiva l'animazione e attiva il box collider
 * quando torna dentro --> disattiva il collider e ritorna dentro.
 * 
 * Il giocatore entra si fa danno nel collider
 * 
 * Se il giocatore entra nel range, si attiva subito,
 * se no torna dentro e rimane "dormiente"
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBox : MonoBehaviour
{
    Transform giocatore;

    bool sonoFuori = false;
    bool giocatoreNelRange = false;
    bool doOnce = true;

    [SerializeField] Collider collAttacco;
    [Range(1, 120)]
    [SerializeField] float tempoNellaScatola = 5;
    [Range(2, 120)]
    [SerializeField] float tempoFuori = 5;
    [Min(0.01f)]
    [SerializeField] float secAnimazUscita,
                           secAnimazRientro;
    [Space(10)]
    [SerializeField] float rangeAttivazione;

    [Header("—— Feedback ——")]
    [SerializeField] Animator nemicoAnim;
    [SerializeField] AudioSource caricaSfx,
                                 esceSfx,
                                 entraSfx;
    


    void Start()
    {
        giocatore = FindObjectOfType<MovimGiocatRb>().transform;

        collAttacco.enabled = false;
        sonoFuori = false;
        giocatoreNelRange = false;
        doOnce = true;

        StartCoroutine(RicaricaTrappola());
    }


    void Update()
    {
        // Calcola la distanza tra il nemico e il giocatore
        float distGiocatore = Vector3.Distance(transform.position, giocatore.position);

        giocatoreNelRange = distGiocatore <= rangeAttivazione;

        //Ogni volta che il giocatore entra nel suo range
        //e si trova dentro, salta subito fuori
        if(giocatoreNelRange
           &&
           !sonoFuori
           &&
           doOnce)
        {
            StopAllCoroutines();
            StartCoroutine(AttivaTrappola());

            doOnce = false;
        }
    }

    IEnumerator AttivaTrappola()
    {
        //Feedback esce
        caricaSfx.Stop();
        esceSfx.Play();
        nemicoAnim.SetBool("SonoFuori", true);

        //Esce dalla scatola
        collAttacco.enabled = true;
        sonoFuori = true;
        print("Jack-Box: Fuori");

        
        yield return new WaitForSeconds(tempoFuori + secAnimazUscita);   //Aspetta il tempo fuori
        

        //Inizia la ricarica
        StartCoroutine(TornaDentro());
    }


    IEnumerator TornaDentro()
    {
        //Feedback ritorna dentro
        entraSfx.Play();
        nemicoAnim.SetBool("SonoFuori", false);

        //Torna nella scatola
        collAttacco.enabled = false;
        sonoFuori = false;
        print("Jack-box: Ri-entra");

        
        yield return new WaitForSeconds(secAnimazRientro);   //Aspetta prima di ricaricare
        

        //Inizia la ricarica
        StartCoroutine(RicaricaTrappola());
    }

    IEnumerator RicaricaTrappola()
    {
        //Feedback ricarica
        caricaSfx.Play();
        print("Jack-box: dentro che sta caricando");


        yield return new WaitForSeconds(tempoNellaScatola);   //Aspetta il tempo dentro


        //Attiva la trappola solo
        //se il giocatore si trova dentro l'area
        if (giocatoreNelRange)
        {
            StartCoroutine(AttivaTrappola());

            //Ogni volta che il giocatore esce dal range
            //resetta la possibilità di poter fare il doOnce
            doOnce = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    public bool LeggiSonoFuori() => sonoFuori;



    #region EXTRA - Gizmos

    private void OnDrawGizmosSelected()
    {
        //Disegna l'area di azione
        Gizmos.color = new Color(1, 0.65f, 0, 1);
        Gizmos.DrawWireSphere(transform.position, rangeAttivazione);
    }

    #endregion
}
