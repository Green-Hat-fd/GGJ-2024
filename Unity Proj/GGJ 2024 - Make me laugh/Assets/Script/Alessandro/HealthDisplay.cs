using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    int health;
    int maxHealth;

    public Image vitaImg;
    public Sprite vitaSpr_25,
                  vitaSpr_50,
                  vitaSpr_75,
                  vitaSpr_full;

    public PlayerHealth playerHealth;

    void Start()
    {
        maxHealth = playerHealth.maxHealth;
    }

    void Update()
    {
        health = playerHealth.health;

        float health_percent = (float)health / maxHealth;


        vitaImg.sprite = health_percent > 0.75f
                          ? vitaSpr_full
                          : health_percent > 0.50f
                              ? vitaSpr_75
                              : health_percent > 0.25f
                                  ? vitaSpr_50
                                  : vitaSpr_25;

        /*
        if (health_percent > 0.75f)
        {
            vitaImg.sprite = vitaSpr_full;
        }
        else 
        {
            if (health_percent > 0.5f)
            {
                vitaImg.sprite = vitaSpr_75;
            }
            else
            {
                if (health_percent > 0.25f)
                {
                    vitaImg.sprite = vitaSpr_50;
                } 
                else
                {
                    vitaImg.sprite = vitaSpr_25;
                }
            }
        }//*/
    }
}
