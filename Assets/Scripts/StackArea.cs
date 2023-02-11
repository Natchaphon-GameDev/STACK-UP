using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackArea : MonoBehaviour
{
    public bool isInArea = false;

    public event Action<StackArea,bool> onCupInArea;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("StackCupArea"))
        {
            isInArea = true;
            GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            onCupInArea?.Invoke(this,isInArea);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StackCupArea"))
        {
            isInArea = false;
            GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            onCupInArea?.Invoke(this,isInArea);
        }
    }
}
