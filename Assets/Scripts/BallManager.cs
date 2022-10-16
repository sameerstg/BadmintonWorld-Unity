using UnityEngine;
using System.Collections;
public class BallManager : MonoBehaviour
{


    public static BallManager _instance;
    public float minimumx, minimumz, maxmumx, maximumz;
    public static GameObject ball;
    Rigidbody ballrb;
    public Transform target;

    public float height = 1;
    public float gravity = -18;

    public bool debugPath;
    public bool boost;
    public Vector3 lastLaunchPosition;
    public float lastLaunchTime;
    TrailRenderer trail;
    public LineRenderer lineRendererForPath;

    public enum BallSpeed
    {
        slowest, slow, slowMedium, medium, mediumFast, fast, fastest
    }
    public BallSpeed ballSpeed;
    private void Awake()
    {
        _instance = this;
        ball = Instantiate(Resources.Load<GameObject>("Ball"));
        ballrb = ball.GetComponent<Rigidbody>();
        ballrb.useGravity = false;
        ball.SetActive(false);

        trail = ball.GetComponent<TrailRenderer>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }

    public void Launch()
    {
        SetBallSpeed();
        trail.Clear();
        target.transform.position = new Vector3(Random.Range(minimumx, maxmumx), 0.1f, Random.Range(minimumz, maximumz));
        /*        print(target.position);
        */
        ball.SetActive(true);
        Physics.gravity = Vector3.up * gravity;
        ballrb.useGravity = true;

        ballrb.velocity = CalculateLaunchData().initialVelocity;
        DrawPath();

    }
    public void Launch(Vector3 position)
    {
        trail.Clear();

        target.position = position+Vector3.up*0.1f;
        
        ball.SetActive(true);
        Physics.gravity = Vector3.up * gravity;
        ballrb.useGravity = true;
/*        print(CalculateLaunchData().initialVelocity);
*/        ballrb.velocity = CalculateLaunchData().initialVelocity;

    }
    public void Launch(Vector3 position, float height, float gravity)
    {
        trail.Clear();

        target.position = position;
        ball.SetActive(true);
        Physics.gravity = Vector3.up * gravity;
        ballrb.useGravity = true;
        ballrb.velocity = CalculateLaunchData(height, gravity).initialVelocity;

    }
    public void Launch(Vector2 minimumXPosAndYPos, Vector2 maximumXPosAndYPos)
    {
        trail.Clear();

        target.transform.position = new Vector3(Random.Range(minimumXPosAndYPos.x, maximumXPosAndYPos.x), 0, Random.Range(minimumXPosAndYPos.y, maximumXPosAndYPos.y));
        ball.SetActive(true);
        Physics.gravity = Vector3.up * gravity;
        ballrb.useGravity = true;
        print(CalculateLaunchData().initialVelocity);
        ballrb.velocity = CalculateLaunchData().initialVelocity;

    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - ballrb.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ballrb.position.x, 0, target.position.z - ballrb.position.z);
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
    LaunchData CalculateLaunchData(float height, float gravity)
    {

        float displacementY = target.position.y - ballrb.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ballrb.position.x, 0, target.position.z - ballrb.position.z);
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        target.gameObject.SetActive(true);

        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ballrb.position;
        int resolution = 50;
        lineRendererForPath.positionCount = 0;
        Vector3[] positions = new Vector3[resolution+1];
        positions[0] = previousDrawPoint;
        lineRendererForPath.positionCount = resolution;

        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ballrb.position + displacement;
            positions[i] = drawPoint;

            
            previousDrawPoint = drawPoint;
        }
        lineRendererForPath.SetPositions(positions);
        StartCoroutine(ResetPath());
    }
    void SetBallSpeed()
    {
        int level = (int)ballSpeed;
        switch (level)
        {
            case 0:
                gravity = -10;
                break;
            case 1:
                gravity = -18;
                break;
            case 2:
                gravity = -100;
                break;
            case 3:
                gravity = -200;
                break;
            case 4:
                gravity = -300;
                break;
            case 5:
                gravity = -400;
                break;
            case 6:
                gravity = -500;
                break;
            default:
                gravity = -18;
                break;
        }
    }
    
    struct LaunchData
    {
        public Vector3 initialVelocity;
        public float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
    IEnumerator ResetPath()
    {
        yield return new WaitForSeconds(1f);
        lineRendererForPath.positionCount = 0;
        target.gameObject.SetActive(false);

    }
}
