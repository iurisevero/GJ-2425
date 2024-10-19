using System;
using UnityEngine;

namespace GJ.Bullet
{

	public class SpawnRecipe
	{
        public GameObject bulletResource;

        public int numberOfBullets;

		public float bulletSpeed;

		public float lifeTime;

		public float angleVelocity;

		public float damage;

        public float spread;

         public bool isRandom;

        public BulletType type;
	}
}