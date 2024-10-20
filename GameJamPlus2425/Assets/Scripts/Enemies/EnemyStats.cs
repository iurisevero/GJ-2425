using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GJ.Bullet;


namespace GJ.AI{
    public class EnemyStats : MonoBehaviour
    {
        public EnemyData enemyData;
        
        Material material;
        SpriteRenderer spr;
        
        [SerializeField] public float currentMoveSpeed;
        [SerializeField] public float currentAcceleration;
        [SerializeField] public float currentKnockbackRes;
        [SerializeField] public float currentHealth;
        
        float damageFlashDuration = 0.1f;
        float damageFlashCooldown = 0f;
        float deathFadeTime = 0.75f;

        public bool isDead = false;

        private float flashTimer; //used for damageFlashCooldown

        private void OnEnable()
        {   

            spr = GetComponentInChildren<SpriteRenderer>();
            currentMoveSpeed = enemyData.MoveSpeed;
            currentAcceleration = enemyData.Acceleration;
            currentHealth = enemyData.MaxHealth;

            isDead = false;
            material = spr.material;
            material.SetFloat("_FlashAmount", 0);
        }

        IEnumerator DamageFlash(){
            material.SetFloat("_FlashAmount", 1);
            yield return new WaitForSeconds(damageFlashDuration);
            material.SetFloat("_FlashAmount", 0);
        }

        void Update(){
            //decrease flash timer if flash happend
            if(flashTimer > 0){
                flashTimer -= Time.deltaTime;
            }
        }    

        public void TakeDamage(float dmg){
            currentHealth -= dmg;
            AudioManager.Instance.Play("EnemyDamage");

            if (flashTimer <= 0 ){
                StartCoroutine(DamageFlash());
                flashTimer = damageFlashCooldown;
            }

            if(currentHealth <= 0){
                Die();
                StartCoroutine(KillFade());
            }

            
        }

        IEnumerator KillFade(){
            WaitForEndOfFrame w = new WaitForEndOfFrame();
            
            float t = 0, orig = material.GetFloat("_Fade");

            SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>(true); //gets any child sprite from enemy

            while (t < deathFadeTime) {
                yield return w;
                t += Time.deltaTime;
                
                material.SetFloat("_Fade", ( 1 - t / deathFadeTime) * orig);
                float fade = material.GetFloat("_Fade");
                foreach(SpriteRenderer renderer in childRenderers) {
                    renderer.material =  material;
                    renderer.material.SetFloat("_Fade", fade);
                }
            }

            gameObject.SetActive(false);
            
            foreach(SpriteRenderer renderer in childRenderers) {
                renderer.material =  material;
                renderer.material.SetFloat("_Fade", orig);
            }
        }

        public void Die(){
            isDead = true;
        }

        public SpawnRecipe CreateRecipe(GunData gunData){
			SpawnRecipe recipe = new SpawnRecipe();
			recipe.bulletResource = gunData.bulletResource;
			recipe.numberOfBullets = gunData.numberOfBullets;
			recipe.bulletSpeed = gunData.bulletSpeed;
            recipe.lifeTime = gunData.lifeTime;
            recipe.angleVelocity = gunData.angleVelocity;
            recipe.damage = gunData.damage;
            recipe.type =gunData.type;
			recipe.spread = gunData.spread;
			recipe.isRandom = gunData.isRandom;
			return recipe;
		}


    }
}    