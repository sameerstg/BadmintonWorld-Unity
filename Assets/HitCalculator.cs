using UnityEngine;

public class HitCalculator : MonoBehaviour
{


    InputManager inputManager;
    float startTime, endTime;
    Vector2 startPos, endPos;

    private void Awake()
    {
        inputManager = InputManager._instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += StartTouch;
        inputManager.OnEndTouch += EndTouch;

    }


    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartTouch;
        inputManager.OnEndTouch -= EndTouch;

    }
    private void StartTouch(Vector3 position, float time)
    {
        /*        print(position);
        */
        startPos = position;
        startTime = time;
    }

    private void EndTouch(Vector3 position, float time)
    {
        endPos = position;
        endTime = time;
        Calculate();

    }
    public float GetDeltaTime()
    {
        return endTime - startTime;
    }
    public Vector2 GetDeltaPos()
    {
        return endPos - startPos;
    }
    void Calculate()
    {
        /*        print($"delta time = {GetDeltaTime()}");
                print($"X Delta = {GetDeltaPos().x} , Y Delta {GetDeltaPos().y}");
        */
        ClearCalculation();
    }
    void ClearCalculation()
    {
        startPos = Vector2.zero;
        endPos = Vector2.zero;
        startTime = 0;
        endTime = 0;

    }


}
