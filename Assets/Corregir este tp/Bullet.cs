using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10f;

    void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject.name);
        Destroy(gameObject);

    }
}
