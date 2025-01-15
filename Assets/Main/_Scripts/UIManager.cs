using System;
using AssetKits.ParticleImage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject startPanel, gamePanel, losePanel;
    [SerializeField] private Image timeToClickImage;
    [SerializeField] private Image difficultyImg;
    [SerializeField] private TMP_Text pointsText, losePointsText, startText, highScoreText;
    [SerializeField] private ParticleImage puffParticle;
    [SerializeField] private UIMovingItem clock;
    private void Awake()
    {
        if (UIManager.instance) Destroy(this);
        else UIManager.instance = this;

        pointsText.text = "0";
    }

    private void Start()
    {
        UpdateInputTexts();
    }

    private void Update()
    {
        if (GameManager.instance.gameStarted)
        {
            Color difficultyColor = difficultyImg.color;
            difficultyColor.a = Mathf.Lerp(0.20f, 0, GameManager.instance.GetDifficultyPercentage());
            // Debug.Log("Difficulty percentage: " + GameManager.instance.GetDifficultyPercentage() + " || Color: " + difficultyColor.ToString());
            difficultyImg.color = difficultyColor;
        }
        else
        {
            Color difficultyColor = difficultyImg.color;
            difficultyColor.a = 0;
            difficultyImg.color = difficultyColor;
        }

    }

    public void UpdateTimeToClick(float time, float limitTime)
    {
        float percentage = time / limitTime;
        timeToClickImage.fillAmount = percentage;
        clock.UpdatePosition(percentage);
        // Debug.Log("Percentage" + percentage);
    }

    public void ShowStartPanel()
    {
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        losePanel.SetActive(false);
        pointsText.text = "0";
    }

    public void ShowGamePanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        losePanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
        losePointsText.text = pointsText.text;
        highScoreText.text = GameManager.instance.GetHighScore().ToString();
        PlayParticlePuff();
    }

    public void PlayParticlePuff()
    {
        puffParticle.Play();
    }

    public void UpdatePoints(float points)
    {
        pointsText.text = points.ToString();
    }

    public void UpdateInputTexts()
    {
        string binding = InputManager.instance.GetBindingForAction("Start");
        startText.text = "Press " + binding + " to start";
    }

    [System.Serializable]
    public class UIMovingItem
    {
        public RectTransform rectTransform;
        public RectTransform pointA, pointB;

        public void UpdatePosition(float percentage)
        {
            rectTransform.position = Vector2.Lerp(pointA.position, pointB.position, percentage);
        }
    }
}

