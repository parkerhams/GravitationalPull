using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBackgrounds : MonoBehaviour {

    [SerializeField]
    Transform cameraPos;
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float moveStrength = 2;
    [SerializeField]
    

    Rigidbody objectRigidbody;

    //************CANNOT TAKE FULL CREDIT FOR THIS SCRIPT - courtesy of philjhale on the Unity Forums**********
    void Update()
    {
        //FollowCamera();


        objectRigidbody.AddForce(new Vector3(0, moveStrength));
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
}
