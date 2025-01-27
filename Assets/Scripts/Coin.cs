using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 5;

    public static Action<Coin> OnCoinCollected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnCoinCollected?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
