using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class triggerQTE : MonoBehaviour
{
    public UnityEvent playerIsGrab;

    private void Update()
    {
        playerIsGrab?.Invoke();
    }


}
