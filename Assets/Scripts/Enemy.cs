using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;          // �������� �������� NPC
    public float distance = 5.0f;
    private Vector3 startPosition;      // ����������, �� ������� NPC ����� ��������� ����� � ������

    private EnemyHealth health;         // ������ �������� �����
    // ��������� ������� NPC
    private bool movingRight = true;


    void Start()
    {
        startPosition = transform.position;
        health = new EnemyHealth(100, this);  // ��������� �������� NPC

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

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

        // ����� ��� ������ NPC
    public void Die()
    {
        Destroy(gameObject);
    }
}

        // ������� ����� ������������� ����� (��������)
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

    // ����� ��������, �������������� �� EnemyStats
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

   
