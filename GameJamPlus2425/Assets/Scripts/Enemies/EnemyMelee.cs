using System.Collections;
using UnityEngine;

namespace GJ.AI
{
    public class EnemyMelee : AIBehaviour
    {
        [SerializeField] private GameObject attackAreaPrefab;  // Prefab for the AttackArea
        [SerializeField] private Transform firepoint;  // Firepoint to position the attack area


        protected override void OnEnable(){
            base.OnEnable();
            isAttacking = false;
        }

        private void OnDisable() {
        }

        private void FixedUpdate()
        {
            if (enemyStats.isDead)
            {
                agent.velocity = Vector2.zero;
                
                return;
            }

            if (player != null)
            {      
                Vector3 playerPosition = player.position;
                float distToPlayer = (playerPosition - myTransform.position).magnitude;

                if (!(distToPlayer >= minDistanceMovement && (isAttackAndMove || !isAttacking)))
                {   
                    DecelerateMomentum();
                }
                else
                {
                    UpdateMovementStats();
                }


                if (distToPlayer <= minDistanceAttack && !isAttacking)
                {
                   // StartCoroutine(Attack());
                }
                SetMovement();  
            }  

        }


        

        private IEnumerator Attack()
        {
            SetAttacking(true);  
            isAiming = true;
            yield return new WaitForSeconds(enemyStats.enemyData.attackDelay);

            isAiming = false;
            yield return new WaitForSeconds(enemyStats.enemyData.aimingDelay);

            //TODO Attack

            yield return new WaitForSeconds(enemyStats.enemyData.afterAttackDelay);
            SetAttacking(false);
        }


        private void SetAttacking(bool state)
        {
            isAttacking = state;
        }
    }
}