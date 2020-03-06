using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    private static ViewManager instance;
    public static ViewManager Getinstance()
    {
        return instance;
    }

    public GameObject mainView;
    public Button startBtn;
    public GameObject gameView;
    public Text scoreText;
    public GameObject resultView;
    public Button reGameBtn;
    public Text currentScore;
    public Text topScore;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameView.SetActive(false);
        resultView.SetActive(false);
        startBtn.onClick.AddListener(()=> {
            mainView.SetActive(false);
            gameView.SetActive(true);
            GetComponent<Main>().ReStartCreateCube();
        });
        reGameBtn.onClick.AddListener(()=> {
            gameView.SetActive(true);
            resultView.SetActive(false);
            SceneManager.LoadScene(0);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText.IsActive())
        {
            scoreText.text = GameManager.Getinstance().Score.ToString();
        }
    }

    public void GameOver()
    {
        gameView.SetActive(false);
        resultView.SetActive(true);
        if (!PlayerPrefs.HasKey("TopScore"))
        {
            PlayerPrefs.SetInt("TopScore",GameManager.Getinstance().Score);
            currentScore.text = GameManager.Getinstance().Score.ToString();
            topScore.text ="历史最佳得分: " +GameManager.Getinstance().Score.ToString();
        }
        else
        {
            if (PlayerPrefs.GetInt("TopScore")>= GameManager.Getinstance().Score)
            {
                currentScore.text = GameManager.Getinstance().Score.ToString();
                topScore.text = "历史最佳得分: " + PlayerPrefs.GetInt("TopScore").ToString();
            }
            else
            {
                PlayerPrefs.SetInt("TopScore", GameManager.Getinstance().Score);
                currentScore.text = GameManager.Getinstance().Score.ToString();
                topScore.text = "历史最佳得分: " + PlayerPrefs.GetInt("TopScore").ToString();
            }
        }


    }
}
