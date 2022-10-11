using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {


	public static BallLauncher _instance;
	public float minimumx, minimumy, maxmumx, maximumy;
	public static GameObject ball;
	Rigidbody ballrb;
	public Transform target;

	public float h = 1;
	public float gravity = -18;

	public bool debugPath;
	public bool boost;
    private void Awake()
    {
		_instance = this;
    }
    void Start() {
		ball = Instantiate(Resources.Load<GameObject>("Ball"));
		
		ballrb = ball.GetComponent<Rigidbody>();
		ballrb.useGravity = false;
		ball.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Launch ();
		}

		if (debugPath) {
			DrawPath ();
		}
	}

	void Launch() {
		Physics.gravity = Vector3.up * gravity;
		ballrb.useGravity = true;
		ballrb.velocity = CalculateLaunchData ().initialVelocity;
	}public void Launch(Vector3 position) {
		target.position = position;
		ball.SetActive(true);
		Physics.gravity = Vector3.up * gravity;
		ballrb.useGravity = true;
        if (boost)
        {
			LaunchData launchData = CalculateLaunchData();
			launchData.timeToTarget *= 10;
			ballrb.velocity = launchData.initialVelocity;
		}
        else
        {
			ballrb.velocity = CalculateLaunchData().initialVelocity;

		}
	}

	LaunchData CalculateLaunchData() {
		float displacementY = target.position.y - ballrb.position.y;
		Vector3 displacementXZ = new Vector3 (target.position.x - ballrb.position.x, 0, target.position.z - ballrb.position.z);
		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath() {
		LaunchData launchData = CalculateLaunchData ();
		Vector3 previousDrawPoint = ballrb.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++) {
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = ballrb.position + displacement;
			Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
			previousDrawPoint = drawPoint;
		}
	}

	struct LaunchData {
		public  Vector3 initialVelocity;
		public  float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
		
	}
}
	