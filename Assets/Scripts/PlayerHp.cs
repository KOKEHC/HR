//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PlayerHp : MonoBehaviour
//{
//    public int Hp = 3;
//    private Slider slider;
//    public GameObject panel;
//    public GameObject Player;

//    void Start()
//    {
//        slider = GetComponent<Slider>();
//    }

//    void Update()
//    {
//        slider.value = Hp;
//        if (Hp <= 0)
//        {
//            panel.SetActive(true);
//        }
//        if (Player.transform.position.y <= -38)
//        {
//            Hp = 0;
//        }
//    }

//    public void Damage(int damage)
//    {
//        Hp -= damage;
//        if (Hp <= 0)
//        {
//            panel.SetActive(true);
//        }
//    }
//    public void ReloadScene()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//    }
//}
