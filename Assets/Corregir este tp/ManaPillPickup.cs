using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPillPickup : MonoBehaviour
{
    public int manaAmount = 20; // Cantidad de maná que restaura la píldora
    public float rotationSpeed = 50f; // Velocidad de rotación de la píldora

    private void Update()
    {
        // Hace que la píldora gire constantemente
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMana playerMana = other.GetComponent<PlayerMana>();
            if (playerMana != null)
            {
                playerMana.RestoreMana(manaAmount);
                Debug.Log("Maná restaurado: " + manaAmount);
                Destroy(gameObject); // Destruye la píldora después de ser recogida
            }
        }
    }
}