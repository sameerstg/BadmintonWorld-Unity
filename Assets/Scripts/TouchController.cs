using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class TouchController : MonoBehaviour
{
    public static TouchController _instance;
    Vector3 worldPosition, screenPosition;
    Plane plane = new Plane(Vector3.up, 0);
    Vector3 startPos, endPos;
    public GameObject pointer;
    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        screenPosition = Camera.main.ScreenToWorldPoint(mousePos);
        screenPosition.z = Camera.main.nearClipPlane;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                worldPosition = ray.GetPoint(distance);
                Instantiate(pointer, worldPosition + (Vector3.up * 0.1f), Quaternion.Euler(new Vector3(90, 0, 0)));

            }

            AICharacterControl._instance.SetTarget(worldPosition);
            if (Input.GetKey(KeyCode.S))
            {

                BallManager._instance.Launch(worldPosition);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            BallManager.ball.transform.position = AICharacterControl._instance.transform.position;
            BallManager.ball.transform.position += new Vector3(2, 4, 0);
            BallManager.ball.SetActive(false);
        }

    }
    private void OnMouseDrag()
    {
        print("drah");

        /*        trail.transform.position = screenPosition;
        */
    }
}
