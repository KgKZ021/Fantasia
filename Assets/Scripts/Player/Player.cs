using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Player Controller
/// Controls all the actions player will perform
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed=7f;
    [SerializeField] private GameInput gameInput;

    private bool isRunning;

    private void Update()
    {
        Movements();
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    private void Movements()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 1.5f;
        float playerHeight = 2.3f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //if cannot move towards moveDir,attempt to move (X) only
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;

            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //if Cannot move only on the X, attempt Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;

                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ; 
                }
                else
                {
                    //Cannot move in any direction

                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance; 
        }

        

        isRunning = moveDir != Vector3.zero;

        //float rotateSpeed = 7f;
        //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); 
    }


}
