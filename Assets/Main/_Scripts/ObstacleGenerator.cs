using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public List<Obstacle> actualObstacles = new List<Obstacle>();
    [SerializeField] private List<Obstacle> obstacles = new List<Obstacle>();
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Transform spawnPosition;
    private float xLeft => spawnPosition.position.x - 2.5f;
    private float xRight => spawnPosition.position.x + 2.5f;
    private float yTop => spawnPosition.position.y;

    public static event System.Action OnCreatedObstacle;

    private void OnEnable()
    {
        Obstacle.OnObstacleDestroyed += ResetObstacle;
        Obstacle.OnObstacleDestroyed += m => InstantiateObstacle();
    }
    private void OnDisable()
    {
        Obstacle.OnObstacleDestroyed -= ResetObstacle;
        Obstacle.OnObstacleDestroyed -= m => InstantiateObstacle();
    }

    private void Awake()
    {
        GeneratePool();
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            InstantiateObstacle();
        }
    }

    private void GeneratePool()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateObstacle();
        }
    }

    private void CreateObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Count);
        Vector3 position = new Vector3(Random.Range(0, 2) == 0 ? xLeft : xRight, yTop, 0);
        GameObject obstacle = Instantiate(obstacles[randomIndex].gameObject, position, Quaternion.identity, spawnPosition);
        obstacle.GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        obstacle.SetActive(false);
        obstacles.Add(obstacle.GetComponent<Obstacle>());
    }

    public Obstacle GetObstacle()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (!obstacles[i].gameObject.activeSelf)
            {
                return obstacles[i];
            }
        }

        CreateObstacle();
        return obstacles[obstacles.Count - 1];
    }

    public void ResetObstacles()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].gameObject.SetActive(false);
        }
        actualObstacles.Clear();
    }

    public void ResetObstacle(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
        actualObstacles.Remove(obstacle);
    }

    public void InstantiateObstacle()
    {
        OnCreatedObstacle?.Invoke();
        Obstacle obstacle = GetObstacle();
        obstacle.gameObject.SetActive(true);

        Vector3 position = new Vector3(Random.Range(0, 2) == 0 ? xLeft : xRight, yTop, 0);
        obstacle.transform.position = position;
        actualObstacles.Add(obstacle);
    }
}
