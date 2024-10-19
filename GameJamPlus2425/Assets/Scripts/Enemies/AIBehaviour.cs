using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace GJ.A
{
    public class AIBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("PlayerCenter")?.transform;
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.enabled = true;

        }

        protected bool IsPlayerVisible()
        {
            
        }

        protected virtual void UpdateMovementStats()
        {
            agent.speed = Mathf.MoveTowards(agent.speed, enemyStats.currentMoveSpeed, acelerationRate * Time.fixedDeltaTime);
        }

        protected virtual void DecelerateMomentum()
        {
            agent.speed = Mathf.MoveTowards(agent.speed, 0, acelerationRate * Time.fixedDeltaTime);
        }

        protected virtual void SetMovement()
        {
            if (Vector3.Distance(myTransform.position, lastPlayerPosition) > 0.1f)
            {
                agent.SetDestination(player.position);
                lastPlayerPosition = player.position;
            }

            //TODO make die and disable navmesh
            //if (agent != null && enemyStats.isDead)
            //{
            //    agent.enabled = false;
            //}

        }

        protected Transform myTransform;

        [Header("Target")]
        [SerializeField] protected Transform player;
        [SerializeField] protected float maxRayDistance = 15f;

        [Header("Controllers")]
        [SerializeField] protected EnemyData data;
        [SerializeField] protected float acelerationRate = 0.1f;
        [SerializeField] public float minDistanceMovement;
        [SerializeField] public float minDistanceAttack;
        [SerializeField] public bool isAttackAndMove = false;

        [Header("Timers")]
        //[SerializeField] public float aimingDelay;
        //[SerializeField] public float attackDelay;
        //[SerializeField] public float afterAttackDelay;

        public bool isAiming { get;  set; }  
        public bool isAttacking { get;  set; }  

        protected NavMeshAgent agent;

        protected Rigidbody2D rb;
        protected RaycastHit2D hit;
        protected bool isFlip;
        protected bool isPlayerVisible;
        protected bool lastIsPlayerVisible;

        protected float raycastCooldown = 0.2f;
        protected float lastRaycastTime;
    }
}