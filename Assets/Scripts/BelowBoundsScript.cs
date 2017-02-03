using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelowBoundsScript : MonoBehaviour {

    Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;

    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y < mainCamera.transform.position.y - 6)
        {
            Destroy(gameObject);
        }
		
	}
}
