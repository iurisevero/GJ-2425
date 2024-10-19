using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


namespace GJ.AI{
    public class EnemyStats : MonoBehaviour
    {
        public EnemyData enemyData;


        
        [SerializeField] public float currentMoveSpeed;
        [SerializeField] public float currentAcceleration;
        [SerializeField] public float currentKnockbackRes;
        [SerializeField] public float currentHealth;
        

        public bool isDead = false;

        private void OnEnable()
        {   
            currentMoveSpeed = enemyData.MoveSpeed;
            currentAcceleration = enemyData.Acceleration;
            currentHealth = enemyData.MaxHealth;

            isDead = false;


        }

        public void TakeDamage(float dmg){

            currentHealth -= dmg;

            if(currentHealth <= 0){
                Die();

            }

            
        }

        public void Die(){
            isDead = true;
        }


    }
}    