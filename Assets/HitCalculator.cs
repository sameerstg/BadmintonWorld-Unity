using System;
using UnityEngine;

public class HitCalculator : MonoBehaviour
{


    InputManager inputManager;
    float startTime, endTime;
    Vector2 startPos, endPos;
    public GameObject obj;
    Vector3 defaultPos;
    private void Awake()
    {
        inputManager = InputManager._instance;
        defaultPos = obj.transform.position;
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
        startPos = position;
        startTime = time;
    }
    private void StayTouch(Vector3 position)
    {


        SetTartgetPosition(Cam.ViewportToWorldPoint(new Vector3(mousePosition.x / Screen.width,8,8));

        //obj.transform.position = Camera.main.ViewportToWorldPoint( new Vector3(defaultPos.x+(position.x/Screen.width), defaultPos.y, defaultPos.z));
        //obj.transform.position = new Vector3(obj.transform.position.x,defaultPos.y,defaultPos.z);
    }

    private void EndTouch(Vector3 position, float time)
    {
        endPos = position;
        endTime = time;
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
        print($"delta time = {GetDeltaTime()}");
        print($"X Delta = {GetDeltaPos().x} , Y Delta {GetDeltaPos().y}");
        print($"Angle = {Vector2.Angle(GetDeltaPos(),Vector2.up)}");

        ClearCalculation();
    }
    void ClearCalculation()
    {
        startPos = Vector2.zero;
        endPos = Vector2.zero;
        startTime = 0;
        endTime = 0;

    }

    void SetTartgetPosition(Vector3 position)
    {
        obj.transform.position = new Vector3(position.x,1,position.z);
    }void SetTartgetPosition( float x)
    {
        obj.transform.position = new Vector3(x,defaultPos.y,defaultPos.z);
    }
}
