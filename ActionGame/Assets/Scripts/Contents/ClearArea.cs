using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearArea : MonoBehaviour
{
    public Action OnAreaClearEventHandler = null;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {        
            OnAreaClearEventHandler.Invoke();
        }
    }
}
