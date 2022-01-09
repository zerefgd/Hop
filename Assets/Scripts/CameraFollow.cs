using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private Transform player;
    private Vector3 offset;
    public bool canFollow;

    // Start is called before the first frame update
    void Start()
    {
        canFollow = true;
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        if (!canFollow) return;

        transform.position = new Vector3(transform.position.x,transform.position.y, player.position.z + offset.z);
    }
}
