using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public static bool isBallOut;



    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ground"))
        {
            isBallOut = true;
            StartCoroutine(SetBallOff());
        }
    }
    IEnumerator SetBallOff()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
