using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitTool : MonoBehaviour
{
    public Plane[] splitPlanes;
    public bool isBaseCube;
    public Vector3 xPosCenter;
    public Vector3 zPosCenter;
    // Start is called before the first frame update
    void Start()
    {
        if (isBaseCube)
        {
            splitPlanes = CreatePlane();
        }
        //GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Plane[] CreatePlane(List<Vector3> rayMaskingPos)
    {
        xPosCenter = (rayMaskingPos[0] + rayMaskingPos[1]) / 2;
        zPosCenter = (rayMaskingPos[2] + rayMaskingPos[3]) / 2;
        //
        GameObject a1 = new GameObject("a1");
        a1.transform.parent = transform;
        a1.transform.position = rayMaskingPos[0];
        //a1.transform.localScale = Vector3.one;
        GameObject b1 = new GameObject("b1"); ;
        b1.transform.parent = transform;
        b1.transform.position = rayMaskingPos[0] + new Vector3(-0.5f, 1, 0);
        //b1.transform.localScale = Vector3.one;

        GameObject c1 = new GameObject("c1");
        c1.transform.parent = transform;
        c1.transform.position = rayMaskingPos[0] + new Vector3(-0.5f, 0, 0);
        //c1.transform.localScale = Vector3.one;

        Plane p1 = new Plane(a1.transform.position, b1.transform.position, c1.transform.position);
        //
        GameObject a2 = new GameObject("a2");
        a2.transform.parent = transform;
        a2.transform.position = rayMaskingPos[1];
        a2.transform.localScale = Vector3.one;

        GameObject b2 = new GameObject("b2");
        b2.transform.parent = transform;
        b2.transform.position = rayMaskingPos[1] + new Vector3(0.5f, 1, 0);
        b2.transform.localScale = Vector3.one;

        GameObject c2 = new GameObject("c2");
        c2.transform.parent = transform;
        c2.transform.position = rayMaskingPos[1] + new Vector3(0.5f, 0, 0);
        c2.transform.localScale = Vector3.one;

        Plane p2 = new Plane(a2.transform.position, b2.transform.position, c2.transform.position);


        //
        GameObject a3 = new GameObject("a3");
        a3.transform.parent = transform;
        a3.transform.position = rayMaskingPos[2];
        a3.transform.localScale = Vector3.one;

        GameObject b3 = new GameObject("b3");
        b3.transform.parent = transform;
        b3.transform.position = rayMaskingPos[2] + new Vector3(0, 1, 0.5f);
        b3.transform.localScale = Vector3.one;

        GameObject c3 = new GameObject("c3");
        c3.transform.parent = transform;
        c3.transform.position = rayMaskingPos[2] + new Vector3(0, 0, 0.5f);
        c3.transform.localScale = Vector3.one;

        Plane p3 = new Plane(a3.transform.position, b3.transform.position, c3.transform.position);

        //
        GameObject a4 = new GameObject("a4");
        a4.transform.parent = transform;
        a4.transform.position = rayMaskingPos[3];
        a4.transform.localScale = Vector3.one;

        GameObject b4 = new GameObject("b4");
        b4.transform.parent = transform;
        b4.transform.position = rayMaskingPos[3] + new Vector3(0, 1, 0);
        b4.transform.localScale = Vector3.one;

        GameObject c4 = new GameObject("c4");
        c4.transform.parent = transform;
        c4.transform.position = rayMaskingPos[3] + new Vector3(0, 0, -0.5f);
        c4.transform.localScale = Vector3.one;

        Plane p4 = new Plane(a4.transform.position, b4.transform.position, c4.transform.position);
        splitPlanes = new Plane[] { p1, p2, p3, p4 };
        return new Plane[] { p1, p2, p3, p4 };
    }
    public Plane[] CreatePlane()
    {
        //
        GameObject a1 = new GameObject("a1");
        a1.transform.parent = transform;
        a1.transform.localPosition = new Vector3(-0.5f, 0, 0);
        a1.transform.localScale = Vector3.one;
        GameObject b1 = new GameObject("b1"); ;
        b1.transform.parent = transform;
        b1.transform.localPosition = new Vector3(-0.5f, 1, 0);
        b1.transform.localScale = Vector3.one;

        GameObject c1 = new GameObject("c1");
        c1.transform.parent = transform;
        c1.transform.localPosition = new Vector3(-0.5f, 0, 1);
        c1.transform.localScale = Vector3.one;

        Plane p1 = new Plane(a1.transform.position, b1.transform.position, c1.transform.position);
        //
        GameObject a2 = new GameObject("a2");
        a2.transform.parent = transform;
        a2.transform.localPosition = new Vector3(0.5f, 0, 0);
        a2.transform.localScale = Vector3.one;

        GameObject b2 = new GameObject("b2");
        b2.transform.parent = transform;
        b2.transform.localPosition = new Vector3(0.5f, 1, 0);
        b2.transform.localScale = Vector3.one;

        GameObject c2 = new GameObject("c2");
        c2.transform.parent = transform;
        c2.transform.localPosition = new Vector3(0.5f, 0, 1);
        c2.transform.localScale = Vector3.one;

        Plane p2 = new Plane(a2.transform.position, b2.transform.position, c2.transform.position);


        //
        GameObject a3 = new GameObject("a3");
        a3.transform.parent = transform;
        a3.transform.localPosition = new Vector3(0, 0, 0.5f);
        a3.transform.localScale = Vector3.one;

        GameObject b3 = new GameObject("b3");
        b3.transform.parent = transform;
        b3.transform.localPosition = new Vector3(0, 1, 0.5f);
        b3.transform.localScale = Vector3.one;

        GameObject c3 = new GameObject("c3");
        c3.transform.parent = transform;
        c3.transform.localPosition = new Vector3(1, 0, 0.5f);
        c3.transform.localScale = Vector3.one;

        Plane p3 = new Plane(a3.transform.position, b3.transform.position, c3.transform.position);

        //
        GameObject a4 = new GameObject("a4");
        a4.transform.parent = transform;
        a4.transform.localPosition = new Vector3(0, 0, -0.5f);
        a4.transform.localScale = Vector3.one;

        GameObject b4 = new GameObject("b4");
        b4.transform.parent = transform;
        b4.transform.localPosition = new Vector3(0, 1, -0.5f);
        b4.transform.localScale = Vector3.one;

        GameObject c4 = new GameObject("c4");
        c4.transform.parent = transform;
        c4.transform.localPosition = new Vector3(1, 0, -0.5f);
        c4.transform.localScale = Vector3.one;

        Plane p4 = new Plane(a4.transform.position, b4.transform.position, c4.transform.position);
        splitPlanes = new Plane[] { p1, p2, p3, p4 };
        return new Plane[] { p1, p2, p3, p4 };
    }

}
