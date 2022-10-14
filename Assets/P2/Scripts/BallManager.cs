using UnityEngine;

public class BallManager : MonoBehaviour
{


    public static BallManager _instance;
    public float minimumx, minimumz, maxmumx, maximumz;
    public static GameObject ball;
    Rigidbody ballrb;
    public Transform target;

    public float h = 1;
    public float gravity = -18;

    public bool debugPath;
    public bool boost;
    public Vector3 lastLaunchPosition;
    public float lastLaunchTime;
    TrailRenderer trail;
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
        trail.Clear();
        target.transform.position = new Vector3(Random.Range(minimumx, maxmumx), 0, Random.Range(minimumz, maximumz));
        print("shoot");

        ball.SetActive(true);
        Physics.gravity = Vector3.up * gravity;
        ballrb.useGravity = true;
        ballrb.velocity = CalculateLaunchData().initialVelocity;
    }
    public void Launch(Vector3 position)
    {
        trail.Clear();

        target.position = position;
        ball.SetActive(true);
        Physics.gravity = Vector3.up * gravity;
        ballrb.useGravity = true;
        print(CalculateLaunchData().initialVelocity);
        ballrb.velocity = CalculateLaunchData().initialVelocity;

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
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ballrb.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ballrb.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
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
}
