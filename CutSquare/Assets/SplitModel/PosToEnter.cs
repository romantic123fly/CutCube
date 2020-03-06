using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PosToEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetModelCenter();
    }
    public  void SetModelCenter()
    {
        Vector3 postion = transform.position;
        Quaternion rotation = transform.rotation;
        Vector3 scale = transform.localScale;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        Vector3 center = Vector3.zero;

        Renderer[] renders = transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;
        }
        //center /= transform.GetComponentsInChildren().Length;
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);
        }

        transform.position = postion;
        transform.rotation = rotation;
        transform.localScale = scale;

        foreach (Transform t in transform)
        {
            t.position = t.position - bounds.center;
        }
        transform.transform.position = bounds.center + transform.position;
    }

}
