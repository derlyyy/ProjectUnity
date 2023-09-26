using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int damage;
    public float attackRange = 10f;
    public float bulletSpeed = 10f;
    
    public int maxAmmo = 30;
    public int currentAmmo;

    private Transform target;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    Enemy FindNearestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        Enemy nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);

                    if (distance < nearestDistance)
                    {
                        nearestEnemy = enemy;
                        nearestDistance = distance;
                    }
                }
            }
        }

        return nearestEnemy;
    }
    
    public void AttackNearestEnemy()
    {
        Enemy nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null && currentAmmo > 0)
        {
            currentAmmo--;
            
            Vector3 direction = (nearestEnemy.transform.position - transform.position).normalized;
            
            direction = Quaternion.Euler(0, 0, 0) * direction;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;
        }
    }
}
