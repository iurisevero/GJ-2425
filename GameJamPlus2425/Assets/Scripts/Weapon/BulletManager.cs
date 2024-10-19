using UnityEngine;
using System.Collections.Generic;

namespace GJ.Bullet{
    public class BulletManager : MonoBehaviour
    {
        public static List<GameObject> bullets;
        public static BulletManager instance;

        private void Awake(){
            instance = this;
        }

        private void Start(){
            bullets = new List<GameObject>();
        }
        
        public static GameObject GetBulletFromPool(){
            for (int i = 0; i < bullets.Count; i++){
                if (!bullets[i].activeSelf){
                    var b = bullets[i].GetComponent<BulletBehaviour>();
                    b.timer = b.lifeTime;
                    bullets[i].SetActive(true);
                    bullets[i].transform.SetParent(instance.transform);
                    return bullets[i];
                }
            }
            return null;
        }

        public static GameObject GetBulletFromPoolWithType(BulletType type){
            for (int i = 0; i < bullets.Count; i++){
                if (!bullets[i].activeSelf && bullets[i].GetComponent<BulletBehaviour>().type == type){
                    var b = bullets[i].GetComponent<BulletBehaviour>();
                    b.timer = b.lifeTime;
                    bullets[i].SetActive(true);
                    bullets[i].transform.SetParent(instance.transform);
                    return bullets[i];
                }
            }
            return null;
        }
    }
}
