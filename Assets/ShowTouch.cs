using UnityEngine;
[DefaultExecutionOrder(-1)]
public class ShowTouch : MonoBehaviour
{
    InputManager inputManager;
    public GameObject trailObj;
    LineRenderer trail;
    public Camera trailCamera;
    Vector3[] trailPositions = new Vector3[2];
    private void Awake()
    {
        inputManager = InputManager._instance;

        trailCamera = GetComponent<Camera>();
        /*        trailObj = Instantiate(trailObj);
        */
        trail = trailObj.GetComponent<LineRenderer>();
    }
    private void OnEnable()
    {

        inputManager.OnStartTouch += OnStartPosition;
        inputManager.OnStayTouch += OnStayPosition;
        inputManager.OnEndTouch += OnEndPosition;
    }



    private void OnDisable()
    {
        inputManager.OnStartTouch -= OnStartPosition;
        inputManager.OnStayTouch -= OnStayPosition;
        inputManager.OnEndTouch -= OnEndPosition;

    }
    private void OnStartPosition(Vector3 position, float time)
    {
        print("start");
        print($" event Position = {position} and mouse pos = {Input.mousePosition}");
        /*        trailPositions[0] =  trailCamera.ScreenToWorldPoint( new Vector3(position.x/Screen.width,position.y/Screen.height,trailCamera.nearClipPlane));
        */
        trailPositions[0] = new Vector3(position.x / Screen.width, position.y / Screen.height, 0);

        trailPositions[0] = trailCamera.ScreenToWorldPoint(trailPositions[0]);
        print(Screen.width);
        print(Screen.height);

        /*        trailObj = Instantiate(trailObj);
                trail = trailObj.GetComponent<LineRenderer>();
                trail.SetPosition(0, new Vector3(position.x, position.y, trailCamera.nearClipPlane + 2));
        */
    }

    public void OnStayPosition(Vector3 position)
    {
        print($" event Position = {position} and mouse pos = {Input.mousePosition}");

        trailPositions[1] = new Vector3(position.x / Screen.width, position.y / Screen.height, 0);
        trailPositions[0].z = trailCamera.nearClipPlane;
        trail.SetPositions(trailPositions);
        /*        trail.SetPosition(1,new Vector3(position.x, position.y, trailCamera.nearClipPlane+2));
        */
    }
    private void OnEndPosition(Vector3 position, float time)
    {
        print("End");
        //trailPositions[1] = trailCamera.ScreenToWorldPoint(new Vector3(position.x, position.y, trailCamera.nearClipPlane));
        trailPositions[1] = new Vector3(position.x / Screen.width, position.y / Screen.height, 0);
        trailPositions[0].z = trailCamera.nearClipPlane;
        trail.SetPositions(trailPositions);

        /*        Destroy(trail);
        */
    }

}
