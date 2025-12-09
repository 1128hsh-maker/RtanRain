using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject rain;
    public static GameManager instance;
    public GameObject EndPanel;

    public Text totalScoreTxt;
    public Text timeTxt;

    int totalScore;

    float totalTime = 30.0f;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeRain", 0f, 1f);
        MakeRain();
    }

    // Update is called once per frame
    void Update()
    {
        if(totalTime > 0f)
        {
            totalTime -= Time.deltaTime;
            
        }
        else if (totalTime <= 0f)
        {
            Time.timeScale = 0f;
            EndPanel.SetActive(true);

        }

        timeTxt.text = totalTime.ToString("N2");
    }

    void MakeRain()
        {
        Instantiate(rain);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        totalScoreTxt.text = totalScore.ToString();
        Debug.Log(totalScore);

    }
}
