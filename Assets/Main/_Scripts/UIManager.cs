using AssetKits.ParticleImage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject startPanel, gamePanel, losePanel;
    [SerializeField] private Image timeToClickImage;
    [SerializeField] private TMP_Text pointsText, losePointsText, startText;
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

