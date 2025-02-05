using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Fire : MonoBehaviour
{
    public GameObject[] enemyBulletPrefabs;
    public float MinPower;
    public float MaxPower;

    public void SpawnBullet()
    {
            GameObject selectedBulletPrefab = enemyBulletPrefabs[Random.Range(0, enemyBulletPrefabs.Length)];
            GameObject bullet = Instantiate(selectedBulletPrefab, transform.position, Quaternion.identity);
            Vector2 shotDirection = -transform.up;
            float shotPower = Random.Range(MinPower, MaxPower);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = shotDirection * shotPower;
    }
}