using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variables para configurar el enemigo
    public float detectionRadius = 5f;  // Radio de detección del jugador
    public float moveSpeed = 3f;         // Velocidad de movimiento del enemigo
    public float damageRadius = 1f;      // Radio para matar al jugador

    // Referencias
    private Transform player;            // Referencia al transform del jugador
    private bool isPlayerDetected = false;

    void Start()
    {
        // Buscar el jugador en la escena
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Verificar si el jugador está dentro del radio de detección
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // El jugador está detectado, seguirlo
            isPlayerDetected = true;
            
            // Mover al enemigo hacia el jugador
            transform.position = Vector3.MoveTowards(
                transform.position, 
                player.position, 
                moveSpeed * Time.deltaTime
            );

            // Rotar al enemigo para mirar al jugador
            transform.LookAt(player);

            // Verificar si está lo suficientemente cerca para matar
            if (distanceToPlayer <= damageRadius)
            {
                KillPlayer();
            }
        }
    }

    void KillPlayer()
    {
        // Obtener el script de salud/muerte del jugador
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            // Llamar al método para matar al jugador
            playerHealth.Die();
        }
        else
        {
            Debug.LogError("No se encontró el script de salud del jugador");
        }
    }

    // Método opcional para dibujar el radio de detección en el editor
    void OnDrawGizmosSelected()
    {
        // Dibujar radio de detección en color verde
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Dibujar radio de muerte en color rojo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}

// Script adicional para el jugador (PlayerHealth)
public class PlayerHealth : MonoBehaviour
{
    public void Die()
    {
        // Lógica para matar al jugador
        Debug.Log("¡El jugador ha muerto!");
        
        // Puedes agregar aquí:
        // - Reiniciar el nivel
        // - Mostrar pantalla de Game Over
        // - Desactivar el jugador
        gameObject.SetActive(false);
    }
}