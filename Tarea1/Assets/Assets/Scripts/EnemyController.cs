using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform initialPosition; // Posición inicial del enemigo
    [SerializeField] private float chaseSpeed = 5f; // Velocidad de persecución
    [SerializeField] private float returnSpeed = 2f; // Velocidad de retorno a la posición inicial
    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private float fireRate = 1f; // Tasa de disparo en segundos
    [SerializeField] private float projectileSpeed = 10f; // Velocidad del proyectil
    private float nextFireTime; // Tiempo para el próximo disparo
    public int points = 10; // Puntos otorgados por este enemigo al ser derrotado

    private bool playerInRange = false; // Indica si el jugador está dentro del área del círculo
    public ScoreManager scoreManager; // Referencia al ScoreManager para actualizar el puntaje

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange)
        {
            // Perseguir al jugador
            Vector2 directionToPlayer = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
            transform.Translate(directionToPlayer * chaseSpeed * Time.deltaTime);

            // Disparar proyectil si ha pasado el tiempo de disparo
            if (Time.time > nextFireTime)
            {
                FireProjectile();
                nextFireTime = Time.time + 1f / fireRate; // Calcular el próximo tiempo de disparo
            }
        }
        else
        {
            // Regresar a la posición inicial
            Vector2 directionToInitialPosition = (initialPosition.position - transform.position).normalized;
            transform.Translate(directionToInitialPosition * returnSpeed * Time.deltaTime);
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        if (projectileRigidbody != null)
        {
            Vector2 directionToPlayer = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
            projectileRigidbody.velocity = directionToPlayer * projectileSpeed;
        }
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject); // Destruir el enemigo
        scoreManager.UpdateScore(points); // Actualizar el puntaje al derrotar al enemigo
    }
}
