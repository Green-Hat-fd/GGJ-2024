using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrazioneScript : MonoBehaviour
{
    [Space(10)]
    [SerializeField] AudioSource audioNarrazione;
    [SerializeField] AudioClip clipNarrazione;
    [Range(0, 10)]
    [SerializeField] float finitoDelaySec = 1.5f;
    [Space(10)]
    [SerializeField] TMP_Text testoDaModificare;
    [TextArea(0, 4)]
    [SerializeField] string sottotitolo;
    float tempoSott;



    private void Awake()
    {
        //Prende il tempo della narrazione
        //e gli aggiunge il delay
        tempoSott = audioNarrazione.clip.length + finitoDelaySec;

        DisattivaSottotitoli();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttivaLineaDiDialogo();
        }
    }


    void AttivaLineaDiDialogo()
    {
        //Attiva la narrazione come audio
        audioNarrazione.Stop();
        audioNarrazione.clip = clipNarrazione;
        audioNarrazione.Play();

        //Attiva i sottotitoli...
        testoDaModificare.text = sottotitolo;
        testoDaModificare.gameObject.SetActive(true);
        Invoke(nameof(DisattivaSottotitoli), tempoSott); //...e li toglie dopo che ha finito l'audio


        //Disattiva il collider
        GetComponent<Collider>().enabled = false;
    }


    void DisattivaSottotitoli()
    {
        testoDaModificare.gameObject.SetActive(false);
    }
}
