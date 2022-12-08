using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    private bool viewed = false;
    // Update is called once per frame
    void Update()
    {
        // check if object is in main camera view
        if (transform.position.x > 4 && transform.position.z > -3 && transform.position.z < 3)
        {
            Debug.Log("Hello!");
        }
    }
}