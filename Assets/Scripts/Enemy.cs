using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;          // Скорость движения NPC
    public float distance = 5.0f;
    private Vector3 startPosition;      // Расстояние, на которое NPC будет двигаться влево и вправо

    private EnemyHealth health;         // Объект здоровья врага
    // Начальная позиция NPC
    private bool movingRight = true;


    void Start()
    {
        startPosition = transform.position;
        health = new EnemyHealth(100, this);  // Начальное здоровье NPC

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Проверяем направление движения
        if (movingRight)
        {
            // Двигаем NPC вправо
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            // Проверяем, достигли ли мы правой границы
            if (transform.position.x >= startPosition.x + distance)
            {
                movingRight = false;  // Меняем направление на левое
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
        else
        {
            // Двигаем NPC влево
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            // Проверяем, достигли ли мы левой границы
            if (transform.position.x <= startPosition.x - distance)
            {
                movingRight = true;  // Меняем направление на правое
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

        // Метод для смерти NPC
    public void Die()
    {
        Destroy(gameObject);
    }
}

        // Базовый класс характеристик врага (здоровье)
    public class EnemyStats
    {
    protected int hp;

    public EnemyStats(int startHp)
    {
        hp = startHp;
    }

    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }
    }

    // Класс здоровья, унаследованный от EnemyStats
    public class EnemyHealth : EnemyStats
{ 
        private Enemy enemy;

    public EnemyHealth(int startHp, Enemy enemyObj) : base(startHp)
    {
        enemy = enemyObj;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (IsDead())
        {
            enemy.Die();
        }
    }
}

   
