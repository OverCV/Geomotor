using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Auto Forward Settings")]
    public float forwardSpeed = 15f;
    
    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer = 1;
    
    private PrometeoCarController carController;
    private Rigidbody carRigidbody;
    private bool isGrounded = true;
    
    void Start()
    {
        carController = GetComponent<PrometeoCarController>();
        carRigidbody = GetComponent<Rigidbody>();
        
        // Optimizar el vehículo para mejor manejo
        if (carController != null)
        {
            // Velocidades mejoradas
            carController.maxSpeed = 200;
            carController.maxReverseSpeed = 80;
            
            // Aceleración mejorada (66% más rápida en arranque)
            carController.accelerationMultiplier = 10;
            
            // Ángulo de giro mayor para A/D
            carController.maxSteeringAngle = 45;
            carController.steeringSpeed = 1.0f;
            
            // Mejor recuperación tras derrapes y frenadas
            carController.brakeForce = 600;
            carController.decelerationMultiplier = 4;
            carController.handbrakeDriftMultiplier = 3;
            
            // Salto mejorado
            carController.jumpForce = 15f;
            carController.jumpCooldown = 0.3f;
        }
    }
    
    void Update()
    {
        HandleInput();
        CheckGrounded();
    }
    
    void FixedUpdate()
    {
        // Auto-forward: El carrito siempre avanza hacia adelante
        AutoForward();
    }
    
    void AutoForward()
    {
        if (carController != null)
        {
            carController.GoForward();
        }
    }
    
    void HandleInput()
    {
        if (carController == null) return;
        
        // Steering mejorado (A/D para izquierda/derecha con mayor ángulo)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            carController.TurnLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            carController.TurnRight();
        }
        else
        {
            carController.ResetSteeringAngle();
        }
        
        // Jump (W o Flecha Arriba para saltar) - SOLO para saltar
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            Jump();
        }
        
        // Freno de mano con S o Flecha Abajo
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            carController.Handbrake();
        }
        else
        {
            carController.RecoverTraction();
        }
        
        // Pause game with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.currentState == GameState.Playing)
                {
                    GameManager.Instance.PauseGame();
                }
                else if (GameManager.Instance.currentState == GameState.Paused)
                {
                    GameManager.Instance.ResumeGame();
                }
            }
        }
    }
    
    void Jump()
    {
        if (carRigidbody != null && isGrounded)
        {
            // Salto más potente y espectacular
            Vector3 jumpVector = Vector3.up * jumpForce;
            
            // Añadir un poco de impulso hacia adelante para saltos más dinámicos
            jumpVector += transform.forward * (jumpForce * 0.3f);
            
            carRigidbody.AddForce(jumpVector, ForceMode.Impulse);
            
            // Marcar como no grounded temporalmente
            isGrounded = false;
            
            Debug.Log("¡Salto ejecutado!");
        }
    }
    
    void CheckGrounded()
    {
        // Raycast hacia abajo para verificar si está en el suelo
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);
        
        // Si no está en el suelo y está muy abajo, "morir"
        if (transform.position.y < -5f)
        {
            Die();
        }
    }
    
    void Die()
    {
        // Reiniciar posición del carrito
        transform.position = new Vector3(0, 2, 0);
        transform.rotation = Quaternion.identity;
        
        // Detener el rigidbody
        if (carRigidbody != null)
        {
            carRigidbody.linearVelocity = Vector3.zero;
            carRigidbody.angularVelocity = Vector3.zero;
        }
        
        Debug.Log("¡El carrito se salió de la pista!");
        
        // Trigger Game Over
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
    
    void OnDrawGizmos()
    {
        // Dibujar el raycast de ground check en el editor
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundCheckDistance);
    }
}