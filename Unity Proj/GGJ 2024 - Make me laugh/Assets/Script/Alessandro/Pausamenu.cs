using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Pausamenu : MonoBehaviour
{
    bool GameIsPaused = false;

    public Canvas pauseMenuUI;
    GameObject pauseMenu;
    List<AudioSource> sfxNellaScena,
                      dialoghiNellaScena;

    [SerializeField] List<MonoBehaviour> scriptDaDisbilitare;
    [SerializeField] AudioMixerGroup gruppoSfx,
                                     gruppoDialoghi;


    void Start()
    {
        pauseMenu = pauseMenuUI.gameObject;

        //Trova tutti gli AudioSource nella scena
        //e li divide nelle categorie
        AudioSource[] tuttiAudioSource = FindObjectsOfType<AudioSource>(true);
        
        sfxNellaScena = new List<AudioSource>();
        dialoghiNellaScena = new List<AudioSource>();

        for (int i = 0; i < tuttiAudioSource.Length; i++)
        {
            var source = tuttiAudioSource[i];

            //Lo aggiunge alla lista degli SFX
            if (source.outputAudioMixerGroup == gruppoSfx)
            {
                sfxNellaScena.Add(source);
                tuttiAudioSource[i] = null;
            }

            //Lo aggiunge alla lista dei Dialoghi
            if (source.outputAudioMixerGroup == gruppoDialoghi)
            {
                dialoghiNellaScena.Add(source);
                tuttiAudioSource[i] = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameIsPaused = !GameIsPaused;
            print(GameIsPaused);

            MettiInPausa(GameIsPaused);
        }
    }

    void MettiInPausa(bool inPausa)
    {
        pauseMenu.SetActive(inPausa);

        SistemaSuoni(!inPausa);

        Time.timeScale = inPausa
                           ? 0f
                           : 1f;

        Cursor.visible = inPausa;
        Cursor.lockState = inPausa
                            ? CursorLockMode.None
                            : CursorLockMode.Locked;

        AttivaScript(!inPausa);
    }

    public void Riprendi_UI()
    {
        GameIsPaused = false;

        MettiInPausa(false);
    }


    void AttivaScript(bool sonoAttivo)
    {
        foreach (MonoBehaviour scr in scriptDaDisbilitare)
        {
            scr.enabled = sonoAttivo;
        }
    }

    void SistemaSuoni(bool daRiprodurre)
    {
        foreach (AudioSource audio in sfxNellaScena)
        {
            if (daRiprodurre)
            {
                audio.UnPause();
            }
            else
            {
                audio.Pause();
            }
        }
    }


    public void Menu()
    {
        Debug.Log("Loading menu...");
        SceneManager.LoadScene(0);
    }

    public void Esci()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
