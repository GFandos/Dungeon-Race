using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    private float y;
    private float x;
    private bool mouseMiddleClicked;
    private Transform target;
    public float rotationSpeed;
    public float distanceToPlayer;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;
        mouseMiddleClicked = false;
        y = transform.eulerAngles.y;
        x = transform.eulerAngles.x;
        target = player.gameObject.transform;

    }

    // Update is called once per frame
    void Update () {


        if (Input.GetMouseButtonDown(2))
        {
            mouseMiddleClicked = true;
        }
        if (Input.GetMouseButtonUp(2))
        {
            mouseMiddleClicked = false;
        }

    }

    private void LateUpdate()
    {
        if (target && mouseMiddleClicked == true)
        {

            Vector3 rotationVector = new Vector3(0, 0, -distanceToPlayer);

            y += Input.GetAxis("Mouse X") * rotationSpeed * distanceToPlayer * 0.02f;
            x += Input.GetAxis("Mouse Y") * rotationSpeed * distanceToPlayer * 0.02f;
            var rotation = Quaternion.Euler(x, y, 0);
            var position = rotation * rotationVector + target.position;
            transform.rotation = rotation;
            transform.position = position;
            offset = transform.position - player.transform.position;

        } else {
            transform.position = player.transform.position + offset;
            
        }


    }
}
