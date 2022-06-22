using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private UnityEvent onReset;

    public static GameManager instance;

    private GameObject readyPanel;
    private Text scoreText;
    private Text bestScoreText;
    private Text messageText;

    private bool isRoundActive = false;

    private int score = 0;

    private ShooterRotator shooterRotator;
    private CamFollow cam;

    private void Awake()
    {
        instance = this;

        GameObject uiCanvas = GameObject.Find("UICanvas");
        readyPanel = uiCanvas.transform.Find("Round Ready Panel").gameObject;
        scoreText = uiCanvas.transform.Find("Info Bar/Score Text").GetComponent<Text>();
        bestScoreText = uiCanvas.transform.Find("Info Bar/Best Score Text").GetComponent<Text>();
        messageText = readyPanel.GetComponentInChildren<Text>();

        shooterRotator = FindObjectOfType<ShooterRotator>();
        cam = FindObjectOfType<CamFollow>();

        SpawnGenerator spawner = FindObjectOfType<SpawnGenerator>();
        onReset = new UnityEvent();
        onReset.AddListener(spawner.Reset);
    }

    private void Start()
    {
        UpdateUI();
        StartCoroutine("RoundRoutine");
    }

    public void AddScore(int _newScore)
    {
        score += _newScore;
        UpdateBestScore();
        UpdateUI();
    }

    private void UpdateBestScore()
    {
        if(GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    private int GetBestScore() => PlayerPrefs.GetInt("BestScore");

    private void UpdateUI()
    {
        scoreText.text = $"Score : {score}";
        bestScoreText.text = $"Best Score : {GetBestScore()}";
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    private void Reset()
    {
        score = 0;
        UpdateUI();

        StartCoroutine("RoundRoutine");
    }

    private IEnumerator RoundRoutine()
    {
        // Ready
        onReset.Invoke();

        readyPanel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.IDLE);
        shooterRotator.enabled = false;

        isRoundActive = false;

        messageText.text = "Ready...";

        yield return new WaitForSeconds(3f);

        // Play
        isRoundActive = true;
        readyPanel.SetActive(false);
        shooterRotator.enabled = true;

        cam.SetTarget(shooterRotator.transform, CamFollow.State.READY);

        while(isRoundActive)
        {
            yield return null;
        }

        // End
        readyPanel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait For Next Round...";

        yield return new WaitForSeconds(3f);
        Reset();
    }
}
