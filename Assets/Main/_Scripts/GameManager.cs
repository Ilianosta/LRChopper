using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] float timeToClick = 3f;
    [SerializeField] AnimationCurve timeToClickCurve;
    float score;
    float actualTimeToClick;
    float limitTimeToClick = 0.25f;

    const string HIGHSCORE_SAVEKEY = "HighScoreKey";

    public bool gameStarted { get; private set; }

    private void Awake()
    {
        if (GameManager.instance) Destroy(this);
        else GameManager.instance = this;

        gameStarted = false;
        SuscribeToStart(true);

        Obstacle.OnObstacleDestroyed += AddScore;
    }

    private void Start()
    {
        UIManager.instance.ShowStartPanel();
    }

    private void SuscribeToStart(bool suscribe)
    {
        if (suscribe) InputManager.instance.OnPressSomething += StartGame;
        else InputManager.instance.OnPressSomething -= StartGame;
    }

    private void Update()
    {
        if (!gameStarted) return;

        actualTimeToClick -= Time.deltaTime;
        UIManager.instance.UpdateTimeToClick(actualTimeToClick, GetClampedLimitTimeToClick());

        if (actualTimeToClick <= 0)
        {
            Lose();
        }
    }

    public void StartGame()
    {
        score = 0;
        actualTimeToClick = timeToClick;
        UIManager.instance.ShowGamePanel();
        gameStarted = true;
        SuscribeToStart(false);
    }

    public void AddScore(Obstacle obstacle)
    {
        score++;
        UIManager.instance.UpdatePoints(score);
        actualTimeToClick = GetClampedLimitTimeToClick();
        // Debug.Log("Actual time to click: " + actualTimeToClick);
    }

    public void Lose()
    {
        gameStarted = false;
        CheckSaveKey(HIGHSCORE_SAVEKEY, score);
        UIManager.instance.ShowLosePanel();
    }

    public void RestartGame()
    {
        SuscribeToStart(true);
        UIManager.instance.ShowStartPanel();
    }

    private float GetClampedLimitTimeToClick()
    {
        return Mathf.Clamp(timeToClick * timeToClickCurve.Evaluate(score / 10), limitTimeToClick, timeToClick);
    }

    private float CheckSaveKey(string saveKey, float value)
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            if (PlayerPrefs.GetFloat(saveKey) < value)
            {
                PlayerPrefs.SetFloat(saveKey, value);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(saveKey, value);
        }
        return PlayerPrefs.GetFloat(saveKey);
    }

    public float GetHighScore()
    {
        return PlayerPrefs.GetFloat(HIGHSCORE_SAVEKEY);
    }
}
