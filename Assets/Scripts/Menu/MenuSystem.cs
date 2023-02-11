using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private List<RectTransform> _pic;
    public static int mode = 0;

    private void Awake()
    {
        mode = 0;
    }

    public void Toggle()
    {
        if (mode == 2)
        {
            mode = 0;
            return;
        }
        mode ++;
        
    }
}