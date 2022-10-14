
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputSystemMain inputSystem;

    public delegate void StartTouchEvent(Vector3 position, float time);
    public event StartTouchEvent onStartTouch;
    void Awake()
    {
        inputSystem = new InputSystemMain();
    }

    void Start()
    {
        inputSystem.Touch.TouchPress.started += ctx => StartTouch(ctx);
        inputSystem.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }
    private void OnEnable()
    {
        inputSystem.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Disable();
    }
    void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Start Touch");
        Debug.Log(inputSystem.Touch.TouchPosition.ReadValue<Vector2>());
        if (onStartTouch != null)
        {
            onStartTouch(inputSystem.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
            print(inputSystem.Touch.TouchPosition.ReadValue<Vector2>().x);
        }
    }
    void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("End Touch");

    }
}
