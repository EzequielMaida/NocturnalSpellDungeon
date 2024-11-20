using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerSalud : MonoBehaviour
{
    public int maxSalud = 100;
    public int currentSalud;

    [Header("UI Elements")]
    public TextMeshProUGUI saludText;

    private void Start()
    {
        currentSalud = maxSalud;
        UpdateManaUI();
    }

    public bool HasEnoughSalud(int amount)
    {
        return currentSalud >= amount;
    }

    public void UseMana(int amount)
    {
        if (HasEnoughSalud(amount))
        {
            currentSalud -= amount;
            UpdateManaUI();
            Debug.Log("Salud usado: " + amount + ". Salud restante: " + currentSalud);
        }
        else
        {
            Debug.Log("No hay suficiente Salud!");
        }
    }
    
    public void RestoreMana(int amount)
    {
        currentSalud = Mathf.Min(currentSalud + amount, maxSalud);
        UpdateManaUI();
        Debug.Log("Salud restaurado: " + amount + ". Salud actual: " + currentSalud);
    }

    private void UpdateManaUI()
    {
        if (saludText != null)
        {
            saludText.text = currentSalud + " / " + maxSalud;
        }
    }
    public void Die()
    {
        SceneManager.LoadScene("Playground");
    }
}