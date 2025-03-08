using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeCoin : MonoBehaviour
{
    public int coin = 0;
    public Text countCoins;
    // Start is called before the first frame update
    void Start()
    {
        countCoins.text = "- "+coin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {      
        if (other.gameObject.name.Contains("Russian"))
        {         
            coin += 1;
            countCoins.text = "- " + coin;
            Destroy(gameObject);
        }
       
    }


}
