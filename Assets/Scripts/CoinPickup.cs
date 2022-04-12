using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int intPointsForCoinPickup = 100;
    [SerializeField] AudioClip coinPickupSFX;

    bool blnWasCollected = false;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !blnWasCollected)
        {
            blnWasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(intPointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
