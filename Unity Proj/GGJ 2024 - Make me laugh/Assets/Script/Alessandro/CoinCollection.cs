using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{

    public int Coin = 0;

    public TextMeshProUGUI CoinText;

    public AudioSource Monete;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Coin")
        {
            Coin++;
            CoinText.text = "Coin: " + Coin.ToString();
            Monete.Play();
            Debug.Log(Coin);
            Destroy(other.gameObject);
        }
    }
}
