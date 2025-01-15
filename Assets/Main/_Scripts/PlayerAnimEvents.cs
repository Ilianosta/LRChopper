using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField] private ParticleSystem punchPS;
    [SerializeField] private Transform punchPSPosition;
    public void PunchParticle()
    {
        Instantiate(punchPS.gameObject, punchPSPosition.position, Quaternion.identity);
        CameraManager.instance.ShakeCamera(2, 0.3f);
    }

    public void SendPunchAudioClip()
    {
        AudioManager.Instance.PlayChopSFX();
    }
}
