using System;
using System.Collections.Generic;
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
    public float xRange, zRange,minZRange;
    float angle;
    public static Vector2 lastDeltaPos;
    public static float lastDeltaTime;
    BallManager ballManager;



    
    enum Height{
        low = 1,
        medium = 5,
        high = 10

    }enum Speed{
        slow = 35,
        medium = 80,
        fast = 200

    }
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

        /*Calculate();*/
    }

    private void EndTouch(Vector3 position, float time)
    {


        endPos = position;
        endTime = time;

        if (Vector3.Distance(position,startPos)>40)
        {
            Calculate();
            SetTartgetPosition(endPosR - startPosR);
        }

        ClearCalculation();
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
        angle = endPosR.x - startPos.x;
        lastDeltaPos = GetDeltaPos();
        lastDeltaTime = GetDeltaTime();
        DebugCalculation();
    }
    void DebugCalculation()
    {
/*        print($"delta time = {GetDeltaTime()}");
        print($"X Delta = {GetDeltaPos().x} , Y Delta {GetDeltaPos().y}");
*//*        print($"Angle = {Vector2.Angle(GetDeltaPos(), Vector2.up)}");
*/

    }
    void ClearCalculation()
    {
        lastDeltaPos = Vector3.zero;
        lastDeltaTime = 0;
        startPos = Vector2.zero;
        startPosR = Vector2.zero;
        endPos = Vector2.zero;
        endPosR = Vector2.zero;
        startTime = 0;
        endTime = 0;

    }

    void SetTartgetPosition(Vector3 position)
    {
        /*        obj.transform.position = new Vector3(position.x,1,position.z);
        */
        int height = 2,speed;
        string color;
        /*if (lastDeltaPos.y <35)
        {
            height = (int)Height.low;
        }
        else if (lastDeltaPos.y < 60)
        {
            height = (int)Height.medium;

        }
        else
        {
            height = (int)Height.high;

        }*/if (lastDeltaTime <0.08)
        {
            speed = (int)Speed.fast;
            color ="f";
        }
        else if (lastDeltaTime < 0.3)
        {
            speed = (int)Speed.medium;
            color = "m";

        }
        else
        {
            speed = (int)Speed.slow;
            color = "s";

        }
        float totalZ = -((zRange - minZRange)/100*lastDeltaPos.y);
        totalZ += Math.Abs(minZRange);
/*        print(totalZ);
        print($"height {height} , speed {speed}");
*/        ballManager.Launch(new Vector3(position.x, obj.transform.position.y, totalZ), height, -speed,color);


    }
    void SetTartgetPosition( float x)
    {
        /*        obj.transform.position = new Vector3(x,defaultPos.y,defaultPos.z);
        */
        ballManager.Launch(new Vector3(x, obj.transform.position.y, obj.transform.position.z));
    }
}
