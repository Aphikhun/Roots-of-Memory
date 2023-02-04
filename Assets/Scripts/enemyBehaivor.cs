using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaivor : MonoBehaviour
{

    public bool isStun = false;
    public bool canChasing = true;
    private Rigidbody2D rb_enemy;
    private float enemy_attack_cooldown = 1f;
    private float attack_time = 0f;
    [SerializeField] private float enemy_attack_range = 3f;
    [SerializeField] private float enemy_move_speed = 4f;
    [SerializeField] private int atk = 2;
    [SerializeField] private float stuntimelimit = 5f;
    [SerializeField] private float stuntime;
    [SerializeField] private Transform player;
    private BoxCollider2D enemycollider;
    private int atk_tmp;
    private float enemy_attack_range_tmp;
    private float enemy_move_speed_tmp;

    // Start is called before the first frame update
    void Start()
    {
        rb_enemy = GetComponent<Rigidbody2D>();
        
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
        /*if (!isStun)
        {
            player_control.isGrab = true;
        }*/
        
        
        if (player_control.isGrab!)
        {
            if (attack_time >= enemy_attack_cooldown)
            {
                player_health.getDamage(atk);
                attack_time = 0;
            }
        }
        else
        {
            //Stun();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "StunBomb")
        {
            Stun();
        }
    }

    private void Stun()
    {
        atk_tmp = atk;
        enemy_attack_range_tmp = enemy_attack_range;
        enemy_move_speed_tmp = enemy_move_speed;
        stuntime = stuntimelimit;
        //Destroy(this.gameObject);
        if (stuntime > 0)
        {
            
            isStun = true;
            canChasing = false;
            atk = 0;
            enemy_attack_range = 0;
            enemy_move_speed = 0;
        }
        else
        {
            isStun = false;
            canChasing = true;
            atk = atk_tmp;
            enemy_attack_range = enemy_attack_range_tmp;
            enemy_move_speed = enemy_move_speed_tmp;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!isStun)
        {
            if (canChasing)
            {
                Chase();
            }
            attack_time += Time.deltaTime;
            DetectPlayer();
        }
        else if (isStun)
        {
            stuntime -= Time.deltaTime;
            //Destroy(rb_enemy);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        }
        else if (!isStun)
        {
            stuntime = 0;
            isStun = false;
            canChasing = true;
            atk = atk_tmp;
            enemy_attack_range = enemy_attack_range_tmp;
            enemy_move_speed = enemy_move_speed_tmp;

        }
    }
}
