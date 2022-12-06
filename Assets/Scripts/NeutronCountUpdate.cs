using UnityEngine;

public class NeutronCountUpdate : MonoBehaviour
{
    public CheckInside CheckInside;

    // get text mesh pro text component
    private TMPro.TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        textMesh.text = CheckInside.Neutrons.ToString();
    }
}
