using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Vector3 mousePosition;
    private float speed = 5f;
    private bool escape = false;
    public GameOver gameOver;
    public Rigidbody2D rb;
    public bool canMove = true;

    void Start()
    {
        gameOver = GameObject.FindObjectOfType(typeof(GameOver)) as GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameOver.Pause();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            gameOver.EndGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Escape") && escape)
        {
            gameOver.Win();
        }
    }

    public void Escape()
    {
        escape = true;
    }
}
