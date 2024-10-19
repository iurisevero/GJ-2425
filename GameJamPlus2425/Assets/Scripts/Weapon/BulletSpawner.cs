using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ.Bullet 
{
    public class BulletSpawner : MonoBehaviour
    {
        float[] rotations;

        // Select a random rotation from min to max for each bullet
        public float[] RandomRotations(SpawnRecipe recipe)
        {
            float maxRotation = -recipe.spread / 2;
            float minRotation = recipe.spread / 2;
            for (int i = 0; i < recipe.numberOfBullets; i++)
            {
                rotations[i] = Random.Range(minRotation, maxRotation);
            }
            return rotations;
        }

        // This will set random rotations evenly distributed between the min and max Rotation.
        public float[] DistributedRotations(SpawnRecipe recipe)
        {
            float maxRotation = -recipe.spread / 2;
            float minRotation = recipe.spread / 2;

            for (int i = 0; i < recipe.numberOfBullets; i++)
            {
                var fraction = (float)i / ((float)recipe.numberOfBullets - 1);
                var difference = maxRotation - minRotation;
                var fractionOfDifference = fraction * difference;
                var rotation = fractionOfDifference + minRotation;
                // Verifica se a rotação é NaN
                if (float.IsNaN(rotation)) rotation = 0f; // Atribui 0 se for NaN

                rotations[i] = rotation;
            }

            return rotations;
        }

        public GameObject[] SpawnBullets(SpawnRecipe recipe)
        {
            rotations = new float[recipe.numberOfBullets];
            if (recipe.isRandom)
            {
                RandomRotations(recipe);
            }
            else
            {
                DistributedRotations(recipe);
            }

            // Spawn Bullets
            GameObject[] spawnedBullets = new GameObject[recipe.numberOfBullets];
            for (int i = 0; i < recipe.numberOfBullets; i++)
            {
                StartCoroutine(SpawnBulletWithDelay(recipe, i, spawnedBullets));
            }
            return spawnedBullets;
        }

        private IEnumerator SpawnBulletWithDelay(SpawnRecipe recipe, int index, GameObject[] spawnedBullets)
        {
            float spawnDelay = Random.Range(0f, 0.05f);
            yield return new WaitForSeconds(spawnDelay);

            GameObject bullet = BulletManager.GetBulletFromPoolWithType(recipe.type);
            if (bullet == null)
            {
                if (recipe.bulletResource != null)
                {
                    bullet = Instantiate(recipe.bulletResource, transform.position, Quaternion.identity);
                    BulletManager.bullets.Add(bullet);
                    bullet.transform.SetParent(BulletManager.instance.transform);
                }
                else
                {
                    Debug.LogError("Bullet resource is not assigned.");
                    yield break;
                }
            }
            else
            {
                bullet.transform.localPosition = transform.position;
            }

            spawnedBullets[index] = bullet;

            var b = bullet.GetComponent<BulletBehaviour>();
            if (b != null)
            {
                b.rotation = rotations[index];
                b.speed = recipe.bulletSpeed;

                // Apply debuff to speed if there are multiple bullets
                if (recipe.numberOfBullets > 1)
                {
                    float speedDebuff = Random.Range(-3f, 0f);
                    b.speed += speedDebuff;
                    if(b.speed < 0)b.speed = 0;
                }

                b.velocity = Vector2.zero;
                b.lifeTime = recipe.lifeTime;
                b.timer = b.lifeTime;
                b.angleVelocity = recipe.angleVelocity;
                b.damage = (int)recipe.damage;
                b.type = recipe.type;


                // Convert direction vector to degrees
                float angleDegrees = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;

                // Add object's rotation to the converted degrees
                angleDegrees += b.rotation;

                // Convert degrees back to radians
                float angleRadians = angleDegrees * Mathf.Deg2Rad;

                // Calculate new velocity vector using sine and cosine
                b.velocity = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
                b.direction = b.velocity;
            }
            else
            {
                Debug.LogError("Bullet behaviour component is missing.");
            }

        }
    }
}