using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 100;
    public int currentMana;

    [Header("UI Elements")]
    public TextMeshProUGUI manaText;

    private void Start()
    {
        currentMana = maxMana;
        UpdateManaUI();
    }

    public bool HasEnoughMana(int amount)
    {
        return currentMana >= amount;
    }

    public void UseMana(int amount)
    {
        if (HasEnoughMana(amount))
        {
            currentMana -= amount;
            UpdateManaUI();
            Debug.Log("Maná usado: " + amount + ". Maná restante: " + currentMana);
        }
        else
        {
            Debug.Log("No hay suficiente maná!");
        }
    }
    
    public void RestoreMana(int amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        UpdateManaUI();
        Debug.Log("Maná restaurado: " + amount + ". Maná actual: " + currentMana);
    }

    private void UpdateManaUI()
    {
        if (manaText != null)
        {
            manaText.text = currentMana + " / " + maxMana;
        }
    }
}