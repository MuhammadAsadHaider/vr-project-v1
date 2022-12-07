using UnityEngine;

public class DetailPanelGenerate : MonoBehaviour
{
    public GameObject Details;

    public void GenerateDetails()
    {
        GameObject detail = Instantiate(Details, transform.position + new Vector3(1.4f, 0, -4f), Quaternion.identity);
        detail.transform.SetParent(transform);
    }
}
