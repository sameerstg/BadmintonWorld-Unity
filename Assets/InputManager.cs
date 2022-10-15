using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager _instance;
    public InputSystemMain inputSystem;

    public delegate void StartTouchEvent(Vector3 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void StayTouchEvent(Vector3 position);
    public event StayTouchEvent OnStayTouch;
    public delegate void EndTouchEvent(Vector3 position, float time);
    public event EndTouchEvent OnEndTouch;
    bool isTouching;
    void Awake()
    {
        _instance = this;
        inputSystem = new InputSystemMain();
        inputSystem.Touch.TouchPress.started += ctx => StartTouch(ctx);
        inputSystem.Touch.TouchPosition.started += ctx => StayTouch(ctx);
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
/*        StartTouch(context);
*/        

        OnStartTouch?.Invoke(inputSystem.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        /*        print(inputSystem.Touch.TouchPosition.ReadValue<Vector2>());
        */
        isTouching = true;
        StartCoroutine(StayTouch());

        /*        print(inputSystem.Touch.TouchPosition.ReadValue<Vector2>());
        */
    }
    void EndTouch(InputAction.CallbackContext context)
    {
        //print("End");
        isTouching = false;
        OnEndTouch?.Invoke(inputSystem.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        print(inputSystem.Touch.TouchPosition.ReadValue<Vector2>());

    }
    private void StayTouch(InputAction.CallbackContext ctx)
    {
        OnStayTouch?.Invoke(inputSystem.Touch.TouchPosition.ReadValue<Vector2>());
        //print("stay");
    }
    IEnumerator StayTouch()
    {
        while (isTouching)
        {
            yield return null;
            if (isTouching)
            {
                OnStayTouch?.Invoke(inputSystem.Touch.TouchPosition.ReadValue<Vector2>());

            }

        }

    }
}
