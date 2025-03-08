//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Hero : MonoBehaviour
//{
//    [SerializeField] private float speed = 3f;
//    [SerializeField] private int lives = 3;
//    [SerializeField] private float jumpForce = 15f;
//    private bool isGrounded = false;

//    private Rigidbody2D rb;
//    private SpriteRenderer sprite;
//    private Animator anim;
//    public Slider sl;
//    private PlayerHp ph;
//    private float timer = 3.6f;
//    private bool die = false;
//    public GameObject zoneatack;
//    float timer2 = 0.5f;

//    private void Awake()
//    {
//        ph = sl.GetComponent<PlayerHp>();

//        rb = GetComponent<Rigidbody2D>();
//        sprite = GetComponentInChildren<SpriteRenderer>();
//        anim = GetComponent<Animator>();
//        Die();
//    }

//    private void FixedUpdate()
//    {
//        CheckGround();
//    }

//    private void Update()
//    {
//        if (!die)
//        {
//            if (isGrounded) State = States.idle;
//            if (Input.GetButton("Horizontal"))
//                Run();
//            if (isGrounded && Input.GetButtonDown("Jump"))
//                Jump();


//        }

//        Die();
//        if (isGrounded && Input.GetKey(KeyCode.Mouse0))
//        {
//            Attack();
//            zoneatack.gameObject.transform.localPosition = new Vector3(1.1f, 0.94f, 0);


//        }
//        else
//        {
//            zoneatack.gameObject.transform.localPosition = new Vector3(0, 0.94f, 0);
//        }



//    }

//    private void Attack()
//    {
//        if (isGrounded) State = States.Attack;
//        AudioSource audioSource = GetComponent<AudioSource>();
//        if (audioSource != null)
//        {
//            audioSource.Play();
//        }

//    }


//    private void Run()
//    {
//        if (isGrounded) State = States.run;

//        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
//        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
//        sprite.flipX = dir.x < 0.0f;
//    }

//    private void Jump()
//    {
//        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
//    }

//    private void CheckGround()
//    {
//        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
//        isGrounded = collider.Length > 1;

//        if (!isGrounded) State = States.jump;
//    }
//    private async void Die()
//    {
//        if (ph.Hp <= 0)
//        {

//            anim.SetInteger("hp", ph.Hp);
//            timer -= Time.deltaTime;
//            if (timer <= 0)
//            {
//                die = true;
//                anim.enabled = false;
//            }



//        }

//    }

//    private States State
//    {
//        get { return (States)anim.GetInteger("state"); }
//        set { anim.SetInteger("state", (int)value); }
//    }
//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.name.Contains("Enemy"))
//        {
//            ph.Damage(1);
//        }
//    }
//}

//public enum States
//{
//    idle,
//    run,
//    jump,
//    Attack,
//    Die
//}