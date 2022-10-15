using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PointerController : MonoBehaviour
{
    Vector3 worldPosition, screenPosition;
    Plane plane = new Plane(Vector3.up, 0);
    Vector3 startPos, endPos;
    public GameObject pointer;
    InputManager inputManager;
    private void Awake()
    {
        inputManager = InputManager._instance;

    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += StartTouch;

    }
    
    private void StartTouch(Vector3 position, float time)
    {
        Vector3 mousePos = position;

        screenPosition = Camera.main.ScreenToWorldPoint(mousePos);
        screenPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
            Instantiate(pointer, worldPosition + (Vector3.up * 0.1f), Quaternion.Euler(new Vector3(90, 0, 0)));

        }

        AICharacterControl._instance.SetTarget(worldPosition);
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= StartTouch;

    }
}
