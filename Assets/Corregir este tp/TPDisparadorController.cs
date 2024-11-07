using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TPDisparadorController : MonoBehaviour
{
    [Header("Cinemachine")]
    [Tooltip("La camara para apuntar")]
    public CinemachineVirtualCamera aimCamera;
    public int prioridadApuntando = 5;

    public LayerMask aimColliderMask = new LayerMask();

    public Animator animator;
    public TPInput input;
    public GameObject crosshair;

    public Transform aimTarget;

    [Header("Disparo")]
    public GameObject balaFuegoPrefab; // Prefab de la bala de fuego
    public GameObject balaHieloPrefab; // Prefab de la bala de hielo
    public Transform balaSpawnPosition;

    [Header("Sistema de Maná")]
    public PlayerMana playerMana; // Referencia al componente PlayerMana
    public int manaFuego = 20; // Coste de maná para bala de fuego
    public int manaHielo = 15; // Coste de maná para bala de hielo

    public Rig aimRig;

    private bool isFuegoBala = true; // True = bala de fuego, False = bala de hielo
    private int aimLayerIndex;

    private void Start()
    {
        aimLayerIndex = animator.GetLayerIndex("Aim");
    }

    private void Update()
    {
        // Cambiar tipo de bala con Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isFuegoBala = !isFuegoBala;
            // Opcional: Mostrar mensaje en consola del tipo de bala actual
            Debug.Log("Tipo de bala cambiado a: " + (isFuegoBala ? "Fuego" : "Hielo"));
        }

        if (input.aim)
            Aiming();
        else
            NoAiming();
    }

    private void Aiming()
    {
        aimCamera.Priority = prioridadApuntando;

        crosshair.SetActive(true);
        aimTarget.gameObject.SetActive(true);

        var weight = Mathf.Lerp(aimRig.weight, 1f, Time.deltaTime * 10f);
        aimRig.weight = weight;
        animator.SetLayerWeight(aimLayerIndex, weight);

        MoveAimTarget();
    }

    private void NoAiming()
    {
        aimCamera.Priority = 0;

        crosshair.SetActive(false);
        aimTarget.gameObject.SetActive(false);

        var weight = Mathf.Lerp(aimRig.weight, 0f, Time.deltaTime * 10f);
        aimRig.weight = weight;
        animator.SetLayerWeight(aimLayerIndex, weight);
    }

    private void MoveAimTarget()
    {
        var mouseWorldPosition = Vector3.zero;

        var screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderMask))
        {
            aimTarget.position = hit.point;
            mouseWorldPosition = hit.point;
        }

        var aimDirection = (mouseWorldPosition - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 1f);

        // Sistema de disparo con maná
        if (input.shoot)
        {
            int manaRequired = isFuegoBala ? manaFuego : manaHielo;
            
            if (playerMana.HasEnoughMana(manaRequired))
            {
                
                var balaDirection = (mouseWorldPosition - balaSpawnPosition.position).normalized;
                
                // Seleccionar el prefab correcto basado en isFuegoBala
                GameObject balaToSpawn = isFuegoBala ? balaFuegoPrefab : balaHieloPrefab;
                
                Instantiate(
                    balaToSpawn, 
                    balaSpawnPosition.position,
                    Quaternion.LookRotation(balaDirection, Vector3.up)
                );

                // Consumir el maná
                playerMana.UseMana(manaRequired);
            }
        }
    }
}