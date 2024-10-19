using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ.AI
{
    public class AISpriteLocker : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void LockSprite(Transform player)
        {
            if (player == null || spriteRenderer == null) return;

            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, -angle + 90, 0);
        }
    }
}
