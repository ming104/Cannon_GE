using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("타이머")]
    private float _timer;
    public Text _timer_Text;
    private bool isPlaying;

    [Header("점수")]
    public int score;
    public Text Score_Text;

    [Header("게임 끝")]
    public GameObject GameEndPanel;
    public Text GameEndScoreText;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        GameEndPanel.SetActive(false);
        score = 0;
        StartCoroutine(GameScheduler());
        Time.timeScale = 1f;
    }

    void Update()
    {
        Score_Text.text = "Score : " + score;
    }

    private IEnumerator GameScheduler()
    {
        _timer = 60f;
        isPlaying = true;

        while (_timer > 0 && isPlaying)
        {
            _timer -= Time.deltaTime;
            string minutes = Mathf.Floor(_timer / 60).ToString("00");
            string seconds = (_timer % 60).ToString("00");
            _timer_Text.text = string.Format("{0}:{1}", minutes, seconds);
            yield return null;

            if (_timer <= 0)
            {
                GameEnd();
            }
        }

    }

    private void GameEnd()
    {
        GameEndScoreText.text = "Score : " + score;
        GameEndPanel.SetActive(true);
        Time.timeScale = 0f;
        StopAllCoroutines();
        isPlaying = false;
    }


    public void ReGameButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }

    public void OffGameButton()
    {
        Application.Quit(0);
    }
}
