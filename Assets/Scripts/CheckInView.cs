using UnityEngine;

public class CheckInView : MonoBehaviour
{
    private bool viewed = false;
    // Update is called once per frame
    void Update()
    {
        // check if object is in main camera view
        if (!viewed && GetComponent<Renderer>().isVisible)
        {
            Debug.Log("Object is in view");
            viewed = true;
        }
    }
}
