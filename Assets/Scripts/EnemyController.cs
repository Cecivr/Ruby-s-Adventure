using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    [SerializeField]bool vertical;
    float changeTime = 3.0f;

    Rigidbody2D rb2D;
    float timer;
    int direction = 1;

    Animator anim;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction *= -1 ;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            anim.SetFloat("Move X", 0);
            anim.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            anim.SetFloat("Move X", direction);
            anim.SetFloat("Move Y", 0);
        }

        rb2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
