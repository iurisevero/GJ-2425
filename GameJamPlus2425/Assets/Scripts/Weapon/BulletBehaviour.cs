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

        [HideInInspector]public float speed;
        [HideInInspector]public float rotation;
        [SerializeField]public float lifeTime;
        [SerializeField]public BulletType type;
        [SerializeField]public float timer;
        [HideInInspector]public int damage;
        [HideInInspector]public float angleVelocity;

        
        private SpriteRenderer bulletSpr;
        public Vector3 direction;

        private Rigidbody rb;

        void OnEnable()
        {
            bulletSpr = GetComponent<SpriteRenderer>();
            timer = lifeTime;
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            Movement();
            
            timer -= Time.deltaTime;
            if (timer <= 0) CheckTimerEnd(); 
        }

        public void ChangeDirection(Vector3 newDirection)
        {
            direction = newDirection.normalized;
            transform.forward = direction; 
        }

        void Movement()
        {
            rb.velocity = direction * speed; 
        }


        void OnTriggerEnter(Collider col) {
            if(col.CompareTag("Player") && type == BulletType.enemyBullet){
                Player player = col.GetComponentInParent<Player>();
                player.TakeDamage(damage);
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
