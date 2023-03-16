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
    bool broken = true;

    Animator anim;

    [SerializeField] ParticleSystem smokeEffect;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction *= -1;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        
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

    public void Fix()
    {
        broken = false;
        rb2D.simulated = false;
        anim.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}
