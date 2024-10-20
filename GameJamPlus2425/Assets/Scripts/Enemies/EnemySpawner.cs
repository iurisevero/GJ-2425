using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GJ;
using GJ.AI;

namespace F{
    public class EnemySpawner : MonoBehaviour
    {
        [System.Serializable]
        public class EnemyGroup{
                
            public GameObject enemyPrefab;

            public int startDelay;

            [Tooltip("Number of seconds the game waits before trying to spawn more enemies from the session (up to the Max).")]
            public float spawnCD;
                    
            [Tooltip("Number of enemies from the session that are spawned at once, after every SpawnCD seconds, until they reach their Max number.")]
            public int numSpawn;

            [Tooltip("Number max of enemies.")]
            public int maximum;

            [Tooltip("Numspawns spawns the enemies together in the same spot.")]
            public bool isTogether = false;

            [Tooltip("The number of enemies of this type already spawned in this wave.")]
            [NonSerialized]
            public int spawnCount = 0; 

            [Tooltip("How much time has passed since last spawn(cooldown).")]
            [NonSerialized]
            public float timerSpawn = 0;
        }

        [System.Serializable]
        public class Wave{ 
 
            //already spawned enemies
            [NonSerialized]public int waveCount;

            [SerializeField] public List<EnemyGroup> enemyGroups = new List<EnemyGroup>();
            
        }

        [SerializeField] public Wave wave;


        [Header("Pool")]
        private  ObjectPooler objPooler;

        [Header("Spawner Attributes")]
        [SerializeField] private float spawnTogetherTimer = 0.25f; //timer between each enemy spawned in each enemy group 

        [Header("Spawner Positions")]
        public List<Transform> relativeSpawnPoints; //A list to store all the relative spawn point of enemies

        private bool canSpawn = true;

        private int lastSpawnIndex = -1;
        
        public static EnemySpawner Instance;

        
        private void Awake()
        {
            Instance = this;

        }

        private void Start(){
            this.objPooler = ObjectPooler.SharedInstance;

            PrepareWaveEnemies();

        }


        private void Update()
        {
            if(canSpawn) SpawnEnemies();
        }


        private void PrepareWaveEnemies() {
            foreach (var enemyGroup in wave.enemyGroups) {
                AddEnemiesToPool(enemyGroup.enemyPrefab);
            }
        }

        private int CountActiveObjects(string objectPoolTag)
		{
			int num = 0;
			List<GameObject> allPooledObjects = this.objPooler.GetAllPooledObjects(objectPoolTag);
			for (int i = 0; i < allPooledObjects.Count; i++)
			{
				if (allPooledObjects[i].activeInHierarchy)
				{
					num++;
				}
			}
			return num;
		}

        private void SpawnEnemies() {
            foreach (var enemyGroup in wave.enemyGroups ) {
                if(CountActiveObjects(enemyGroup.enemyPrefab.name) < enemyGroup.maximum){
                    SpawnEnemyGroup(enemyGroup);
                } 
            }   
        }

        private void SpawnEnemyGroup(EnemyGroup enemyGroup) {
            if (wave.waveCount >= enemyGroup.startDelay) {
                if (enemyGroup.timerSpawn >= enemyGroup.spawnCD) {
                    Vector2 spawnPosition = Vector2.zero;
                    float timerMultiplier = 0; // Used for timing if isTogether
                    bool isTogetherToggle = false;

                    for (int i = 0; i < enemyGroup.numSpawn; i++) {
                        int spawnIndex = GetValidSpawnIndex(lastSpawnIndex);

                        // If isTogether is true and isTogetherToggle is set, reuse the last spawn index
                        if (enemyGroup.isTogether && isTogetherToggle) {
                            spawnIndex = lastSpawnIndex;
                        }

                        // Validate the spawn index
                        if (spawnIndex == -1) {
                            Debug.LogWarning("No valid spawn point found!");
                            return;
                        }

                        // Get the spawn position from the valid index
                        spawnPosition = relativeSpawnPoints[spawnIndex].position;
                        isTogetherToggle = true;
                        lastSpawnIndex = spawnIndex;

                        // Handle timing for enemies spawning together
                        if (enemyGroup.isTogether) {
                            timerMultiplier++;
                        }

                        // Spawn the enemy with a delay if isTogether is true
                        StartCoroutine(SpawnEnemy(enemyGroup, spawnPosition, timerMultiplier * spawnTogetherTimer));

                        // Update counts
                        enemyGroup.spawnCount++;
                        wave.waveCount++;
                    }

                    // Reset the cooldown after spawning
                    enemyGroup.timerSpawn = 0;
                }

                // Increment the spawn timer
                enemyGroup.timerSpawn += Time.deltaTime;
            }
        }

        private int GetValidSpawnIndex(int lastSpawnIndex) {
            List<int> validIndices = new List<int>();
            for (int i = 0; i < relativeSpawnPoints.Count; i++) {
                if (i != lastSpawnIndex && i != (lastSpawnIndex + 1) % relativeSpawnPoints.Count && i != (lastSpawnIndex - 1 + relativeSpawnPoints.Count) % relativeSpawnPoints.Count) {
                    validIndices.Add(i);
                }
            }

            if (validIndices.Count > 0) {
                return validIndices[UnityEngine.Random.Range(0, validIndices.Count)];
            }

            return 0;
        }

        // Coroutine para spawnar um inimigo usando o Object Pooler
        private IEnumerator SpawnEnemy(EnemyGroup enemyGroup, Vector2 spawnPosition, float timer) {
            yield return new WaitForSeconds(timer); // Wait for timer

            GameObject go = objPooler.GetPooledObject(enemyGroup.enemyPrefab.name);
            if (go != null) {
                go.transform.position = spawnPosition;
                go.transform.rotation = Quaternion.identity;
                go.SetActive(true);
                
            } else {
                // Handle the case where no object is available and shouldExpand is false
                Debug.Log("No available enemies in pool and expansion not allowed.");
            }
        }

        public void AddEnemiesToPool(GameObject enemyPrefab, int amount = 10, bool shouldExpand = true) {
            string tag = enemyPrefab.name;
            objPooler.AddObject(tag, enemyPrefab, amount, shouldExpand);
        }

    }
}