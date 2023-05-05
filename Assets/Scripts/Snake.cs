using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{

    public int score;
    public Spawner spawnerScript;
    public Text scoreText;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprite;
    public GameObject gameOver;


    public float speed;
    public GameObject tailPrefab;

    private Vector2 direction;
    private List<GameObject> tailObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.right;
        tailObjects.Add(gameObject);
        InvokeRepeating(nameof(Move), 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Snake controls
        if (Time.deltaTime < 0.5f)
        {
            if (Input.GetKey(KeyCode.W) && direction != Vector2.down)
            {
                direction = Vector2.up;
                spriteRenderer.sprite = sprite[0];
                spriteRenderer.flipX = false;
                spriteRenderer.flipY = false;
            }
            else if (Input.GetKey(KeyCode.S) && direction != Vector2.up)
            {
                direction = Vector2.down;
                spriteRenderer.sprite = sprite[0];
                spriteRenderer.flipX = false;
                spriteRenderer.flipY = true;
            }
            else if (Input.GetKey(KeyCode.A) && direction != Vector2.right)
            {
                direction = Vector2.left;
                spriteRenderer.sprite = sprite[1];
                spriteRenderer.flipX = true;
                spriteRenderer.flipY = false;
            }
            else if (Input.GetKey(KeyCode.D) && direction != Vector2.left)
            {
                direction = Vector2.right;
                spriteRenderer.sprite = sprite[1];
                spriteRenderer.flipX = false;
                spriteRenderer.flipY = false;
            }
        }

    }


    //Snake Movement
    void Move()
    {
        transform.Translate(direction * speed);

        for (int i = tailObjects.Count - 1; i > 0; i--)
        {
            tailObjects[i].transform.position = tailObjects[i - 1].transform.position;
        }

        if (tailObjects.Count > 1)
        {
            tailObjects[0].transform.position = transform.position;
            tailObjects[1].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    //Snake Grow
    void Grow()
    {
        for(int i = 0; i < 3; i++)
        {
            Vector2 tailPosition = tailObjects[tailObjects.Count - 1].transform.position;
            GameObject newTail = Instantiate(tailPrefab, tailPosition, Quaternion.identity);
            tailObjects.Add(newTail);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Scores

        if (collision.CompareTag("Small"))
        {
            Destroy(collision.gameObject);
            score += 5;
            scoreText.text = score.ToString();
            spawnerScript.PowerUps();
            Grow();
        }
        else if (collision.CompareTag("Big"))
        {
            Destroy(collision.gameObject);
            score += 50;
            scoreText.text = score.ToString();
            spawnerScript.PowerUps();
        }


        //Snake Death
        if (collision.CompareTag("danger"))
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
        
        if (collision.CompareTag("Tail"))
        {
            if(tailObjects.Count > 4)
            {
                Time.timeScale = 0;
                gameOver.SetActive(true);
            }
        }
    }

}
