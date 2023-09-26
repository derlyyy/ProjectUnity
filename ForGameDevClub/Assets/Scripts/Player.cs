using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Characteristics")]
    public int health;
    public int maxHealth;
    public int damage;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
        {
            Destroy(gameObject); // уничтожаем игрока при хп <= 0
        }
        
        health -= damage;
    }
}
