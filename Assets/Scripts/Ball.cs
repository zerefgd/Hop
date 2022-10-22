using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Ball : MonoBehaviour
{

    [SerializeField] private float _distance, _time;

    [SerializeField] private AnimationCurve _jumpingCurve, _fallingCurve;

    [SerializeField]
    private GameObject explosionPrefab;



    private float timeElapsed;
    private bool isMovingTop;

    private Vector3 startPos, endPos;
    private Vector3 currentStartPos, currentEndPos, currentOffset;
 
    public bool hasGameStarted;

    private void Start()
    {
        hasGameStarted = false;

        timeElapsed = 0f;
        
        startPos = transform.localPosition;
        endPos = startPos + _distance * Vector3.up;        
        isMovingTop = true;

        currentStartPos = startPos;
        currentEndPos = endPos;
        currentOffset = currentEndPos - currentStartPos;
    }

    private void FixedUpdate()
    {

        if (!hasGameStarted) return;

        timeElapsed += Time.fixedDeltaTime;

        float offsetValue = timeElapsed / _time;
        offsetValue = isMovingTop ? _jumpingCurve.Evaluate(offsetValue) : _fallingCurve.Evaluate(offsetValue);

        transform.localPosition = currentStartPos +  offsetValue * currentOffset ;

        if(timeElapsed >= _time)
        {
            timeElapsed -= _time;

            currentStartPos = isMovingTop ? endPos : startPos;
            currentEndPos = isMovingTop ? startPos : endPos;

            if (!isMovingTop)
            {

                if (!Physics.Raycast(transform.position, Vector3.down, 4f))
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

                GameManager.instance.SpawnBlock();
            }

            isMovingTop = !isMovingTop;
            currentOffset = currentEndPos - currentStartPos;
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
