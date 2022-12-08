using UnityEngine;

public class Welcome : MonoBehaviour
{
    private float i = 0;
    private int m = 1;

    private AudioSource audioSource;

    private void Start()
    {
        // get audio source
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("welcome");
    }

    // Update is called once per frame
    void Update()
    {
        i += 0.01f * m;

        if (i > 5)
        {
            audioSource.Play();
            i = 0;
            m = 0;
        }
    }
}
