using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    private static Split instance;
    public static Split Getinstance() {
        return instance;
    }
    public float cubeHeight;
    public List<Transform> markingPoints;
    public  List<Vector3> rayMarkingPoints;
    public GameObject targetCube;
    public GameObject splitCube;
    private bool isZ ;
    public  bool isRay;
    private void Awake()
    {
        instance = this;
        rayMarkingPoints = new List<Vector3>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ReStartCreateCube();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SplitCube());
        }
    }

    IEnumerator SplitCube()
    {
        if (targetCube != null)
        {
            DOTween.KillAll();
            //targetCube.GetComponent<Rigidbody>().useGravity =true;
             //targetCube.SendMessage("Split", splitCube.splitPlanes, SendMessageOptions.DontRequireReceiver);
             var newGameObjects = targetCube.GetComponent<ShatterTool>().Split(splitCube.GetComponent<SplitTool>().splitPlanes);
            yield return new WaitForSeconds(0.05f);

            if (newGameObjects == null || newGameObjects.Length == 0)
            {
                Debug.Log("Defeated!!");
            }
            else
            {
                splitCube.GetComponent<SplitTool>().enabled = false;
                splitCube.GetComponent<ShatterTool>().enabled = false;
                splitCube.GetComponent<UvMapper>().enabled = false;

                for (int i = 0; i < splitCube.GetComponentsInChildren<Transform>().Length; i++)
                {
                    if (i != 0)
                    {
                        Destroy(splitCube.GetComponentsInChildren<Transform>()[i].gameObject);
                    }
                }
                if (newGameObjects.Length == 1)
                {
                    splitCube = newGameObjects[0];
                    Debug.Log("Perfect!!");
                }
                else if (newGameObjects.Length == 2)
                {
                    if (newGameObjects[0].transform.localPosition.y <= newGameObjects[1].transform.localPosition.y)
                    {
                        if (newGameObjects[0].GetComponent<Rigidbody>().mass <= newGameObjects[1].GetComponent<Rigidbody>().mass)
                        {
                            splitCube = newGameObjects[1];
                           
                            newGameObjects[0].GetComponent<MeshCollider>().enabled =false;
                            newGameObjects[0].GetComponent<Rigidbody>().useGravity = true;
                            newGameObjects[0].GetComponent<Rigidbody>().isKinematic = false;
                            Destroy(newGameObjects[0], 0.5f);
                        }
                        else
                        {
                            splitCube = newGameObjects[0];
                            newGameObjects[1].GetComponent<MeshCollider>().enabled = false;
                            newGameObjects[1].GetComponent<Rigidbody>().useGravity = true;
                            newGameObjects[1].GetComponent<Rigidbody>().isKinematic = false;

                            Destroy(newGameObjects[1], 0.5f);
                        }
                    }

                    if (newGameObjects[0].transform.localPosition.y > newGameObjects[1].transform.localPosition.y)
                    {
                        if (newGameObjects[0].GetComponent<Rigidbody>().mass >= newGameObjects[1].GetComponent<Rigidbody>().mass)
                        {
                            splitCube = newGameObjects[0];
                            newGameObjects[1].GetComponent<MeshCollider>().enabled = false;
                            newGameObjects[1].GetComponent<Rigidbody>().useGravity = true;
                            newGameObjects[1].GetComponent<Rigidbody>().isKinematic = false;

                            Destroy(newGameObjects[1], 0.5f);
                        }
                        else
                        {
                            splitCube = newGameObjects[1];
                            newGameObjects[0].GetComponent<MeshCollider>().enabled = false;
                            newGameObjects[0].GetComponent<Rigidbody>().useGravity = true;
                            newGameObjects[0].GetComponent<Rigidbody>().isKinematic = false;

                            Destroy(newGameObjects[0], 0.5f);
                        }
                    }

                }
                rayMarkingPoints.Clear();
                foreach (var item in markingPoints)
                {
                    rayMarkingPoints.Add(item.GetComponent<CreateRay>().Create());
                }
                splitCube.GetComponent<SplitTool>().CreatePlane(rayMarkingPoints);
                ReStartCreateCube();
            }

        }
    }

    public void ReStartCreateCube()
    {
        isRay = false;
        foreach (var item in markingPoints)
        {
            item.position += new Vector3(0,cubeHeight,0);
        }
        var tempTarget = Instantiate(splitCube.gameObject);
        tempTarget.name = "cube";
        tempTarget.GetComponent<SplitTool>().isBaseCube =false;
        for (int i = 0; i < tempTarget.GetComponentsInChildren<Transform>().Length; i++)
        {
            if (i != 0)
            {
                Destroy(tempTarget.GetComponentsInChildren<Transform>()[i].gameObject);
            }
        }
        if (isZ)
        {
            markingPoints[0].transform.position = new Vector3(splitCube.transform.position.x, markingPoints[0].transform.position.y, markingPoints[0].transform.position.z);
            markingPoints[1].transform.position = new Vector3(splitCube.transform.position.x, markingPoints[1].transform.position.y, markingPoints[1].transform.position.z);
            tempTarget.transform.position = markingPoints[0].transform.position;
            tempTarget.transform.DOMove(markingPoints[1].transform.position, 3).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            markingPoints[2].transform.position = new Vector3(markingPoints[2].transform.position.x, markingPoints[2].transform.position.y, splitCube.transform.position.z);
            markingPoints[3].transform.position = new Vector3(markingPoints[3].transform.position.x, markingPoints[3].transform.position.y, splitCube.transform.position.z);
            tempTarget.transform.position = markingPoints[2].transform.position;
            tempTarget.transform.DOMove(markingPoints[3].transform.position, 3).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        isZ = !isZ;
        tempTarget.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        targetCube = tempTarget;
    }
}
