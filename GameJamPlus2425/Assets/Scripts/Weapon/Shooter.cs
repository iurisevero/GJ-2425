using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private string bulletPoolKey;
    // private string specialBulletPoolKey;
    private InputHandler _input;
    private bool canShoot;
    private float timeFromLastShot;

    public Camera playerCamera;
    public GameObject bulletPrefab;
    public GameObject specialBulletPrefab;
    public Transform bulletSpawn;

    public bool rayhit = false;
    public float fireRate = 1.5f;
    public float bulletVelocity = 30;
    public float bulletLifeTime = 3f;
    public float bulletRange = 100;

    private void Awake()
    {
        bulletPoolKey = this.name + "Bullet";
        canShoot = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _input = GetComponentInParent<InputHandler>();
        GameObjectPoolController.AddEntry(bulletPoolKey, bulletPrefab, 10, 50);
    }

    // Update is called once per frame
    private void Update()
    {
        if(_input.fire && canShoot) {
            Shoot();
        }

        if (!canShoot)
        {
            timeFromLastShot += Time.deltaTime;
            if (timeFromLastShot > fireRate)
            {
                canShoot = true;
            }
        }
    }

    private void Shoot()
    {
        canShoot = false;

        // Remove ammo

        // Get raycast hit target
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint = new Vector3();
        if(Physics.Raycast(ray, out hit, bulletRange)) {
            targetPoint = hit.point;
        } else {
            targetPoint = ray.GetPoint(bulletRange);
        }

        if(rayhit) {
            Debug.Log("Hit here");
        }

        // Spawn projectile
        Poolable p = GameObjectPoolController.Dequeue(bulletPoolKey);
        Bullet bullet = p.GetComponent<Bullet>();
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.forward = (targetPoint - bulletSpawn.position).normalized;
        bullet.bulletVelocity = bulletVelocity;
        bullet.lifeTime = bulletLifeTime;
        bullet.bulletRange = bulletRange;
        // bullet.transform.localScale = Vector3.one;
        bullet.gameObject.SetActive(true);
        bullet.ShootBullet();

        // Start firerate cooldown
        timeFromLastShot = 0f;
    }
}
