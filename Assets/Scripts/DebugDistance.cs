using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugDistance : MonoBehaviour
{
    Vector3 startPosition;
    public GameObject pointer;
    bool isCollided = false;
    Lerp lerp ; 
    
    void Start()
    {
        startPosition = transform.position;
        lerp = Lerp._instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Finish") && !isCollided)
        {
            isCollided = true;
            /*            print(collision.gameObject.name);
            *//*            Instantiate(pointer, collision.transform.position + (Vector3.up * 0.1f), Quaternion.Euler(new Vector3(90, 0, 0)));
               *            
               *            
            */
            if (Vector3.Distance(startPosition, collision.GetContact(0).point)>3)
            {
                ShotDetail sd = new ShotDetail(lerp.force, lerp.height, Vector3.Distance(startPosition, collision.GetContact(0).point));
                lerp.shotDetails.Add(sd);
                sd.DebugResult();
                string jsonString = JsonConvert.SerializeObject((lerp.shotDetails));
                System.IO.File.WriteAllText(Application.persistentDataPath + "/Shotdetails.json", jsonString);
            }
             
            

        }
    }
}
public class ShotDetail
{
    public float distance;
    public float force;
    public float height;

    public ShotDetail(float force,float height,float distance)
    {
        this.force = force;
        this.height = height;
        this.distance = distance;
    }

   

    public void DebugResult()
    {
        Debug.Log($"Force = {force} , Height = {height} and distance coverage = " + distance);

    }
}