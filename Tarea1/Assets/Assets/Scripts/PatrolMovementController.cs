using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float chaseVelocityModifier = 10f; // Velocidad aumentada cuando se persigue al jugador
    [SerializeField] private GameObject player; // Referencia al GameObject del jugador
    [SerializeField] private float detectionDistance = 2f; // Distancia de detección del jugador
    public int points = 20; // Puntos otorgados por este enemigo al ser derrotado
    public ScoreManager scoreManager; // Referencia al ScoreManager para actualizar el puntaje

    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private bool playerDetected = false; // Indica si el jugador ha sido detectado por el enemigo



    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update() {
        CheckNewPoint();
        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        // Calcular la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, myRBD2.velocity.normalized, 1f);
        Debug.DrawRay(transform.position, myRBD2.velocity.normalized * 3f, Color.blue);

        if (distanceToPlayer < detectionDistance)
        {
            // El jugador está dentro de la distancia de detección
            playerDetected = true;
            myRBD2.velocity = (player.transform.position - transform.position).normalized * chaseVelocityModifier;
        }
        else
        {
            // El jugador no está dentro de la distancia de detección
            playerDetected = false;
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
    }

    private void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
        
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject); // Destruir el enemigo
        scoreManager.UpdateScore(points); // Actualizar el puntaje al derrotar al enemigo
    }

}
