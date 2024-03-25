using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.AddCoin(); 
            }

            Destroy(gameObject);
        }
    }
}
