using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite emptyHeart;
    public Sprite fullheart;
    public Image[] Cuore;

    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;

        for (int i = 0; i < Cuore.Length; i++)
        {
            if (i < health)
            {
                Cuore[i].sprite = fullheart;
            }
            else
            {
                Cuore[i].sprite = emptyHeart;
            }
            
            if (i < maxHealth)
            {
                Cuore[i].enabled = true;
            }
            else
            {
                Cuore[i].enabled = false;
            }
        }
    }
}
