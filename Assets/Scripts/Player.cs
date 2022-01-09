using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float distance, time, xPos;

    private float speed;

    public bool hasGameStarted;

    private void Start()
    {
        hasGameStarted = false;
        speed = distance / time;
    }

    private void FixedUpdate()
    {
        if (!hasGameStarted) return;

        Vector3 temp = new Vector3(Input.GetAxis("Horizontal") * speed * 2f, 0, speed) * Time.fixedDeltaTime;
        transform.Translate(temp);
        temp = transform.position;
        if (temp.x > xPos)
            temp.x = xPos;
        if (temp.x < -xPos)
            temp.x = -xPos;
        transform.position = temp;
    }

}
