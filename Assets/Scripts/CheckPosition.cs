using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    public GameObject AI;
    
    private bool played = false;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = AI.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if object is in main camera view
        if (!played && transform.position.x > 4 && transform.position.z > -3 && transform.position.z < 3)
        {
            audioSource.clip = Resources.Load<AudioClip>($"instructions");
            audioSource.Play();
            played = true;
        }
    }
}