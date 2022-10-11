using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Transform shootingPostition;
    public GameObject ball;
    private void Start()
    {
        Invoke("ShootBall",2.0f);
        ball = BallLauncher.ball;
    }
    private void Update()
    {
        if (!ball.activeInHierarchy)
        {
            ShootBall();
            
        }
    }
    public void ShootBall()
    {
        print("shoot");
        
        ball.transform.position = shootingPostition.position+Vector3.back *2;
        BallLauncher._instance.Launch();
    }
}
