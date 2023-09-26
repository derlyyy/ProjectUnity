using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Characteristics")]
    public int maxHealth;
    public int currentHealth;
    public int damage;
    public float attackCooldown = 2f; // время между атаками
    public float moveSpeed;
    
    private float lastAttackTime;
    
    
    [Header("Drop Item")]
    public GameObject itemDrop; // дроп после смерти

    [Header("Attack Range Info")]
    public float detectionRadius = 10f; // радиус нахождения
    public float attackRange = 2f; // дальность врага
    
    private Transform player;
    private float distance;
    
    private Rigidbody2D rb;
    private bool isAttacking;
    private bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        currentHealth = maxHealth;
        lastAttackTime = -attackCooldown;
        
        isAttacking = false;
        isDead = false;
        
    }

    void Update()
    {
        // при приближение игрока в зону видимости монстра - монстр должен приблизиться к игроку и атаковать
        distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            Vector2 direction = player.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            // обновление полоски здоровья
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        // создание предмета
        Instantiate(itemDrop, transform.position, Quaternion.identity);
        // удаление врага
        Destroy(gameObject);
    }
}
