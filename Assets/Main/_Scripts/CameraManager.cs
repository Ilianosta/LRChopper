using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin perlinNoise;
    private float shakeTimer;
    private float shakeIntensity;
    private float shakeTimerTotal;

    private void Awake()
    {
        if (CameraManager.instance) Destroy(this);
        else CameraManager.instance = this;
    }

    public void ShakeCamera(float intensity, float duration)
    {
        if (perlinNoise != null)
        {
            perlinNoise.AmplitudeGain = intensity;
            shakeIntensity = intensity;
            shakeTimerTotal = duration;
            shakeTimer = duration;
        }
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            // Gradualmente reduce la intensidad
            if (perlinNoise != null)
            {
                perlinNoise.AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
            }

            if (shakeTimer <= 0f && perlinNoise != null)
            {
                // Finaliza el temblor
                perlinNoise.AmplitudeGain = 0f;
            }
        }
    }
}
