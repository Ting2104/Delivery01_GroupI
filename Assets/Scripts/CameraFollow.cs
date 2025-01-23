using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 minBounds; // Límite inferior izquierdo (x, y)
    [SerializeField] private Vector2 maxBounds; // Límite superior derecho (x, y)
    [SerializeField] private float smoothSpeed = 0.125f; // Velocidad de interpolación

    private Vector3 _offset; 

    void Start()
    {
        if (player != null)
        {
            _offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + _offset;

            // Limitar la posición deseada dentro de los límites
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

            // Interpolación para movimiento suave
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}