using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompSuNemico : MonoBehaviour
{
    public float RimbalzoSuNemico;
    public float RimbalzoSuTrampolino;
    public Rigidbody rb;
    public GameObject mortePart;

    [Header("—— Feedback ——")]
    [SerializeField] AudioSource boingTrampolinoSfx_source;
    [SerializeField] List<AudioClip> boingTrampolinoSfx;
    
    void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Dentiera"))
        {
            Instantiate(mortePart, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            rb.velocity = new Vector2(rb.velocity.x, RimbalzoSuNemico);
        }
        
        if(other.CompareTag("Jack")
            &&
            !other.GetComponentInParent<JackInTheBox>().LeggiSonoFuori())
        {
            Instantiate(mortePart, other.transform.position, Quaternion.identity);
            Destroy(other.transform.parent.gameObject);
            rb.velocity = new Vector2(rb.velocity.x, RimbalzoSuNemico);
        }

         if(other.CompareTag("Trampolino"))
        {
            rb.velocity = new Vector2(rb.velocity.x, RimbalzoSuTrampolino);

            //Feedback
            int i_rand = Random.Range(0, boingTrampolinoSfx.Count);
            AudioClip clip = boingTrampolinoSfx[i_rand];
            boingTrampolinoSfx_source.PlayOneShot(clip);
        }
    }
}
