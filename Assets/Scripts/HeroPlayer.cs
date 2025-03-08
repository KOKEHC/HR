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
        health = new Health(3, sl, this);
        
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

    public class Health             // подкласс здоровья
    {
        private int hp = 3;
        private Slider slider;
        private HeroPlayer hero;
        private GameObject panel;          

        public Health(int startHp, Slider healthSlider, HeroPlayer player)
        {
            hp = startHp;
            slider = healthSlider;
            hero = player;
            
        }

        public void TakeDamage(int damage)   // метод получения урона
        {
            hp -= damage;
            slider.value = hp;
            if (hp <= 0) {
                hero.Die();
                panel.SetActive(true);
            }
        }
    }

    private void Die()                     // метод смерти
    {
        Destroy(gameObject);                
        SceneManager.LoadScene(0);
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