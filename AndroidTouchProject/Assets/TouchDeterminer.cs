﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDeterminer : MonoBehaviour
{

    private float tapTimer = 0f;
    private float MAX_ALLOWED_TAP_TIME = 0.2f;
    private bool hasMoved;

    TouchManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<TouchManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.touchCount);
        //Update times
        tapTimer += Time.deltaTime;
        if (Input.touchCount > 0)
        {
            Touch[] all_touches = Input.touches;
            Touch firstTouch = all_touches[0];
            List<Vector2> touchPositions = new List<Vector2>();
            print(firstTouch.phase);

            switch (all_touches.Length)
            {
                case 1:
                    touchPositions.Add(firstTouch.position);
                    switch (firstTouch.phase)
                    {
                        case TouchPhase.Began:
                            tapTimer = 0f;
                            hasMoved = false;
                            break;

                        case TouchPhase.Moved:
                            hasMoved = true;
                            if (tapTimer > MAX_ALLOWED_TAP_TIME && hasMoved)
                            {
                                manager.drag(touchPositions);
                            }
                            break;

                        case TouchPhase.Stationary:
                            break;

                        case TouchPhase.Ended:
                            if ((tapTimer < MAX_ALLOWED_TAP_TIME) && !hasMoved)
                            {
                                //print("Tapped at " + firstTouch.position + " Duration: " + tapTimer + " seconds");
                                //Ray tapRay = Camera.main.ScreenPointToRay(firstTouch.position);
                                //if (Physics.Raycast(tapRay))
                                //{
                                //    print("I hit something");
                                //}
                                manager.tap(firstTouch.position);
                            }
                            touchPositions.Clear();
                            break;
                    }
                    break;
                case 2:

                    break;
            }
        }
    }
}
