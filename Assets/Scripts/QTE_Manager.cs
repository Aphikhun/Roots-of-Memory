
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTE_Manager : GameManager
{
    
    public int stack = 0;
    public float fill_amount = 1f;
    private float time = 1f;
    private bool start_count = false;
    private int QTE_gen;
    private int wait_for_key;
    private int correct_key;
    private string key = "";
    private bool getKey = false;

    private string[] key_list = { "W", "A", "S", "D" };
    [SerializeField] private TextMeshProUGUI key_text;
    [SerializeField] private int num = 6;
    [SerializeField] private GameObject QTE_panel;
    // Start is called before the first frame update
    void Start()
    {
        fill_amount= 1f;
        stack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
        if (Input.anyKeyDown)
        {
            key = Input.inputString[0].ToString().ToUpper();
            getKey= true;
        }*/
        if (wait_for_key == 0 && stack < num)
        {
            QTE_gen = Random.Range(0,key_list.Length);

            wait_for_key = 1;
            key_text.SetText(key_list[QTE_gen].ToString());
            start_count = true;

            if(Input.GetKeyDown(KeyCode.W))
            {
                key_list[QTE_gen] = "W";
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                key_list[QTE_gen] = "S";
            }else if(Input.GetKeyDown(KeyCode.D))
            {
                key_list[QTE_gen] = "D";
            }else if (Input.GetKeyDown(KeyCode.A))
            {
                key_list[QTE_gen] = "A";    
            }
            

            if (key == key_list[QTE_gen])
            {
                Debug.Log("press right");
                correct_key = 1;
                StartCoroutine(KeyPressing());
            }
            else
            {
                Debug.Log("press wrong");
                correct_key = 2;
                StartCoroutine(KeyPressing());
            }
        }

        if(stack >= num)
        {
            QTE_panel.SetActive(false);
        }
        if (start_count)
        {
            time -= Time.deltaTime;
            GetComponent<Image>().fillAmount = time;

            if (time <= 0)
            {
                stack = 0;
                correct_key = 0;
                wait_for_key = 0;

                time = 1f;
                fill_amount = 1f;
                start_count= false;
                getKey = false;
            }
        }

    }
    IEnumerator KeyPressing()
    {
        if(correct_key == 1)
        {
            stack++;
            correct_key = 0;
            yield return new WaitForSeconds(2f);
            wait_for_key = 0;
            time = 1f;
            fill_amount = 1f;
            start_count = false;
            getKey = false;
            Debug.Log("sus "+stack);
        }
        else
        {
            stack = 0;
            correct_key = 0;
            yield return new WaitForSeconds(2f);
            wait_for_key = 0;
            time = 1f;
            fill_amount = 1f;
            start_count = false;
            getKey = false;
            Debug.Log("fail ");
        }
    }
    

}
