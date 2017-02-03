using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {

    float speed = 0.02f;

	//// Use this for initialization
	//void Start () {
		
	//}

    private void Update()
    {
        transform.position += new Vector3(transform.position.x + (speed * Time.deltaTime), 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Wall")
        {
            transform.position = new Vector3();
            speed = -speed;
        }
    }
}
