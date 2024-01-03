using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 direction;
    private void OnEnable()
    {
        gameObject.transform.position = new Vector3(Random.Range(-9, 9), 6, 0);
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.down * 5f, ForceMode2D.Impulse);
        //Invoke("DestroyEnemy", 4f);
    }

    public void DestroyEnemy()
    {
        EnemyManager.ReturnObject(this);
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            GameManager.Instance.score += 1;
            DestroyEnemy();
        }

        if (other.collider.CompareTag("Floor"))
        {
            DestroyEnemy();
        }
    }
}
