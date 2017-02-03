using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelowBoundsScript : MonoBehaviour {

    Camera mainCamera;

    // Event creation
    public delegate void onCollision();
    public delegate void onDrop();
    public static event onCollision resolveCollision;
    public static event onDrop resolveDrop;

    // check if this item has alread hit the floor
    bool hit = false;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;

    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y < mainCamera.transform.position.y - 6)
        {
            Destroy(gameObject);
            resolveDrop();
        }
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (!hit)
        {
            resolveCollision();
            hit = true;
        }
        
    }
}
