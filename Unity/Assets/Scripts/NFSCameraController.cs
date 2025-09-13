using UnityEngine;

public class NFSCameraController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // El carrito a seguir
    
    [Header("Camera Position")]
    public Vector3 offset = new Vector3(0, 2.5f, -6f); // Posición relativa al carrito
    public float height = 2f; // Altura adicional
    
    [Header("Camera Behavior")]
    public float followSpeed = 10f; // Velocidad de seguimiento
    public float rotationSpeed = 5f; // Velocidad de rotación
    public float lookAheadDistance = 5f; // Qué tan adelante mirar
    
    [Header("Smoothing")]
    public float positionDamping = 3f;
    public float rotationDamping = 2f;
    
    private Vector3 velocity;
    
    void Start()
    {
        // Si no se asignó target, buscar el Prometheus
        if (target == null)
        {
            GameObject prometheus = GameObject.Find("Prometheus");
            if (prometheus != null)
                target = prometheus.transform;
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        FollowTarget();
        LookAtTarget();
    }
    
    void FollowTarget()
    {
        // Calcular la posición deseada detrás del carrito
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        desiredPosition.y += height;
        
        // Suavizar el movimiento de la cámara
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / positionDamping);
    }
    
    void LookAtTarget()
    {
        // Punto hacia donde mirar (un poco adelante del carrito)
        Vector3 lookAtPoint = target.position + target.forward * lookAheadDistance;
        lookAtPoint.y = target.position.y + 1f; // Ajustar altura del punto de mira
        
        // Calcular la rotación deseada
        Vector3 direction = lookAtPoint - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(direction);
        
        // Suavizar la rotación
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationDamping * Time.deltaTime);
    }
    
    void OnDrawGizmos()
    {
        if (target != null)
        {
            // Dibujar líneas de debug
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(target.position + target.TransformDirection(offset), 0.5f);
            
            Gizmos.color = Color.red;
            Vector3 lookAtPoint = target.position + target.forward * lookAheadDistance;
            Gizmos.DrawWireSphere(lookAtPoint, 0.3f);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, lookAtPoint);
        }
    }
}