using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private playerController player_control;
    [SerializeField] private GameObject QTE;


    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player_control.isGrab)
        {
            QTE.SetActive(true);
        }
    }
    
}
