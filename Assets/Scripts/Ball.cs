using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public static bool isBallOut;
    private void Update()
    {
        if (gameObject.transform.position.y < -2)
        {
            gameObject.SetActive(false);
        }
    }


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
