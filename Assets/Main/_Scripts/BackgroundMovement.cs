using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [System.Serializable]
    private class BackgroundPiece
    {
        public Transform transform;
        public float limitX;
        [HideInInspector] public float speed;
        [HideInInspector] public bool movingRight;
        public void RandomizeSpeed()
        {
            speed = Random.Range(0.5f, 1.5f);
        }

        public void Move()
        {
            transform.position += (movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime;
        }
    }

    [SerializeField] private BackgroundPiece[] backgroundPieces;

    private void Update()
    {
        foreach (var piece in backgroundPieces)
        {
            piece.Move();
            if (piece.transform.position.x > piece.limitX || piece.transform.position.x < -piece.limitX)
            {
                Vector3 newPos = piece.transform.position;
                bool spawnRight = Random.Range(0, 2) == 0;
                newPos.x = spawnRight ? 10 : -10;
                piece.transform.position = newPos;
                piece.movingRight = !spawnRight;
                piece.RandomizeSpeed();
            }
        }
    }
}
