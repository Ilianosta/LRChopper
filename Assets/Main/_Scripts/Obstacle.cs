using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isLeft => transform.position.x < 0;
    public float pointsToAdd = 1;
    public static event System.Action<Obstacle> OnObstacleDestroyed;

    private void OnDisable()
    {
        ObstacleGenerator.OnCreatedObstacle -= MoveNext;
    }

    private void OnEnable()
    {
        ObstacleGenerator.OnCreatedObstacle += MoveNext;
    }

    public void DestroyObstacle()
    {
        OnObstacleDestroyed?.Invoke(this);
    }

    private void MoveNext()
    {
        Vector3 actualPosition = transform.position;
        Vector3 targetPosition = actualPosition;
        targetPosition.y -= 1.5f;
        transform.position = targetPosition;
    }
}
