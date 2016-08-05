using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {
    public GameObject targetTemplate;
    public float gameAreaX = 50f;
    public float gameAreaY = 10f;
    public float gameAreaZ = 50f;
    public int targetNumber = 10;

	// Use this for initialization
	void Start () {
        float targetX;
        float targetY;
        float targetZ;
        for (int i = 0; i < targetNumber; i++)
        {
            targetX = Random.Range(-gameAreaX, gameAreaX);
            targetY = Random.Range(0, gameAreaY);
            targetZ = Random.Range(-gameAreaZ, gameAreaZ);
            GameObject target = (GameObject)Instantiate(targetTemplate, transform);
            target.transform.position = new Vector3(targetX, targetY, targetZ);
            target.transform.rotation = Quaternion.LookRotation(-target.transform.position, Vector3.up);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
