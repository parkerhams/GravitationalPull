using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxStar : MonoBehaviour {
    [SerializeField]
    Transform cameraPos;
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float moveStrength = 2;
    private Vector2 moveForce;

    //[SerializeField]
    //Rigidbody2D planetRigidBody;

    // Use this for initialization
    //void Start()
    //{
    //    planetRigidBody = GetComponent<Rigidbody2D>();
    //    moveForce = new Vector2(0, moveStrength);
    //}

    // Update is called once per frame

    //************CANNOT TAKE FULL CREDIT FOR THIS SCRIPT - courtesy of philjhale on the Unity Forums**********
    void Update()
    {
        //FollowCamera();

        //planetRigidBody.AddForce(new Vector2(0, moveStrength));
    }

    public Vector3 pointB;

    IEnumerator Start()
    {
        var pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 60.5f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 60.5f));
        }
    }

    public IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

}
