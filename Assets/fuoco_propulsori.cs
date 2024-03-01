using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuoco_propulsori : MonoBehaviour
{
    Airship airship;
	
    // Start is called before the first frame update
    void Start()
    {
    	airship = GetComponent<Airship>();
    if (airship == null)
    {
        Debug.LogError("Airship component not found!");
    }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = airship.firePoint.transform.position;
    }
}
