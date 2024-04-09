using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyec : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destruye el proyectil si choca con un muro
        }
    }
}
