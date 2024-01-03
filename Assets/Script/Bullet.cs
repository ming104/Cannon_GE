using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject bp;
    private void Awake()
    {
        bp = GameObject.Find("BulletPos");
    }
    private Vector3 direction;
    private void OnEnable()
    {
        gameObject.transform.position = bp.transform.position;
    }
    public void Shoot(Vector3 direction, float bulletSpeed)
    {
        this.direction = direction;
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        Invoke("DestroyBullet", 5f);

    }

    public void DestroyBullet()
    {
        CannonShoot.ReturnObject(this);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            DestroyBullet();
        }
    }
}
