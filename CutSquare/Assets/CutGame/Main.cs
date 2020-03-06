using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main instance;
    public static Main Getinstance()
    {
        return instance;
    }
    public BGColor bgColor;
    public AudioSource audio;
    public float cubeHeight;
    public List<Transform> markingPoints;
    public float moveSpeed;
    public Transform splitCube;
    private Transform targetCube;

    private bool isToX ;
   
    private int forceIntensity =100;
    private int perfectTimes;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        splitCube.GetComponent<MeshRenderer>().material.color = GameManager.Getinstance().colors[0];
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameSartSplit();
        }
    }

    public void GameSartSplit()
    {
        if (targetCube != null)
        {
            DOTween.KillAll();
            GetSplitGameObject();
        }
    }
    void GetSplitGameObject()
    {
        GameObject newGameObject1;
        GameObject newGameObject2;
        float scale1;
        float scale2;
        float pos1;
        float pos2;
        //当沿x轴移动的时候
        if (isToX)
        {
            if (Mathf.Abs(targetCube.position.x - splitCube.position.x) <= 0.5f)
            {
                targetCube.position = new Vector3(splitCube.position.x, targetCube.position.y, splitCube.position.z);
                splitCube = targetCube;
                perfectTimes += 1;
                PlayAudio(perfectTimes.ToString());
                perfectTimes = perfectTimes > 6 ? 6 : perfectTimes;
                GameManager.Getinstance().Score = GameManager.Getinstance().Score+perfectTimes+1;
                Debug.LogError("Prefect!!");
            }
            else if (Mathf.Abs(targetCube.position.x - splitCube.position.x) > targetCube.localScale.x)
            {
                Debug.LogError("GameOver!!");
                targetCube.gameObject.AddComponent<Rigidbody>();
                ViewManager.Getinstance().GameOver();
                return;
            }
            else
            {
                perfectTimes = 0;
                PlayAudio("Cut");
                GameManager.Getinstance().Score++;
                //如果targetCube在splitCube 的x轴正方向
                if (targetCube.position.x > splitCube.position.x)
                {
                    scale1 = splitCube.localScale.x - (targetCube.position.x - splitCube.position.x);
                    scale2 = targetCube.localScale.x - scale1;
                    pos1 = splitCube.position.x + (targetCube.position.x - splitCube.position.x) / 2;
                    pos2 = scale2 / 2 + splitCube.position.x + splitCube.localScale.x / 2;
                }
                else  //如果targetCube在splitCube 的x轴负方向
                {
                    scale1 = splitCube.localScale.x - Mathf.Abs((targetCube.position.x - splitCube.position.x));
                    scale2 = targetCube.localScale.x - scale1;
                    pos1 = splitCube.position.x - Mathf.Abs(targetCube.position.x - splitCube.position.x) / 2;
                    pos2 = splitCube.position.x - scale2 / 2 - splitCube.localScale.x / 2;
                }
                newGameObject1 = Instantiate(targetCube.gameObject);
                newGameObject1.transform.position = new Vector3(pos1, targetCube.position.y, targetCube.position.z);
                newGameObject1.transform.localScale = new Vector3(scale1, cubeHeight, targetCube.localScale.z);
                //newGameObject1.AddComponent<Rigidbody>();
                newGameObject2 = Instantiate(targetCube.gameObject);
                newGameObject2.transform.position = new Vector3(pos2, targetCube.position.y, targetCube.position.z);
                newGameObject2.transform.localScale = new Vector3(scale2, cubeHeight, targetCube.localScale.z);
                //newGameObject2.AddComponent<Rigidbody>();

                //如果targetCube在splitCube 的x轴正方向，判断新生成的两个cube哪一个才是需要的，并且舍弃另外一个
                if (targetCube.position.x > splitCube.position.x)
                {
                    if (newGameObject1.transform.position.x > newGameObject2.transform.position.x)
                    {
                        splitCube = newGameObject2.transform;
                        newGameObject1.AddComponent<Rigidbody>().AddForce(Vector3.right* forceIntensity);
                        Destroy(newGameObject1, 1);
                    }
                    else
                    {
                        splitCube = newGameObject1.transform;
                        newGameObject2.AddComponent<Rigidbody>().AddForce(Vector3.right * forceIntensity);
                        Destroy(newGameObject2, 1);
                    }
                }
                else
                {
                    if (newGameObject1.transform.position.x > newGameObject2.transform.position.x)
                    {
                        splitCube = newGameObject1.transform;
                        newGameObject2.AddComponent<Rigidbody>().AddForce(Vector3.left* forceIntensity);
                      
                        Destroy(newGameObject2, 1);
                    }
                    else
                    {
                        splitCube = newGameObject2.transform;
                        newGameObject1.AddComponent<Rigidbody>().AddForce(Vector3.left * forceIntensity);
                        Destroy(newGameObject1, 1);
                    }
                }
              

                Destroy(targetCube.gameObject);
            }
        }
        else //当沿z轴移动的时候
        {
            if (Mathf.Abs(targetCube.position.z - splitCube.position.z) <= 0.5f)
            {
                targetCube.position = new Vector3(splitCube.position.x, targetCube.position.y, splitCube.position.z);
                splitCube = targetCube;
                Debug.LogError("Prefect!!");
                perfectTimes += 1;
                perfectTimes = perfectTimes > 6 ? 6 : perfectTimes;
                PlayAudio(perfectTimes.ToString());
                GameManager.Getinstance().Score = GameManager.Getinstance().Score + perfectTimes + 1;
            }
            else if (Mathf.Abs(targetCube.position.z - splitCube.position.z) > targetCube.localScale.z)
            {
                Debug.LogError("GameOver!!");
                targetCube.gameObject.AddComponent<Rigidbody>();
                ViewManager.Getinstance().GameOver();

                return;
            }
            else
            {
                perfectTimes = 0;
                PlayAudio("Cut");
                GameManager.Getinstance().Score++;
                if (targetCube.position.z > splitCube.position.z)
                {
                    scale1 = splitCube.localScale.z - (targetCube.position.z - splitCube.position.z);
                    scale2 = targetCube.localScale.z - scale1;
                    pos1 = splitCube.position.z + (targetCube.position.z - splitCube.position.z) / 2;
                    pos2 = scale2 / 2 + splitCube.position.z + splitCube.localScale.z / 2;
                }
                else
                {
                    scale1 = splitCube.localScale.z - Mathf.Abs((targetCube.position.z - splitCube.position.z));
                    scale2 = targetCube.localScale.z - scale1;
                    pos1 = splitCube.position.z - Mathf.Abs(targetCube.position.z - splitCube.position.z) / 2;
                    pos2 = splitCube.position.z - scale2 / 2 - splitCube.localScale.z / 2;
                }
             
                newGameObject1 = Instantiate(targetCube.gameObject);
                newGameObject1.transform.position = new Vector3(targetCube.position.x, targetCube.position.y, pos1);
                newGameObject1.transform.localScale = new Vector3(targetCube.localScale.x, cubeHeight, scale1);
                //newGameObject1.AddComponent<Rigidbody>();
                newGameObject2 = Instantiate(targetCube.gameObject);
                newGameObject2.transform.position = new Vector3(targetCube.position.x, targetCube.position.y, pos2);
                newGameObject2.transform.localScale = new Vector3(targetCube.localScale.x, cubeHeight, scale2);
                //newGameObject2.AddComponent<Rigidbody>();
                if (targetCube.position.z > splitCube.position.z)
                {
                    if (newGameObject1.transform.position.z > newGameObject2.transform.position.z)
                    {
                        splitCube = newGameObject2.transform;
                        newGameObject1.AddComponent<Rigidbody>().AddForce(Vector3.forward* forceIntensity);
                        Destroy(newGameObject1, 1);
                    }
                    else
                    {
                        splitCube = newGameObject1.transform;
                        newGameObject2.AddComponent<Rigidbody>().AddForce(Vector3.forward * forceIntensity);
                        Destroy(newGameObject2, 1); 
                    }
                }
                else
                {
                    if (newGameObject1.transform.position.z > newGameObject2.transform.position.z)
                    {
                        splitCube = newGameObject1.transform;
                        newGameObject2.AddComponent<Rigidbody>().AddForce(Vector3.back* forceIntensity);
                        Destroy(newGameObject2, 1);
                    }
                    else
                    {
                        splitCube = newGameObject2.transform;
                        newGameObject1.AddComponent<Rigidbody>().AddForce(Vector3.back * forceIntensity);
                        Destroy(newGameObject1, 1);
                    }
                }
              
                Destroy(targetCube.gameObject);
            }
        }
      
        ReStartCreateCube();
    }
    public void ReStartCreateCube()
    {
        GameManager.Getinstance().CubeNum++;
        isToX = !isToX;
        if (GameManager.Getinstance().CubeNum > 3)Camera.main.transform.DOMoveY(Camera.main.transform.position.y + cubeHeight*1f, 0.5f);
        foreach (var item in markingPoints)
        {
            item.position += new Vector3(0, cubeHeight, 0);
        }
        var tempTarget = Instantiate(splitCube.gameObject);
        Destroy(tempTarget.GetComponent<Rigidbody>());
        tempTarget.name = "cube";
        if (isToX)
        {
            markingPoints[2].transform.position = new Vector3(markingPoints[2].transform.position.x, markingPoints[2].transform.position.y, splitCube.transform.position.z);
            markingPoints[3].transform.position = new Vector3(markingPoints[3].transform.position.x, markingPoints[3].transform.position.y, splitCube.transform.position.z);
            tempTarget.transform.position = markingPoints[2].transform.position;
            tempTarget.transform.DOMove(markingPoints[3].transform.position, moveSpeed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            markingPoints[0].transform.position = new Vector3(splitCube.position.x, markingPoints[0].transform.position.y, markingPoints[0].transform.position.z);
            markingPoints[1].transform.position = new Vector3(splitCube.position.x, markingPoints[1].transform.position.y, markingPoints[1].transform.position.z);
            tempTarget.transform.position = markingPoints[1].transform.position;
            tempTarget.transform.DOMove(markingPoints[0].transform.position, moveSpeed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
       
        tempTarget.GetComponent<MeshRenderer>().material.color = GameManager.Getinstance().colors[GameManager.Getinstance().CubeNum];
        targetCube = tempTarget.transform;
    }
   
    private void PlayAudio(string audioName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audios/"+ audioName);
        audio.clip = clip;
        audio.Play();
    }
    
}
