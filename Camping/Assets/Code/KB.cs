using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class KB : MonoBehaviour
{
    private Rigidbody2D rigidbodyKB;
    private SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    private bool KBonground = true;
    private bool fire = false;
    private bool saltpepper = false;
    private bool seasoningscore = false;
    private bool grillscore = false;
    private bool onplate = false;
    public Sprite[] spriteArray;
    public AudioClip[] clipArray;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyKB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var initX = rigidbodyKB.position.x;
        var initY = rigidbodyKB.position.y;
        if (!onplate)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbodyKB.position = new Vector2(initX - 0.05f, initY);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rigidbodyKB.position = new Vector2(initX + 0.05f, initY);
            }
            if (KBonground)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    //Debug.Log(MMonground);
                    rigidbodyKB.AddForce(new Vector2(0, 170f));
                    KBonground = false;
                }
            }
        }
        
        if (fire && saltpepper)
        {
            spriteRenderer.sprite = spriteArray[3];
        }
        else if (fire && !saltpepper)
        {
            spriteRenderer.sprite = spriteArray[2];
        }
        else if (!fire && saltpepper)
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        else
        {
            spriteRenderer.sprite = spriteArray[0];
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string orbName = collision.collider.name;
        float contactY = collision.contacts[0].point.y;
        //Debug.Log(orbName);
        //Debug.Log(contactY);
        //Debug.Log(rigidbodyMM.transform.position.y);
        if (orbName.Contains("Square"))
        {
            KBonground = true;
        }
        if (orbName.Equals("seasoning"))
        {
            saltpepper = true;
            if (!seasoningscore)
            {
                ScoreKeeper.ScorePoints(5);
                seasoningscore = true;
            }
        }
        if (orbName.Equals("grill"))
        {
            fire = true;
            KBonground = true;
            audioSource.PlayOneShot(clipArray[1], 0.7f);
            if (!grillscore)
            {
                ScoreKeeper.ScorePoints(5);
                grillscore = true;
            }
        }
        if (collision.collider.CompareTag("Plate"))
        {
            SceneLoad.SetStatus(2);
            onplate = true;
            audioSource.PlayOneShot(clipArray[3], 0.7f);
        }
    }

    void resetGame() {
        SceneLoad.resetScene();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string orbName = collider.name;
        //Debug.Log(orbName);
        if (orbName.Contains("sewage") || orbName.Contains("cream"))
        {
            audioSource.PlayOneShot(clipArray[2], 0.7f);
            Invoke("resetGame", 1f);
        }
        if (orbName.Contains("coin1"))
        {
            ScoreKeeper.ScorePoints(1);
            audioSource.PlayOneShot(clipArray[0], 0.7f);
        }
    }

}
