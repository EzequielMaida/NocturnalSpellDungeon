using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 100;
    public int currentMana;
    public float manaRegenRate = 1f;
    public float manaRegenDelay = 2f;

    [Header("UI Elements")]
    public Image manaBarFill;
    public TextMeshProUGUI manaText;

    private float lastManaUseTime;

    private void Start()
    {
        currentMana = maxMana;
        UpdateManaUI();
    }

    private void Update()
    {
        if (Time.time > lastManaUseTime + manaRegenDelay && currentMana < maxMana)
        {
            RegenerateMana();
        }
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
            lastManaUseTime = Time.time;
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

    private void RegenerateMana()
    {
        currentMana = Mathf.Min(currentMana + (int)(manaRegenRate * Time.deltaTime), maxMana);
        UpdateManaUI();
    }

    private void UpdateManaUI()
    {
        if (manaBarFill != null)
        {
            manaBarFill.fillAmount = (float)currentMana / maxMana;
        }

        if (manaText != null)
        {
            manaText.text = currentMana + " / " + maxMana;
        }
    }
}