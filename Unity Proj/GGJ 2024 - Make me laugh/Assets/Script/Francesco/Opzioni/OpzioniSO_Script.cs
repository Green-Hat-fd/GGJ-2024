using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptable Objects/Opzioni (S.O.)", fileName = "Opzioni_SO")]
public class OpzioniSO_Script : ScriptableObject
{
    //Opzioni
    #region Volume e Audio

    [Space(15)]
    [SerializeField] AudioMixer mixerGenerale;
    [SerializeField] AnimationCurve curvaAudio;
    [Range(0, 110)]
    [SerializeField] float volumeMusica = 0f;
    [Range(0, 110)]
    [SerializeField] float volumeSuoni = 0f;
    [Range(0, 110)]
    [SerializeField] float volumeDialoghi = 0f;

    ///<summary></summary>
    /// <param name="vM"> il nuovo volume, nel range [0; 1.1]</param>
    public void CambiaVolumeMusica(float vM)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("musVol", curvaAudio.Evaluate(vM));
        
        volumeMusica = vM * 100;
    }
    ///<summary></summary>
    /// <param name="vS"> il nuovo volume, nel range [0; 1.1]</param>
    public void CambiaVolumeSuoni(float vS)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("sfxVol", curvaAudio.Evaluate(vS));
        
        volumeSuoni = vS * 100;
    }
    ///<summary></summary>
    /// <param name="vD"> il nuovo volume, nel range [0; 1.1]</param>
    public void CambiaVolumeDialoghi(float vD)
    {
        //Lo mette come volume nel mixer tra [-80; 5] dB
        mixerGenerale.SetFloat("dialoghiVol", curvaAudio.Evaluate(vD));

        volumeDialoghi = vD * 100;
    }

    public AnimationCurve LeggiCurvaVolume() => curvaAudio;

    public float LeggiVolumeMusica() => curvaAudio.Evaluate(volumeMusica);
    public float LeggiVolumeMusica_Percent() => volumeMusica / 100;
    public float LeggiVolumeSuoni() => curvaAudio.Evaluate(volumeSuoni);
    public float LeggiVolumeSuoni_Percent() => volumeSuoni / 100;
    public float LeggiVolumeDiaoghi() => curvaAudio.Evaluate(volumeDialoghi);
    public float LeggiVolumeDialoghi_Percent() => volumeDialoghi / 100;

    #endregion


    #region Schermo intero

    [Space(15)]
    [SerializeField] bool schermoIntero = true;

    public void SchermoIntero_OnOff(bool yn)
    {
        Screen.fullScreen = yn;

        schermoIntero = yn;
    }

    #endregion


    //Altro
    #region Altre funzioni

    #endregion
}