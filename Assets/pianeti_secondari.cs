using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pianeti_secondari : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.gameover) {
	    if (transform.position.x >= -13.5)
		transform.position = new Vector2(transform.position.x - 0.009f * Time.deltaTime, transform.position.y);
	    else
		transform.position = new Vector2(10f, transform.position.y);
	}
    }
}
