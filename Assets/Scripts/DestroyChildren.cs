using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildren : MonoBehaviour
{
    // Update is called once per frame
    public void DestroyChildObjects()
    {
        // destroy all child objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
