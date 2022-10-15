using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public List<GameObject> allGrounds = new List<GameObject>();
    public List<GameObject> mySideGrounds = new List<GameObject>();
    public List<GameObject> opponentSideGrounds = new List<GameObject>();


    private void Start()
    {
        allGrounds = GameObject.FindGameObjectsWithTag("Ground").ToList();
        foreach (var item in allGrounds)
        {
            if (item.name.StartsWith('m'))
            {
                mySideGrounds.Add(item);
            }
            else
            {
                opponentSideGrounds.Add(item);
            }
        }
    }

}
