using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;
    public int damage;
    
    void Start()
    {
        Destroy(gameObject, lifetime); ;
    }

    void Update()
    {
        //transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}