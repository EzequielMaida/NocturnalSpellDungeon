using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChecker : MonoBehaviour
{
    // Nombre de la escena de victoria (cambia esto según tu configuración)
    public string victorySceneName = "Ganaste";

    void FixedUpdate()
    {
        // Busca todos los objetos con el tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Si no quedan enemigos, cambia a la escena de victoria
        if (enemies.Length == 0)
        {
            // Opcional: Puedes agregar una pequeña demora si lo deseas
            // StartCoroutine(ChangeSceneWithDelay());
            
            // Cambio directo de escena
            SceneManager.LoadScene(victorySceneName);
        }
    }

    // Opcional: Método con corrutina si quieres agregar un pequeño retraso
    /*
    IEnumerator ChangeSceneWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Espera medio segundo
        SceneManager.LoadScene(victorySceneName);
    }
    */
}