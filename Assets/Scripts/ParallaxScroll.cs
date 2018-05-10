using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{

    [SerializeField]
    Transform cameraPos;
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float moveStrength = 2;
    private Vector2 moveForce;

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
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 15.5f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 15.5f));
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

    //public void FollowCamera()
    //{
    //    //Once you attach script to the object, drag camera into the cameraPos value.
    //    Vector3 newPos = cameraPos.position;
    //    newPos.z = transform.position.z/playerSpeed;
    //    //newPos.x = transform.position.x;
    //    newPos.y = transform.position.y;
    //    transform.position = newPos;
    //}

    //public void MovingThePlanets()
    //{
    //    planetRigidBody.velocity = new Vector2(planetRigidBody.velocity.x, planetRigidBody.velocity.y * 0.6f);
    //    moveForce = moveForce * -1;

    //    Vector3 theScale = transform.localScale;
    //    theScale.y *= -1;
    //    transform.localScale = theScale;
    //}

    //Have a reference to the background on the player - create a MoveBackground() which has a reference to how fast the player is moving, and then multiply it by half.
}
