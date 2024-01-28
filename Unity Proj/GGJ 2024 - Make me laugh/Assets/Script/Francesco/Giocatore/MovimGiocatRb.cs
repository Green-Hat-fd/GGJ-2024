using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimGiocatRb : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float forzaMovim = 8.5f;
    [SerializeField] float velMax = 7.5f;
    [Range(0, 1)]
    [SerializeField] float attritoNoInput = 5;
    [SerializeField] float potenzaSalto = 8.5f;
    [Space(10)]
    [SerializeField] float moltGravitaInDiscesa = 1.1f;
    [SerializeField] float maxVelInDiscesa = 15f;
    float movimX;
    bool siMuove;
    
    Vector3 muovi;

    [Space(20)]
    [SerializeField] float sogliaRilevaTerreno = 0.25f;
    float mezzaAltezzaGiocat;
    float raggioSpherecast = 0.5f;

    bool siTrovaATerra = false;
    RaycastHit hitBase;

    bool hoSaltato = false;

    [Space(20)]
    [SerializeField] GameObject modello;
    Transform modelloTr;
    Vector3 rot;
    [SerializeField] float velRotazione = 8.5f;

    [Header("—— Feedback ——")]
    [SerializeField] Animator giocatAnim;
    [SerializeField] AudioSource saltoSfx;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        mezzaAltezzaGiocat = GetComponent<CapsuleCollider>().height / 2;

        modelloTr = modello.transform;
        rot = Vector3.up * 90.01f;
    }

    private void Update()
    {
        //Prende gli assi dall'input di movimento
        movimX = Input.GetAxisRaw("Horizontal");
        siMuove = movimX != 0;

        muovi = transform.right * movimX;      //Vettore movimento orizzontale


        //Cambia l'attrito se si trova o a terra in aria
        //rb.drag = siTrovaATerra ? attritoTerr : attritoAria;


        //Prende l'input di salto
        hoSaltato = Input.GetKey(KeyCode.Space);


        //Ruota il giocatore verso dove si sta muovendo
        if (movimX > 0)
        {
            rot = Vector3.up * 90.01f;    //A destra
        }
        if (movimX < 0)
        {
            rot = Vector3.up * -90.01f;    //A sinistra
        }

        modello.transform.rotation = Quaternion.RotateTowards(modello.transform.rotation,
                                                              Quaternion.Euler(rot),
                                                              Time.deltaTime * velRotazione * 20);


        //Feedback
        giocatAnim.SetBool("Cammina", siMuove);
    }

    void FixedUpdate()
    {
        //Calcolo se si trova a terra
        //(non colpisce i Trigger e "~0" significa che collide con tutti i layer)
        siTrovaATerra = Physics.SphereCast(transform.position,
                                           raggioSpherecast,
                                           -transform.up,
                                           out hitBase,
                                           mezzaAltezzaGiocat + sogliaRilevaTerreno - raggioSpherecast,
                                           ~0,
                                           QueryTriggerInteraction.Ignore);


        float moltVelAria = !siTrovaATerra ? 0.65f : 1;   //Diminuisce la velocita' orizz. se si trova in aria


        //Salta se premi Spazio e si trova a terra
        if (hoSaltato && siTrovaATerra)
        {
            //Resetta la velocita' Y e applica la forza d'impulso verso l'alto
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * potenzaSalto, ForceMode.Impulse);

            //Feedback
            giocatAnim.SetTrigger("Salto");
        }


        //Movimento orizzontale (semplice) del giocatore
        rb.AddForce(muovi.normalized * forzaMovim * 10f, ForceMode.Force);

        //Feedback
        giocatAnim.SetBool("Sono A Terra", siTrovaATerra);



        #region Limitazione della velocita'

        //Prende la velocita' orizzontale del giocatore
        Vector3 velOrizz = new Vector3(rb.velocity.x, 0, rb.velocity.z),
                velVert = new Vector3(0, rb.velocity.y, 0);


        //Controllo se si accelera troppo, cioe' si supera la velocita'
        if (velOrizz.magnitude >= velMax)
        {
            //Limita la velocita' a quella prestabilita, riportandola al RigidBody
            Vector3 limitazione = velOrizz.normalized * velMax * moltVelAria;
            rb.velocity = new Vector3(limitazione.x, rb.velocity.y, limitazione.z);
        }


        //Aumenta la gravita' quando il giocatore sta cadendo
        if (rb.velocity.y < 0)
        {
            Vector3 _velDiscesa = velVert;

            _velDiscesa *= moltGravitaInDiscesa;
            _velDiscesa.y = Mathf.Clamp(_velDiscesa.y, -maxVelInDiscesa, 0);

            rb.velocity = velOrizz / moltGravitaInDiscesa + _velDiscesa;
        }


        if (!siTrovaATerra)
        {
            //Applica l'attrito dell'aria al giocatore
            //(Riduce la velocita' se il giocatore e' in aria e si sta muovendo)
            if (rb.velocity.x >= 0.05f || rb.velocity.z >= 0.05f)
            {
                rb.AddForce(new Vector3(-rb.velocity.x * 0.1f, 0, -rb.velocity.z * 0.1f), ForceMode.Force);
            }
        }
        else
        {
            //Limita l'attrito quando non riceve input di lato
            if (!siMuove)
            {
                rb.velocity = velVert + velOrizz * attritoNoInput;
            }
        }

        #endregion
    }


    #region EXTRA - Gizmo

    private void OnDrawGizmos()
    {
        //Disegna lo SphereCast per capire se e' a terra o meno (togliendo l'altezza del giocatore)
        Gizmos.color = new Color(0.85f, 0.85f, 0.85f, 1);
        Gizmos.DrawWireSphere(transform.position + (-transform.up * mezzaAltezzaGiocat)
                               + (-transform.up * sogliaRilevaTerreno)
                               - (-transform.up * raggioSpherecast),
                              raggioSpherecast);

        //Disegna dove ha colpito se e' a terra e se ha colpito un'oggetto solido (no trigger)
        Gizmos.color = Color.green;
        if (siTrovaATerra && hitBase.collider)
        {
            Gizmos.DrawLine(hitBase.point + (transform.up * hitBase.distance), hitBase.point);
            Gizmos.DrawCube(hitBase.point, Vector3.one * 0.1f);
        }
    }

    #endregion
}
