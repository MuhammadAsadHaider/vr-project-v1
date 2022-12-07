using UnityEngine;

public class DetailPanelGenerate : MonoBehaviour
{
    public GameObject Details;

    public void GenerateDetails(string test)
    {
        GameObject detail = Instantiate(Details, transform);
        detail.transform.SetParent(transform);
    }
}
