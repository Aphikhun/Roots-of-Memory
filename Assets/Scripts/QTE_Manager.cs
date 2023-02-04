
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTE_Manager : MonoBehaviour
{
    public bool stunEnemy;
    private int num;
    public char[] key;
    public int stack;
    private float fill_amount = 1f;
    private bool waitForKey = true;
    private string[] key_list = { "W","A","S","D"};

    [SerializeField] private playerController player_control;
    [SerializeField] private enemyBehaivor enemy;
    [SerializeField] private GameObject QTE_UI;
    [SerializeField] private TextMeshProUGUI QTE_text;

    public static QTE_Manager instance;
    private void Start()
    {
        
    }
    private void Update()
    {
        QTE_UI.SetActive(player_control.isGrab);
        Countdown();

        if (waitForKey)
        {
            RandomKey();
        }
        CheckKey();
        if(stack >= 6)
        {
            player_control.isGrab = false;
            enemy.isStun = true;

        }

    }

    private void Countdown()
    {
        fill_amount -= Time.deltaTime;

        QTE_UI.GetComponent<Image>().fillAmount = fill_amount;

        if(fill_amount <= 0f )
        {
            Fail();
        }
    }
    void Fail()
    {
        stack = 0;
        fill_amount = 1f;
        waitForKey = true;
    }
    void Success()
    {
        stack++;
        fill_amount = 1f;
        waitForKey = true;
    }
    void RandomKey()
    {
        num = Random.Range(0, 3);
        QTE_text.SetText(key_list[num].ToString());
        waitForKey = false;
    }
    private void CheckKey()
    {
        if (Input.anyKeyDown)
        {
            key = Input.inputString[0].ToString().ToCharArray(); ;
            

            Debug.Log(key);
            if(key != null)
            {
                if (System.Char.IsLetter(key[0]))
                {
                    if (key[0].ToString().ToUpper() == key_list[num])
                    {
                        Success();
                    }
                    else
                    {
                        Fail();
                    }
                }
            }
        }
    }
}
