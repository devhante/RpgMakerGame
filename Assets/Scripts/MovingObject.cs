using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Vector3 vector;

    public float speed;
    public float runSpeed;
    private float applyRunSpeed;

    public int walkCount;
    private int currentWalkCount;

    private bool isRunning = false;
    private bool canMove = true;

    private IEnumerator MoveCoroutine()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applyRunSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            applyRunSpeed = 0;
            isRunning = false;
        }

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        while (currentWalkCount < walkCount)
        {
            if (vector.x != 0)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
            }

            if (isRunning)
            {
                currentWalkCount++;
            }
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }

        currentWalkCount = 0;
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
