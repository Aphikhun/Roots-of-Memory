using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaivor : MonoBehaviour
{
    
    public bool canChasing = true;
    private Rigidbody2D rb_enemy;
    private float enemy_attack_cooldown = 1f;
    private float attack_time = 0f;
    [SerializeField] private float enemy_attack_range = 3f;
    [SerializeField] private float enemy_move_speed = 4f;
    [SerializeField] private int atk = 2;
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        rb_enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canChasing)
        {
            Chase();
        }
        attack_time += Time.deltaTime;
        DetectPlayer();
    }

    private void Chase()
    {
        if (transform.position.x > player.position.x)
        {
            transform.position += Vector3.left * enemy_move_speed * Time.deltaTime;
        }
        if (transform.position.x < player.position.x)
        {
            transform.position += Vector3.right * enemy_move_speed * Time.deltaTime;
        }
        if (transform.position.x == player.position.x)
        {
            canChasing = false;
        }

    }
    private void DetectPlayer()
    {
        Collider2D[] target = Physics2D.OverlapCircleAll(transform.position, enemy_attack_range);
        if(target != null)
        {
            foreach(Collider2D col in target)
            {
                if (col.GetComponent<playerHealth>())
                {
                    canChasing = false;
                    Attack(col);
                }
            }
        }
    }

    private void Attack(Collider2D col)
    {
        playerHealth player_health = col.GetComponent<playerHealth>();
        playerController player_control = col.GetComponent<playerController>(); 
        player_control.isGrab = true;
        
        
        if (player_control.isGrab)
        {
            if (attack_time >= enemy_attack_cooldown)
            {
                player_health.getDamage(atk);
                attack_time = 0;
            }
        }
        else
        {
            Stun();
        }
    }

    private void Stun()
    {
        Destroy(gameObject);
    }

}
