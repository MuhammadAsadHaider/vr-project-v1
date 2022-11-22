// make particle orbit in ellipse around nucleus (proton and neutron)


// Language: csharp
// Path: Assets\Scripts\Orbit.cs

using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float a = 2f;
    public float b = 1f;
    public float speed = 1f;
    public float angle = 0f;
    public float angleOffset = 0f;

    private Vector3 _center;
    private Vector3 _axis;

    void Start()
    {
        _center = transform.parent.position;
        _axis = transform.parent.forward;
    }

    void Update()
    {
        angle += speed * Time.deltaTime;
        var x = Mathf.Cos(angle + angleOffset) * a;
        var z = Mathf.Sin(angle + angleOffset) * b;
        transform.localPosition = new Vector3(x, 0, z);
        transform.RotateAround(_center, _axis, speed * Time.deltaTime);

        // update center and axis
        _center = transform.parent.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}