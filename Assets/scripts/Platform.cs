using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool moveObject;
    [SerializeField] private bool isRight;

    private Vector3 moveRight;
    private Vector3 moveLeft;
    private Vector3 startPos;
    private float randomMove;

    void Start()
    {
        if (Controller.score < 100)
        {
            moveObject = false;
        }

        if (moveObject)
        {
            startPos = transform.position;
            randomMove = (float)Random.Range(0.5f, 4.0f);
        }

        if (Controller.score > 200)
        {
            moveObject = true;
            randomMove = (float)Random.Range(0.5f, 4.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, -1), PlatformGenerator.speed * Time.deltaTime);

        if (transform.position.z <= -5)
        {
            Destroy(gameObject);
        }

        if (moveObject && !isRight)
        {
            moveLeft = new Vector3(startPos.x + randomMove, transform.position.y, transform.position.z);
            moveRight = new Vector3(startPos.x - randomMove, transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(moveLeft, moveRight, Mathf.PingPong(Time.time, 1));
        }
        else if (moveObject && isRight)
        {
            moveRight = new Vector3(startPos.x - randomMove, transform.position.y, transform.position.z);
            moveLeft = new Vector3(startPos.x + randomMove, transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(moveRight, moveLeft, Mathf.PingPong(Time.time, 1));
        }
    }
}
