using UnityEngine;
using System.Collections;

public class ShellController : MonoBehaviour
{
    public GameObject barrel;
    public float shellSpeed = 500f;
    public float maxShellDistance = 500f;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = barrel.transform.rotation;
        transform.position = barrel.transform.TransformPoint(barrel.transform.localPosition + new Vector3(0, 1, 0));
        rb.AddForce(barrel.transform.up * shellSpeed, ForceMode.Impulse);
        //Debug.Log(barrel.transform.up * shellSpeed);
    }

    // Update is called once per frame
    void Update()
    {
       if (Vector3.Magnitude(transform.position - barrel.transform.position) > maxShellDistance)
        {
            Destroy(gameObject);
        }
    }

    void onOnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}