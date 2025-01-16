using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField] private ObstacleGenerator obstacleGenerator;
    [SerializeField] private Button btnLeft, btnRight;
    PlayerInput playerInput;

    public event Action OnPressStart;
    public event Action OnLeftPressed;
    public event Action OnRightPressed;

    InputControl actualDevice;
    private void Awake()
    {
        if (InputManager.instance) Destroy(this);
        else InputManager.instance = this;

        playerInput = GetComponent<PlayerInput>();

        playerInput.actions.All(a =>
        {
            a.performed += ctx => OnInputPerformed(ctx);
            return true;
        });

        actualDevice = playerInput.devices[0];

        btnLeft.onClick.AddListener(() => OnLeft(null));
        btnRight.onClick.AddListener(() => OnRight(null));
    }

    private void OnInputPerformed(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        actualDevice = device;

        UIManager.instance.UpdateInputTexts();

        if (actualDevice.displayName == "Touchscreen")
        {
            UIManager.instance.ShowTouchPanel(true);
        }
        else
        {
            UIManager.instance.ShowTouchPanel(false);
        }
        Debug.Log("Dispositivo detectado: " + device.displayName);
    }
    public string GetBindingForAction(string actionName)
    {
        InputAction action = playerInput.actions.Where(a => a.name == actionName).FirstOrDefault();

        if (actualDevice.displayName == "Mouse") actualDevice = playerInput.GetDevice<Keyboard>();
        if(actualDevice.displayName == "Touchscreen") return "anywhere";


        InputControl control = action.controls.Where(c => c.device == actualDevice).FirstOrDefault();
        // Debug.Log("Action: " + action.name + " || Binding: " + control.displayName);

        return control.displayName;
    }

    public void OnLeft(InputValue value)
    {
        if (!GameManager.instance.gameStarted) return;

        OnLeftPressed?.Invoke();
        Obstacle obstacle = obstacleGenerator.actualObstacles.First();

        if (obstacle.isLeft) obstacle.DestroyObstacle();
        else GameManager.instance.Lose();
    }

    public void OnRight(InputValue value)
    {
        if (!GameManager.instance.gameStarted) return;

        OnRightPressed?.Invoke();
        Obstacle obstacle = obstacleGenerator.actualObstacles.First();

        if (!obstacle.isLeft) obstacle.DestroyObstacle();
        else GameManager.instance.Lose();
    }

    public void OnStart()
    {
        if (GameManager.instance.gameStarted) return;

        OnPressStart?.Invoke();
    }
}
