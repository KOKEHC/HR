using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;          // Скорость движения NPC
    public float distance = 5.0f;
    private Vector3 startPosition;      // Расстояние, на которое NPC будет двигаться влево и вправо
    private Health health;              // Подкласс для здоровья NPC


    // Начальная позиция NPC
    private bool movingRight = true;


    void Start()
    {
        startPosition = transform.position;
        health = new Health(100);  // Начальное здоровье NPC

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

    // Метод для нанесения урона NPC
    public void TakeDamage(int damage)
    {
        health.DecreaseHealth(damage);  // Уменьшаем здоровье через метод в подклассе
        if (health.CurrentHP <= 0)
        {
            Die();  // Если здоровье NPC 0 или меньше, вызываем метод смерти
        }
    }

    // Метод для смерти NPC
    private void Die()
    {
        Destroy(gameObject);  // Уничтожаем объект NPC
    }

    public class Health
    {
        public int MaxHP { get; private set; }   // Максимальное здоровье
        public int CurrentHP { get; private set; }   // Текущее здоровье

        // Конструктор для инициализации максимального здоровья
        public Health(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }

        // Метод для уменьшения здоровья
        public void DecreaseHealth(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0)
            {
                CurrentHP = 0;  // Здоровье не может быть меньше нуля
            }
        }
    }
}
   
