using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool isGrounded = false;
    private bool die = false;
    public GameObject panel;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    public Slider sl;
    public GameObject zoneatack;
 
    private Health health;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        health = new Health(3, sl, this, panel);
        
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()                                   // реализация управления
    {
        if (!die)
        {
            if (isGrounded)
                State = States.idle;
            if (Input.GetButton("Horizontal"))
                Run();
            if (isGrounded && Input.GetButtonDown("Jump"))                            
                Jump();
            if (isGrounded && Input.GetKey(KeyCode.Mouse0))
                Attack();
            
        }
    }

    private void Attack()                                // метод реализации атаки
    {
        if (isGrounded) State = States.Attack;
        zoneatack.transform.localPosition = new Vector3(1.1f, 0.94f, 0);                          
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void Run()                                     // метод реализации бега
    {
        if (isGrounded) State = States.run;                                                         
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);      
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()                         // метод прыжка
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);        
    }

    private void CheckGround()                   // метод проверки земли
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);   
        isGrounded = colliders.Length > 1;
        if (!isGrounded) State = States.jump;
    }

    private States State                     // состояния анимации
    {
        get { return (States)anim.GetInteger("state"); }     
        set { anim.SetInteger("state", (int)value); }
    }

    void OnCollisionEnter2D(Collision2D collision)             //нанесение урона, если атака достигла врага
    {
        if (collision.gameObject.name.Contains("Enemy"))       
        {
            health.TakeDamage(1);
        }
    }

    public void Die()
    {
        die = true;
        panel.SetActive(true); // Включаем панель проигрыша
        Destroy(gameObject);
        
    }

    // Базовый класс для Статов
    public class CharacterStats
    {
        protected int hp;
        protected int maxHp;

        public CharacterStats(int startHp)
        {
            hp = startHp;
            maxHp = startHp;
        }

        public virtual void Heal(int amount) // метод для лечения
        {
            hp += amount;
            if (hp > maxHp)
                hp = maxHp;
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

    // Класс Health наследуется от CharacterStats
    public class Health : CharacterStats
    {
        private Slider slider;
        private HeroPlayer hero;
        private GameObject panel; 

        public Health(int startHp, Slider healthSlider, HeroPlayer player, GameObject panelObj) : base(startHp)
        {
            slider = healthSlider;
            hero = player;
            panel = panelObj;
            slider.maxValue = maxHp;
            slider.value = startHp;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            slider.value = hp;

            if (IsDead()) // Если здоровье закончилось
            {
                panel.SetActive(true); // Показываем панель проигрыша
                hero.Die(); // Герой умирает
            }
        }

        public override void Heal(int amount) // Метод лечения
        {
            base.Heal(amount);
            slider.value = hp; // Обновляем значение на слайдере
        }
    }

    // Класс Armor (броня, которая поглощает урон)
    public class Armor : CharacterStats
    {
        private int armorValue; // Количество брони
        private Health health;  // Ссылка на здоровье

        public Armor(int startHp, int armor, Health playerHealth) : base(startHp)
        {
            armorValue = armor;
            health = playerHealth;
        }

        public override void TakeDamage(int damage)
        {
            int damageAfterArmor = damage - armorValue; // Броня уменьшает урон
            if (damageAfterArmor < 0)
                damageAfterArmor = 0; // Урон не может быть отрицательным

            base.TakeDamage(damageAfterArmor); // Наносим урон броне

            if (IsDead()) // Если броня разрушена, урон идет в здоровье
            {
                health.TakeDamage(damageAfterArmor);
            }
        }

        public void RepairArmor(int amount)
        {
            hp += amount;
            if (hp > maxHp)
                hp = maxHp;
        }
    }
}

public enum States            // состояния анимаций
{
    idle,
    run,
    jump,
    Attack,
    Die
}