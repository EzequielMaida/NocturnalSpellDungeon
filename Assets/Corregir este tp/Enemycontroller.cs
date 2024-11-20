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

     public int vida = 100;
    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Morir();
        }
    }
    private void Morir()
    {
        Destroy(gameObject);
    }

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
                
            }
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

     public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TPDisparadorController jugador = other.GetComponent<TPDisparadorController>();
            if (jugador != null)
            {
                jugador.Morir();
            }
        }
    }
}