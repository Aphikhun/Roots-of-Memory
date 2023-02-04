using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int max_player_hp = 100;
    public int change_state_player = 40;
    public int current_hp;
    // Start is called before the first frame update
    void Start()
    {
        current_hp = max_player_hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getDamage(int damage)
    {
        if(current_hp > 0)
        {
            current_hp -= damage;
            //Debug.Log("current hp is "+current_hp);
        }
        if(current_hp < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //load check point -- hp = 100
    }
}
