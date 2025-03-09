using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public enum Directions
    {
        left,
        right,
        up,
        down,
        noMove
    }

    public Directions swipeDirection;

    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 inputVector = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                if(inputVector.x > 0)
                {
                    swipeDirection = Directions.right;
                }
                else
                {
                    swipeDirection = Directions.left;
                }
            }
            else
            {
                if (inputVector.y > 0)
                {
                    swipeDirection = Directions.up;
                }
                else
                {
                    swipeDirection = Directions.down;
                }
            }

        }
        else
        {
            swipeDirection = Directions.noMove;
        }
    }
}
