using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float distance, time;

    [SerializeField]
    private GameObject explosionPrefab;

    private float speed, startSpeed, acceleration;

    public bool hasGameStarted;

    private void Start()
    {
        hasGameStarted = false;

        startSpeed = 2 * distance / time;
        acceleration = -0.995f * startSpeed / time;
        speed = startSpeed;
    }

    private void FixedUpdate()
    {

        if (!hasGameStarted) return;

        speed += acceleration * Time.fixedDeltaTime;
        Vector3 temp = new Vector3(0, speed * Time.fixedDeltaTime, 0);
        transform.localPosition += temp;
        temp = transform.localPosition;

        if(temp.y < 0f)
        {

            if(!Physics.Raycast(transform.position,Vector3.down,4f))
            {
                GameManager.instance.GameOver();
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(transform.parent.gameObject);
            }
            else
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.down, out hit, 4f);
                GameObject tempBlock = hit.transform.gameObject;
                GameManager.instance.UpdateScore(tempBlock);
            }

            speed = startSpeed;
            GameManager.instance.SpawnBlock();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
            GameManager.instance.UpdateDiamond();
            GameObject temp = Instantiate(explosionPrefab);
            temp.transform.position = other.gameObject.transform.position;
            temp.GetComponent<Explosion>().SetAsDiamond();
        }
    }
}
