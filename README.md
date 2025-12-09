🌧️ Rtan Rain Dodge Game

Unity로 제작된 간단한 미니게임 프로젝트입니다.
좌우로 움직이는 Rtan 캐릭터를 조작하여 떨어지는 Rain 오브젝트를 피하거나 획득하며 점수를 얻는 게임입니다.

🎮 게임 설명

제한 시간 30초 동안 Rain이 랜덤 위치에 생성됩니다.

Rain은 종류에 따라 크기, 색상, 점수가 다릅니다.

Player와 부딪히면 점수를 얻고, 바닥에 떨어지면 사라집니다.

화면을 클릭하면 Rtan이 좌우 방향을 전환합니다.

시간이 0이 되면 게임이 멈추고 결과 창이 나타납니다.

🗂️ 스크립트 구성
Rtan.cs

캐릭터 이동 및 방향 전환 처리

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rtan : MonoBehaviour
{
    float direction = 0.05f;
    SpriteRenderer renderer;

    void Start()
    {
        Application.targetFrameRate = 60;
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            direction *= -1;
            renderer.flipX = !renderer.flipX;
        }
        if (transform.position.x > 2.49f)
        {
            renderer.flipX = true;
            direction = -0.05f;
        }
        if (transform.position.x < -2.49f)
        {
            renderer.flipX = false;
            direction = 0.05f;
        }

        transform.position += Vector3.right * direction;
    }
}

ReStart.cs

게임 재시작 버튼 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}

Rain.cs

Rain 오브젝트 생성 및 충돌 처리

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Rain : MonoBehaviour
{
    float size = 1.0f;
    int score = 1;

    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        float X = Random.Range(-2.4f, 2.4f);
        float y = Random.Range(3.0f, 5.0f);

        transform.position = new Vector3(X, y, 0);

        int type = Random.Range(1, 5);
        if(type == 1)
        {
            size = 0.8f;
            score = 1;
            renderer.color = new Color(100 / 255f, 100 / 255f, 1f, 1f);
        }
        else if(type == 2)
        {
            size = 1.0f;
            score = 2;
            renderer.color = new Color(130 / 255f, 130 / 255f, 1f, 1f);
        }
        else if(type == 3)
        {
            size = 1.2f;
            score = 3;
            renderer.color = new Color(150 / 255f, 150 / 255f, 1f, 1f);
        }
        else if (type == 4)
        {
            size = 0.8f;
            score = -5;
            renderer.color = new Color(255 / 255f, 100 / 255f, 100/ 255f);
        }

        transform.localScale = new Vector3(size, size, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.AddScore(score);
            Destroy(this.gameObject);
        }
    }
}

GameManager.cs

전체 게임 진행 관리 (점수, 타이머, Rain 생성 등)

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

    void Start()
    {
        InvokeRepeating("MakeRain", 0f, 1f);
        MakeRain();
    }

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
    }
}

▶️ 실행 방법

Unity에서 프로젝트 열기

MainScene 실행

마우스 클릭으로 Rtan 방향 변경

떨어지는 Rain을 피하거나 먹으며 점수 획득

30초 후 게임 종료 → 결과창 표시
