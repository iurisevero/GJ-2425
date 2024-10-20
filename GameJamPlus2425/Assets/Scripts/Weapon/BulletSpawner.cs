using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ.Bullet 
{
    public class BulletSpawner : MonoBehaviour
    {
        public float delayBetweenShots = 0.1f; // Nova constante para o atraso entre tiros
        Vector3[] rotations;

        // Generate a random rotation within the spread range for a single bullet
        public Vector3 RandomRotation(SpawnRecipe recipe)
        {
            float randomX = Random.Range(-recipe.spread / 2, recipe.spread / 2);
            float randomY = Random.Range(-recipe.spread / 2, recipe.spread / 2);
            float randomZ = Random.Range(-recipe.spread / 2, recipe.spread / 2);
            return new Vector3(randomX, randomY, randomZ);
        }

        // Generate a distributed rotation within the spread range for a single bullet
        public Vector3 DistributedRotation(SpawnRecipe recipe)
        {
            float rotationValue = Mathf.Lerp(-recipe.spread / 2, recipe.spread / 2, 0.5f); // Use fixed value since only one bullet is needed
            return new Vector3(rotationValue, rotationValue, rotationValue);
        }

        public GameObject[] SpawnBullets(SpawnRecipe recipe)
        {
            GameObject[] spawnedBullets = new GameObject[recipe.numberOfBullets];

            // Start coroutine to spawn multiple bullets with delay
            StartCoroutine(SpawnMultipleBulletsWithDelay(recipe, spawnedBullets));
            return spawnedBullets;
        }

        private IEnumerator SpawnMultipleBulletsWithDelay(SpawnRecipe recipe, GameObject[] spawnedBullets)
        {
            for (int i = 0; i < recipe.numberOfBullets; i++)
            {
                Vector3 rotation;

                if (recipe.isRandom)
                {
                    rotation = RandomRotation(recipe);
                }
                else
                {
                    rotation = DistributedRotation(recipe);
                }

                // Spawn a single bullet
                yield return StartCoroutine(SpawnBulletWithDelay(recipe, rotation, spawnedBullets, i));

                // Wait for the delay between shots before spawning the next bullet
                yield return new WaitForSeconds(delayBetweenShots);
            }
        }

        private IEnumerator SpawnBulletWithDelay(SpawnRecipe recipe, Vector3 rotation, GameObject[] spawnedBullets, int index)
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
                b.speed = recipe.bulletSpeed;
                b.lifeTime = recipe.lifeTime;
                b.timer = b.lifeTime;
                b.angleVelocity = recipe.angleVelocity;
                b.damage = (int)recipe.damage;
                b.type = recipe.type;

                // Ensure rotation is valid before applying it
                if (float.IsNaN(rotation.x) || float.IsNaN(rotation.y) || float.IsNaN(rotation.z))
                {
                    rotation = Vector3.zero; // Fallback to avoid errors
                }

                // Apply spread to the direction using rotation
                Vector3 directionWithSpread = Quaternion.Euler(rotation) * transform.forward;
                b.ChangeDirection(directionWithSpread);
            }
            else
            {
                Debug.LogError("Bullet behaviour component is missing.");
            }
        }
    }
}
