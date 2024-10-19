using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace GJ.AI
{
    public class AIBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            enemyStats = GetComponent<EnemyStats>();
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.enabled = true;

        }

        protected bool IsPlayerVisible()
        {
            return false;
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

        //[Header("Timers")]
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