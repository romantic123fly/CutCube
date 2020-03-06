using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Getinstance()
    {
        return instance;
    }
    public List<Color> colors = new List<Color>();
    public int Score { get; set; }
    public int CubeNum { get; set; } = 0;

    private void Awake()
    {
        instance = this;
        ColorListInit();
    }
    private void ColorListInit()
    {
        if (colors.Count != 0)
        {
            colors.Clear();
        }
        Color a = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        for (int j = 0; j < 100; j++)
        {
            Color b = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            for (int i = 1; i <= 20; i++)
            {
                var c = Color.Lerp(a, b, 1f / 20 * i);
                if (i % 5 == 0)
                {
                    colors.Add(c);
                    if (i == 20)
                    {
                        a = c;
                    }
                }
            }
        }
    }
}
