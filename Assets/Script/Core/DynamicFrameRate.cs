using UnityEngine;

public class DynamicFrameRate : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        if (SystemInfo.processorCount > 4) 
        {
            Application.targetFrameRate = 60; 
        }
        else
        {
            Application.targetFrameRate = 30; 
        }
    }
}
