using UnityEngine;

namespace GJ.AI{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyData", order = 1)]
    public class EnemyData : ScriptableObject
    {
        [Header("Default navmesh behaviour")]
        [SerializeField] float angularSpeed = 100f;
        public float AngularSpeed {get => angularSpeed; private set => angularSpeed = value;}

        [SerializeField] float acceleration = 20f;    
        public float Acceleration {get => acceleration; private set => acceleration = value;}

        [Header("Default Values")]
        [SerializeField] float moveSpeed = 2f;
        public float MoveSpeed {get => moveSpeed; private set => moveSpeed = value;}

    }
}