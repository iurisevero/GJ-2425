using System.Collections;
using UnityEngine;
using GJ.Bullet;

namespace GJ.AI
{
    public class EnemyShoot : AIBehaviour
    {
        [SerializeField] public GameObject weapon;
        [SerializeField] public GunData gunData;


        protected override void OnEnable()
        {
            base.OnEnable();
            isAiming = true;
            isAttacking = false;
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

                //isPlayerVisible = IsPlayerVisible();
                isPlayerVisible = true;
                if (isPlayerVisible && !(distToPlayer >= minDistanceMovement && (isAttackAndMove || !isAttacking)))
                {   
                    DecelerateMomentum();
                }
                else
                {
                    UpdateMovementStats();
                }

                AimWeapon();

                if (distToPlayer <= minDistanceAttack && isPlayerVisible && !isAttacking)
                {
                    StartCoroutine(Attack());
                }
                SetMovement();  
            }  
        }

private void AimWeapon()
{
    if (isAiming && player != null)
    {
        // Calcular a direção do jogador em relação à arma
        Vector3 directionToPlayer = (player.position - weapon.transform.position).normalized;

        // Calcular os ângulos de rotação desejados para y e z
        float angleY = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        float angleZ = Mathf.Asin(directionToPlayer.y) * Mathf.Rad2Deg;

        // Definir a rotação com x fixo em 0 e aplicar localmente
        Quaternion localRotation = Quaternion.Euler(0, angleY, angleZ);
        weapon.transform.localRotation = localRotation;
    }
}






        private IEnumerator Attack(){
            SetAttacking(true); 
            SetAiming(true);
            yield return new WaitForSeconds(enemyStats.enemyData.aimingDelay);   

            SetAiming(false); 
            yield return new WaitForSeconds(enemyStats.enemyData.attackDelay); 

            if(!enemyStats.isDead){
                BulletSpawner bulletSpawner = weapon.GetComponentInChildren<BulletSpawner>();

                if (bulletSpawner != null)
                {
                    if(enemyStats != null)
                    bulletSpawner.SpawnBullets(enemyStats.CreateRecipe(gunData));
                }
                
                SetAiming(true);

                yield return new WaitForSeconds(enemyStats.enemyData.afterAttackDelay); 
                SetAttacking(false);
            }
            SetAiming(true);
            SetAttacking(false);
            
        }

        private void SetAttacking(bool state)
        {
            isAttacking = state;
        }

        private void SetAiming(bool state)
        {
            isAiming = state;
        }
    }
}