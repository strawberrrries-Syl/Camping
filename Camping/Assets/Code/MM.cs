using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class MM : MonoBehaviour
{
    private Rigidbody2D rigidbodyMM;
    private SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    private bool MMonground = true;
    private bool fire = false;
    private bool sprinkle = false;
    private bool sprinklescore = false;
    private bool grillscore = false;
    private bool onplate = false;
    public Sprite[] spriteArray;
    public AudioClip[] clipArray;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyMM = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var initX = rigidbodyMM.position.x;
        var initY = rigidbodyMM.position.y;
        if (!onplate)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbodyMM.position = new Vector2(initX - 0.05f, initY);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbodyMM.position = new Vector2(initX + 0.05f, initY);
            }
            if (MMonground)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //Debug.Log(MMonground);
                    rigidbodyMM.AddForce(new Vector2(0, 170f));
                    MMonground = false;
                }
            }
        }

        if (fire && sprinkle)
        {
            spriteRenderer.sprite = spriteArray[3];
        }
        else if (fire && !sprinkle)
        {
            spriteRenderer.sprite = spriteArray[2];
        }
        else if (!fire && sprinkle)
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
            MMonground = true;
        }
        if (orbName.Equals("sprinklepng"))
        {
            sprinkle = true;
            if (!sprinklescore)
            {
                ScoreKeeper.ScorePoints(5);
                sprinklescore = true;
            }
        }
        if (orbName.Equals("grill"))
        {
            fire = true;
            MMonground = true;
            audioSource.PlayOneShot(clipArray[1], 0.7f);
            if (!grillscore)
            {
                ScoreKeeper.ScorePoints(5);
                grillscore = true;
            }
        }
        if(collision.collider.CompareTag("Plate"))
        {
            SceneLoad.SetStatus(1);
            onplate = true;
            audioSource.PlayOneShot(clipArray[3], 0.7f);
        }
    }
    void resetGame()
    {
        SceneLoad.resetScene();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        string orbName = collider.name;
        //Debug.Log(orbName);
        if (orbName.Contains("sewage") || orbName.Contains("oil"))
        {
            audioSource.PlayOneShot(clipArray[2], 0.7f);
            Invoke("resetGame", 1f);
        }
        if (orbName.Contains("coin2"))
        {
            ScoreKeeper.ScorePoints(1);
            audioSource.PlayOneShot(clipArray[0], 0.7f);
        }
    }

}
