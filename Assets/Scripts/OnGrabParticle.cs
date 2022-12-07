using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnGrabParticle : MonoBehaviour
{
    void Update()
    {
        // check if object is grabbed
        if (GetComponent<XRGrabInteractable>()?.isSelected ?? false)
        {
            // remove the random motion script
            Destroy(GetComponent<RandomMotion>());
        }
    }
}