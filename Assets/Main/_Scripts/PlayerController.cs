using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform leftPos, rightPos;
    SpriteRenderer spriteRenderer;
    Animator animator;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        InputManager.instance.OnLeftPressed += MoveLeft;
        InputManager.instance.OnRightPressed += MoveRight;
    }

    private void OnDisable()
    {
        InputManager.instance.OnLeftPressed -= MoveLeft;
        InputManager.instance.OnRightPressed -= MoveRight;
    }

    private void MoveLeft()
    {
        transform.position = leftPos.position;
        RotateTo(false);
        animator.Play("punch");
    }

    private void MoveRight()
    {
        transform.position = rightPos.position;
        RotateTo(true);
        animator.Play("punch");
    }

    private void RotateTo(bool left)
    {
        transform.localScale = new Vector3(left ? -1 : 1, 1, 1);
    }
}
