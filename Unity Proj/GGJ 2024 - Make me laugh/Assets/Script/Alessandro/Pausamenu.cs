using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausamenu : MonoBehaviour
{
    bool GameIsPaused = false;

    public Canvas pauseMenuUI;
    GameObject pauseMenu;

    [SerializeField] List<MonoBehaviour> scriptDaDisbilitare;


    void Start()
    {
        pauseMenu = pauseMenuUI.gameObject;
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

        pauseMenu.SetActive(false);
        
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        AttivaScript(true);
    }


    void AttivaScript(bool sonoAttivo)
    {
        foreach (MonoBehaviour scr in scriptDaDisbilitare)
        {
            scr.enabled = sonoAttivo;
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
