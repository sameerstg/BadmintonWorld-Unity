using System;
using UnityEngine;

public class HitCalculator : MonoBehaviour
{


    InputManager inputManager;
    float startTime, endTime;
    Vector2 startPos, endPos,startPosR,endPosR;
    public GameObject obj;
    Vector3 defaultPos;
    Plane plane = new Plane(Vector3.up, 0);
    Vector3 screenPosition, worldPosition;



    BallManager ballManager;
    private void Awake()
    {
        inputManager = InputManager._instance;
        defaultPos = obj.transform.position;
    }
    private void Start()
    {
        ballManager = BallManager._instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += StartTouch;
        inputManager.OnStayTouch += StayTouch;
        inputManager.OnEndTouch += EndTouch;

    }


    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartTouch;
        inputManager.OnStayTouch -= StayTouch;

        inputManager.OnEndTouch -= EndTouch;

    }


    private void StartTouch(Vector3 position, float time)
    {
        /*        print(position);
        */

        Vector3 mousePos = position;

         screenPosition = Camera.main.ScreenToWorldPoint(mousePos);
        screenPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);

        }

        startPosR = worldPosition;
        startPos = position;
        startTime = time;
    }
    private void StayTouch(Vector3 position)
    {

        Vector3 mousePos = position;

        screenPosition = Camera.main.ScreenToWorldPoint(mousePos);
        screenPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);

        }

        endPosR = worldPosition;


        //obj.transform.position = Camera.main.ViewportToWorldPoint( new Vector3(defaultPos.x+(position.x/Screen.width), defaultPos.y, defaultPos.z));
        //obj.transform.position = new Vector3(obj.transform.position.x,defaultPos.y,defaultPos.z);
    }

    private void EndTouch(Vector3 position, float time)
    {


        endPos = position;
        endTime = time;

        if (GetDeltaTime() > 0.1)
        {

            SetTartgetPosition(endPosR.x - startPosR.x);
        }

        Calculate();

        obj.transform.position = defaultPos;

    }
    public float GetDeltaTime()
    {
        return endTime - startTime;
    }
    public Vector2 GetDeltaPos()
    {
        Vector2 tendPos = new Vector2(endPos.x / Screen.width * 100, endPos.y / Screen.height * 100);
        Vector2 tstartPos = new Vector2(startPos.x / Screen.width * 100, startPos.y / Screen.height * 100);
        return tendPos - tstartPos;
    }
    void Calculate()
    {
/*        print($"delta time = {GetDeltaTime()}");
        print($"X Delta = {GetDeltaPos().x} , Y Delta {GetDeltaPos().y}");
        print($"Angle = {Vector2.Angle(GetDeltaPos(),Vector2.up)}");
*/
        ClearCalculation();
    }
    void ClearCalculation()
    {
        startPos = Vector2.zero;
        startPosR = Vector2.zero;
        endPos = Vector2.zero;
        endPosR = Vector2.zero;
        startTime = 0;
        endTime = 0;

    }

    void SetTartgetPosition(Vector3 position)
    {
        obj.transform.position = new Vector3(position.x,1,position.z);
    }void SetTartgetPosition( float x)
    {
        /*        obj.transform.position = new Vector3(x,defaultPos.y,defaultPos.z);
        */
        ballManager.Launch(new Vector3(x, obj.transform.position.y, obj.transform.position.z));
    }
}
