using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Transform shootingPostition;
    public GameObject ball;
    private void Start()
    {
        Invoke("ShootBall", 2.0f);
        ball = BallManager.ball;
    }
    private void Update()
    {
        if (!ball.activeInHierarchy || Input.GetKeyDown(KeyCode.P))
        {
            ShootBall();
            
        }
    }
    public void ShootBall()
    {
        
        
        ball.transform.position = shootingPostition.position+Vector3.back *2;
        BallManager._instance.Launch();
    }
}
