using System.Collections;
using System.Collections.Generic;
using GJ.AI;
using UnityEngine;
using UnityEngine.AI;

public class SpecialHomingBullet : MonoBehaviour
{
    private Rigidbody rb;
    private NavMeshAgent agent;

    public Transform targetEnemy;
    public float bulletVelocity = 10f;
    public float angularSpeed = 100f;
    public float moveSpeed = 2f;
    public float acceleration = 20f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = angularSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.enabled = true;

        targetEnemy = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.speed = Mathf.MoveTowards(agent.speed, moveSpeed, acceleration * Time.fixedDeltaTime);

        if(targetEnemy == null) {
            agent.Move(transform.forward.normalized);
        }
        else {
            agent.SetDestination(targetEnemy.position);
        }
    }

    public void CustomOnTriggerEnter(Collider other)
    {
        Debug.Log("Triggering " + other.name);
        if(targetEnemy != null) return;

        if(other.GetComponent<EnemyStats>() != null) {
            targetEnemy = other.transform;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Do damage
        Debug.Log("Do damage");
    }

    void OnDestroy()
    {
        agent.enabled = false;
    }
}
