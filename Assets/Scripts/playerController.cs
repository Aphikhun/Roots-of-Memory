using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb_player;

    public bool isGrab = false;
    public StunBombBehavior StunBombPrefab;
    public Transform LaunchOffSet;
    
    [SerializeField] private float move_speed = 2f;
    private bool isFacingRight = true;
    private bool isRunning = false;
    private float horizontal;

    public static playerController instance;
    void Start()
    {
        rb_player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrab)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            Move();
        }
        else
        {
            
            //GameManager.instance.QTE_Event();
        }
        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if(isRunning)
        {
            move_speed = 3f;
        }
        else
        {
            move_speed = 2f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(StunBombPrefab, LaunchOffSet.position, LaunchOffSet.rotation);
        }

        Flip();

        
    }

    void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            LaunchOffSet.localScale = localScale;
        }
    }
    void Move()
    {
        rb_player.velocity = new Vector2(horizontal * move_speed, rb_player.velocity.y);
    }
}
