using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

    [SerializeField]
    Transform cameraPos;
    [SerializeField]
    float PlayerSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FollowCamera();
    }

    public void FollowCamera()
    {
        //Once you attach script to the object, drag camera into the cameraPos value.
        Vector3 newPos = cameraPos.position;
        newPos.z = transform.position.z / (PlayerSpeed*5);
        //newPos.x = transform.position.x / (PlayerSpeed*5);
        //ewPos.y = transform.position.y / (PlayerSpeed*5);
        
        transform.position = newPos;
    }
}

