using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin2 : MonoBehaviour
{
    private Rigidbody2D rigidbodycoin;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodycoin = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string orbName = collider.name;
        if (orbName.Equals("MM"))
        {
            Destroy(gameObject);
        }
    }
}
