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
    bool fell = false;

    // Object renderer
    Renderer obRend;

    // Use this for initialization
    void Start () {

        mainCamera = Camera.main;
        obRend = gameObject.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y < mainCamera.transform.position.y - 10 &&
            !fell)
        {
            resolveDrop();
            Destroy(gameObject);
            fell = true;
        }

        // MakeObject Invisibe
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (obRend.enabled)
            {
                obRend.enabled = false;
            }
            else
            {
                obRend.enabled = true;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hit && collision.gameObject.tag == "Floor")
        {
            hit = true;
            fell = true;
            resolveCollision();
        }
        
    }
}
