using UnityEngine;

namespace GJ.Bullet{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GunData", order = 1)]
    public class GunData : ScriptableObject
    {
        
        [Header("Bullet Settings")]

        [SerializeField] public GameObject bulletResource;

        [SerializeField] public float spread = 0f;

        [SerializeField] public int numberOfBullets = 1;

        [SerializeField] public bool isRandom = false;

        [SerializeField] public float bulletSpeed = 1f;

        [SerializeField] public Vector2 bulletVelocity = Vector2.zero;

        [SerializeField] public float lifeTime = 1;

        [SerializeField] public float range = 1;

        [SerializeField] public float angleVelocity = 100;

        [SerializeField] public float damage = 5;

        [SerializeField] public BulletType type;

    }
}