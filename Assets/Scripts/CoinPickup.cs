using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinPickup = 100;
    bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected)
        {
            FindObjectOfType<GameSession>().AddToScore(coinPickup);
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
