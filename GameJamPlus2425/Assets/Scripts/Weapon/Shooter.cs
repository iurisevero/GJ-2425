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
    private bool canShootSpecial;
    private float timeFromLastSpecial;

    public Camera playerCamera;
    public GameObject bulletPrefab;
    public GameObject specialBulletPrefab;
    public Transform bulletSpawn;

    public bool rayhit = false;
    public float fireRate = 1.5f;
    public float bulletVelocity = 30;
    public float bulletLifeTime = 3f;
    public float bulletRange = 100;

    public float specialCooldown = 10f;

    private void Awake()
    {
        bulletPoolKey = this.name + "Bullet";
        canShoot = true;
        if (playerCamera == null)
		{
			playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		}
    }

    // Start is called before the first frame update
    private void Start()
    {
        _input = GetComponentInParent<InputHandler>();
        GameObjectPoolController.AddEntry(bulletPoolKey, bulletPrefab, 10, 50);
        timeFromLastShot = fireRate;
        timeFromLastSpecial = specialCooldown;
    }

    // Update is called once per frame
    private void Update()
    {
        if(_input.fire && canShoot) {
            Shoot();
        }

        if(_input.fireRight && canShootSpecial) {
            Debug.Log("Shoot Special");
            ShootSpecial();
            _input.fireRight = false;
        } else if(_input.fireRight) _input.fireRight = false;

        if (!canShoot)
        {
            timeFromLastShot += Time.deltaTime;
            if (timeFromLastShot > fireRate)
            {
                canShoot = true;
            }
        }

        if (!canShootSpecial)
        {
            timeFromLastSpecial += Time.deltaTime;
            if (timeFromLastSpecial > specialCooldown)
            {
                canShootSpecial = true;
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

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit, bulletRange)) {
            targetPoint = hit.point;
        } else {
            targetPoint = ray.GetPoint(bulletRange);
        }

        if(rayhit && hit.collider != null) {
            Debug.Log("Hit ray hitted: " + hit.collider.name);
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

    private void ShootSpecial()
    {
        canShootSpecial = false;
        GameObject special = Instantiate(specialBulletPrefab, bulletSpawn.position, Quaternion.identity);
        special.transform.forward = bulletSpawn.forward;
        timeFromLastSpecial = 0;
    }
}