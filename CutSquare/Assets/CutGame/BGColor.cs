using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BGColor : MonoBehaviour
{
    bool isStart;
    Color currentMianColor;
    Color currentSecondColor;
    void Start()
    {
        GetComponent<MeshRenderer>().material.SetColor("_MainColor", GameManager.Getinstance().colors[4]);
        GetComponent<MeshRenderer>().material.SetColor("_SecondColor", GameManager.Getinstance().colors[0]);
        currentMianColor = GetComponent<MeshRenderer>().material.GetColor("_MainColor");
        currentSecondColor = GetComponent<MeshRenderer>().material.GetColor("_SecondColor");
    }

    float temp = 0;
    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            temp += Time.deltaTime;
            if (GameManager.Getinstance().colors.Count >= 0)
            {
                GetComponent<MeshRenderer>().material.SetColor("_MainColor", Color.Lerp(currentMianColor, GameManager.Getinstance().colors[GameManager.Getinstance().CubeNum+4], temp / 3));
                GetComponent<MeshRenderer>().material.SetColor("_SecondColor", Color.Lerp(currentSecondColor, GameManager.Getinstance().colors[GameManager.Getinstance().CubeNum], temp / 3));
            }
            if (temp >= 3)
            {
                currentMianColor = GetComponent<MeshRenderer>().material.GetColor("_MainColor");
                currentSecondColor = GetComponent<MeshRenderer>().material.GetColor("_SecondColor");
                isStart = false;
                temp = 0;
                Debug.LogError("end");
            }
        }
    }

    public  void ChangeColor()
    {
        isStart = true;
        if (temp != 0)
        {
            currentMianColor = GetComponent<MeshRenderer>().material.GetColor("_MainColor");
            currentSecondColor = GetComponent<MeshRenderer>().material.GetColor("_SecondColor");
            temp = 0;
        }
    }
}
