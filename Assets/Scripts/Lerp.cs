using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Lerp : MonoBehaviour
{
    public static Lerp _instance;
    public GameObject startPostion, endPostion;

    public float force, height;
    float tempForce, tempHeight;
    Vector3 angle;
    public GameObject sphere;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private Scene currentScne;
    [SerializeField] private Transform _obstaclesParent;
    private bool isCollided;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();


    public List<ShotDetail> shotDetails = new List<ShotDetail>();
    private void Awake()
    {
        _instance = this;
    }
    IEnumerator DebugAll()
    {
        for (float i = 8; i <= 11; i += 0.2f)
        {
            for (float j = 1; j <= 1.5; j += 0.1f)
            {
                yield return new WaitForSeconds(.5f);
                force = i;
                height = j;
                angle = (startPostion.transform.position - endPostion.transform.position);
                SimulateTrajectory(sphere, startPostion.transform.position, (-angle.normalized));

            }
        }
    }
    private void Start()
    {

        currentScne = SceneManager.GetActiveScene();
        CreatePhysicsScene();
        StartCoroutine(DebugAll());

    }

    private void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        foreach (Transform obj in _obstaclesParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            /*            ghostObj.GetComponent<Renderer>().enabled = false;
            */
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);

        }
    }
    public void DebugTrajectory()
    {
        tempForce = force;
        tempHeight = height;
        OnlySimulateTrajectory(sphere, startPostion.transform.position, (-angle.normalized));

    }
    private void FixedUpdate()
    {


    }
    private void Update()
    {
        /* if (force != tempForce || tempHeight != height)
         {
             DebugTrajectory();

         }*/

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            angle = (startPostion.transform.position - endPostion.transform.position);
            SimulateTrajectory(sphere, startPostion.transform.position, (-angle.normalized));
        }
    }
    /*private void Update()
    {
        
    }*/

    public void Init(GameObject obg, Vector3 velocity)
    {


        obg.GetComponent<Rigidbody>().velocity = (velocity + Vector3.up * height) * force;
    }

    public void SimulateTrajectory(GameObject ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);
        var ghostObj2 = Instantiate(ballPrefab, pos, Quaternion.identity);
        ghostObj.name = "1";
        ghostObj2.name = "2";
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        Init(ghostObj, velocity);
        Init(ghostObj2, velocity);
        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++)
        {

            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);

        }

        Destroy(ghostObj);
    }
    public void OnlySimulateTrajectory(GameObject ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        Init(ghostObj, velocity);
        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++)
        {

            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);



        }

        Destroy(ghostObj);
    }
}
