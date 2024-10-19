using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialPos;
    private bool bulletShooted = false;
    public float bulletVelocity = 10f;
    public float lifeTime = 1.25f;
    public float bulletRange = 30f;
    public int bulletDamage = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (bulletShooted) {
            rb.AddForce(transform.forward.normalized * bulletVelocity, ForceMode.Impulse);
            
            if(Mathf.Abs(Vector3.Distance(initialPos, transform.position)) > bulletRange) {
                CancelInvoke("Enqueue");
                Enqueue();
            }
        }
    }

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    private void Enqueue()
    {
        bulletShooted = false;
        Poolable p = gameObject.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit collision " + other.gameObject.name);
        CancelInvoke("Enqueue");
        Enqueue();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit trigger " + other.name);
        CancelInvoke("Enqueue");
        Enqueue();
    }

    public void ShootBullet() 
    {
        Invoke("Enqueue", lifeTime);
        bulletShooted = true;
        initialPos = transform.position;
    }
}
