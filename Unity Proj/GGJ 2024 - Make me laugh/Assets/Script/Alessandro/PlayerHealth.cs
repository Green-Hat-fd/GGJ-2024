using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 4;
    public float invSec = 2;
    bool possoPrendereDanno = true;
    float invSecPassati = 0;


    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (!possoPrendereDanno)
        {
            //Aumenta il tempo dell'invincibilita'
            //e quando lo supera, torna danneggiabile
            if(invSecPassati >= invSec)
            {
                possoPrendereDanno = true;

                invSecPassati = 0;
            }
            else
            {
                invSecPassati += Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (possoPrendereDanno)
        {
            health -= amount;
            possoPrendereDanno = false;
        }


        if (health <= 0)
        {
            Debug.LogError("Morto - apri scena della morte");
            SceneManager.LoadScene(2);
        }
    }
}
