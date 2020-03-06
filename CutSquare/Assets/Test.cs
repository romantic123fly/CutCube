using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public Image a;
    void Start()
    {
        var list = GetComponentsInChildren<Image>();
        ColorListInit();
    }

    private void ColorListInit()
    {
        if (colors.Count!=0)
        {
            colors.Clear();
        }
        for (int j = 0; j < 100; j++)
        {
            Color a =new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));   
            for (int i = 1; i <= 20; i++)
            {
                var b = CreateColor(a, Color.grey, 1f / 20 * i);
                if (i % 4 == 0)
                {
                    colors.Add(b);
                }
            }
        }
    }

    void Update()
    {
   
    }
    Color CreateColor(Color c1,Color c2,  float a)
    {
       var c= Color.Lerp(c1,c2,a);
        return c;
    }

}
