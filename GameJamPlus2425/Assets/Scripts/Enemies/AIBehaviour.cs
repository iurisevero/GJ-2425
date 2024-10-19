using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace GJ.AI
{
    public class AIBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            myTransform = GetComponent<Transform>();
            enemyStats = GetComponent<EnemyStats>();
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            agent = GetComponent<NavMeshAgent>();
            agent.angularSpeed = enemyStats.enemyData.AngularSpeed;
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.enabled = true;

        }



        protected bool IsPlayerVisible()
        {
            float distanceToPlayer = Vector3.Distance(myTransform.position, player.position);
            if (Time.time > lastRaycastTime + raycastCooldown && distanceToPlayer <= maxRayDistance)
            {
                Vector3 directionToPlayer = player.position - myTransform.position;
                float maxRaycastDistance = 30f; 
                int layerMask = ~(LayerMask.GetMask("Bullets") | LayerMask.GetMask("Enemy"));
                hit = Physics2D.Raycast(myTransform.position, directionToPlayer, maxRaycastDistance, layerMask);

                lastRaycastTime = Time.time;

                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    lastIsPlayerVisible = true;
                    return true;
                }
                else
                {
                    lastIsPlayerVisible = false;
                    return false;
                }
            }
            return lastIsPlayerVisible;
        }

        protected virtual void UpdateMovementStats()
        {
            agent.speed = Mathf.MoveTowards(agent.speed, enemyStats.currentMoveSpeed, enemyStats.currentAcceleration * Time.fixedDeltaTime);
        }

        protected virtual void DecelerateMomentum()
        {
            agent.speed = Mathf.MoveTowards(agent.speed, 0, enemyStats.currentAcceleration * Time.fixedDeltaTime);
        }

        protected virtual void SetMovement()
        {
            if (Vector3.Distance(myTransform.position, lastPlayerPosition) > 0.1f)
            {
                agent.SetDestination(player.position);
                lastPlayerPosition = player.position;
            }


            if (agent != null && enemyStats.isDead)
            {
                agent.enabled = false;
            }

        }

        protected Transform myTransform;
        protected EnemyStats enemyStats;
        protected Vector3 lastPlayerPosition;


        [Header("Target")]
        [SerializeField] protected Transform player;
        [SerializeField] protected float maxRayDistance = 15f;

        [Header("Controllers")]
        [SerializeField] protected float acelerationRate = 0.1f;
        [SerializeField] public float minDistanceMovement;
        [SerializeField] public float minDistanceAttack;
        [SerializeField] public bool isAttackAndMove = false;

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