using UnityEngine;

public class UI_ButtonAnim : MonoBehaviour
{
    [SerializeField] private Transform pointShow, pointHide;
    [SerializeField] private RectTransform buttonRect;
    [SerializeField] private float duration = 0.25f;
    float transition = -1;
    bool isShowing;
    public void Show()
    {
        isShowing = !isShowing;
        transition = duration;
    }

    private void Update()
    {
        if (transition < 0) return;

        transition -= Time.deltaTime;

        if (!isShowing) buttonRect.position = Vector3.Lerp(pointHide.position, pointShow.position, transition / duration);
        else buttonRect.position = Vector3.Lerp(pointShow.position, pointHide.position, transition / duration);
    }
}
