using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRay : MonoBehaviour
{
    public Vector3 hitPos;
    public string hitNaem;
    public int dir;
    private void Start()
    {
    }
    // Update is called once per frame
    public Vector3 Create()
    {
        Vector3 dirP = Vector3.zero;
        if (dir == 1) dirP = Vector3.forward;
        if (dir == 2) dirP = -Vector3.forward;
        if (dir == 3) dirP = Vector3.right;
        if (dir == 4) dirP = -Vector3.right;

        Ray ray = new Ray(transform.position, dirP); //创建一条射线对象
        RaycastHit hit;//碰撞信息对象结构体
        bool isRaycast = Physics.Raycast(ray, out hit);
        if (isRaycast)
        {
            Debug.LogError(1);
            hitPos = hit.point;
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            go.transform.position = hitPos;
            go.transform.parent = hit.transform;
            go.GetComponent<SphereCollider>().enabled = false;
            hitNaem = hit.transform.name;
        }
        else
        {
            Debug.LogError("HitInfo Is Null!!"+ hitPos);
        }
        return hitPos;
    }
}
