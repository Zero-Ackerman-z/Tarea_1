using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private PlayerInputActions controls;
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;

    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.Game.Move.performed += ctx => Movimiento(ctx);
        controls.Game.Move.canceled += ctx => StopMovement();
    }
    private void OnEnable()
    {
        controls.Game.Enable(); 
    }
    private void OnDisable()
    {
        controls.Game.Disable();
    }
    private void Update() {
        myRBD2.velocity = movementInput * velocityModifier;
        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);

        Debug.DrawRay(transform.position, (mouseInput - (Vector2)transform.position).normalized * rayDistance, Color.red);

        if (Input.GetMouseButtonDown(0)){
            Fire();
            Debug.Log("Right Click");
        }else if(Input.GetMouseButtonDown(1)){
            Fire();
            Debug.Log("Left Click");
        }
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    public void Movimiento(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void StopMovement()
    {
        movementInput = Vector2.zero;
    }
    private void Fire()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = (mousePosition - (Vector2)transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        if (projectileRigidbody != null)
        {
            projectileRigidbody.velocity = fireDirection * projectileSpeed;
        }
    }
}


