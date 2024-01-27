using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiproduciSuoniAnim : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> clip;



    public void PlaySfxCasuale()
    {
        int i_rand = Random.Range(0, clip.Count);
        AudioClip c = clip[i_rand];

        //Mette la clip nel suono e lo riproduce
        source.PlayOneShot(c);
    }
}
