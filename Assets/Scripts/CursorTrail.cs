
using System;
using UnityEngine;

public class CursorTrail : MonoBehaviour
{
    [SerializeField] LineRenderer trailPrefab = null;
    [SerializeField] Camera Cam;
    [SerializeField] float clearSpeed = 1;
    [SerializeField] float distanceFromCamera = 1;

    private LineRenderer currentTrail;
    private Vector3[] points = new Vector3[2];




    InputManager inputManager;
    private void Awake()
    {
        inputManager = InputManager._instance;
    }
    private void OnEnable()
    {

        inputManager.OnStartTouch += StartTouch;
        inputManager.OnStayTouch += StayTouch;
        inputManager.OnEndTouch += EndTouch;
    }
    void OnDisable()
    {
        inputManager.OnStartTouch -= StartTouch;
        inputManager.OnStayTouch -= StayTouch;
        inputManager.OnEndTouch -= EndTouch;


    }
    private void StartTouch(Vector3 position, float time)
    {
        CreateCurrentTrail();
        AddPoint(0, new Vector3(position.x, position.y, 0));
        AddPoint(1, new Vector3(position.x, position.y, 0));
        UpdateTrailPoints();


    }


    private void StayTouch(Vector3 position)
    {
            AddPoint(1, new Vector3(position.x, position.y, 0));
            UpdateTrailPoints();



    }
    private void EndTouch(Vector3 position, float time)
    {

        DestroyCurrentTrail();


    }


















    private void DestroyCurrentTrail()
    {
        if (currentTrail != null)
        {
            Destroy(currentTrail.gameObject);
            currentTrail = null;
            points = null;
        }

    }

    private void CreateCurrentTrail()
    {
        points = new Vector3[2];
        currentTrail = Instantiate(trailPrefab);
        currentTrail.transform.SetParent(transform, true);
        currentTrail.positionCount = 0;
    }

    private void AddPoint(int index, Vector2 postion)
    {

            points[index] = (Cam.ViewportToWorldPoint(new Vector3(postion.x / Screen.width, postion.y / Screen.height, distanceFromCamera)));

    }

    private void UpdateTrailPoints()
    {//
        if (currentTrail != null && points.Length > 1)
        {
/*            if (Math.Abs(points[0].x - points[1].x)>1)
            {
*/                currentTrail.positionCount = points.Length;
                currentTrail.SetPositions(points);

            
        }
        else
        {
            DestroyCurrentTrail();
        }
    }

    private void ClearTrailPoints()
    {
        float clearDistance = Time.deltaTime * clearSpeed;
        while (points.Length > 1 && clearDistance > 0)
        {
            float distance = (points[1] - points[0]).magnitude;
            if (clearDistance > distance)
            {
                points[0] = new Vector3();
            }
            else
            {
                points[0] = Vector3.Lerp(points[0], points[1], clearDistance / distance);

            }
            clearDistance -= distance;
        }
    }






}



