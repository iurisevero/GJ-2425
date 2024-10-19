using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ.Bullet{
    public enum BulletType{
        enemyBullet,
        playerBullet
    }

    public class BulletBehaviour : MonoBehaviour
    {

        [HideInInspector]public Vector2 velocity;
        [HideInInspector]public float speed;
        [HideInInspector]public float rotation;
        [SerializeField]public float lifeTime;
        [SerializeField]public BulletType type;
        [SerializeField]public float timer;
        [HideInInspector]public int damage;
        [HideInInspector]public float angleVelocity;

        private SpriteRenderer bulletSpr;
        public Vector2 direction;

        private Rigidbody rb;

        void OnEnable()
        {
            bulletSpr = GetComponent<SpriteRenderer>();
            timer = lifeTime;
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            CalculateAngle();
            Movement();
            
            timer -= Time.deltaTime;
            if (timer <= 0) CheckTimerEnd(); 
        }

        public void ChangeDirection(Vector3 target){
            direction = (target - transform.position).normalized;
        }

        void Movement(){
            velocity = Vector2.Lerp(velocity, direction, Time.deltaTime * angleVelocity);
            rb.velocity = transform.right * speed;
        }

        void CalculateAngle(){
            if (velocity != Vector2.zero){
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

                if(bulletSpr != null)
                    bulletSpr.flipY = Mathf.Abs(angle) > 90;

                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = targetRotation;
            }
        }

        void OnTriggerEnter(Collider col) {
            if(col.CompareTag("Player") && type == BulletType.enemyBullet){
                //TODO DEALS DAMAGE PLAYER
                Debug.Log("Player receives damage");
                CheckCollisionEnd();
            }

            else if(col.CompareTag("Enviroment")){
                CheckCollisionEnd();
            }



        }

        public void CheckTimerEnd(){
            switch(type){
                case BulletType.enemyBullet: 
                    gameObject.SetActive(false);
                    break; 
                default:
                    gameObject.SetActive(false);
                    break;
 
            }
        }

        public void CheckCollisionEnd(){
            switch(type){
                case BulletType.enemyBullet: 
                    gameObject.SetActive(false);
                    break; 
                default:
                    gameObject.SetActive(false);
                    break; 
            }
        }

    }
}
