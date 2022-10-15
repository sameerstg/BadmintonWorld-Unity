using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public bool practiceMode;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {

    }

    void Update()
    {

    }

}
