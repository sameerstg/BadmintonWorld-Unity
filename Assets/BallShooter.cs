using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Vector3 shootingPostition;



    public void ShootBall()
    {
        BallLauncher.ball.transform.position = shootingPostition;
/*        BallLauncher._instance()
*/    }
}
