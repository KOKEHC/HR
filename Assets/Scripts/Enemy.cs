using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;          // �������� �������� NPC
    public float distance = 5.0f;
    private Vector3 startPosition;      // ����������, �� ������� NPC ����� ��������� ����� � ������
    private Health health;              // �������� ��� �������� NPC


    // ��������� ������� NPC
    private bool movingRight = true;


    void Start()
    {
        startPosition = transform.position;
        health = new Health(100);  // ��������� �������� NPC

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // ��������� ����������� ��������
        if (movingRight)
        {
            // ������� NPC ������
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            // ���������, �������� �� �� ������ �������
            if (transform.position.x >= startPosition.x + distance)
            {
                movingRight = false;  // ������ ����������� �� �����
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
        else
        {
            // ������� NPC �����
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            // ���������, �������� �� �� ����� �������
            if (transform.position.x <= startPosition.x - distance)
            {
                movingRight = true;  // ������ ����������� �� ������
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }

    // ����� ��� ��������� ����� NPC
    public void TakeDamage(int damage)
    {
        health.DecreaseHealth(damage);  // ��������� �������� ����� ����� � ���������
        if (health.CurrentHP <= 0)
        {
            Die();  // ���� �������� NPC 0 ��� ������, �������� ����� ������
        }
    }

    // ����� ��� ������ NPC
    private void Die()
    {
        Destroy(gameObject);  // ���������� ������ NPC
    }

    public class Health
    {
        public int MaxHP { get; private set; }   // ������������ ��������
        public int CurrentHP { get; private set; }   // ������� ��������

        // ����������� ��� ������������� ������������� ��������
        public Health(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }

        // ����� ��� ���������� ��������
        public void DecreaseHealth(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0)
            {
                CurrentHP = 0;  // �������� �� ����� ���� ������ ����
            }
        }
    }
}
   
