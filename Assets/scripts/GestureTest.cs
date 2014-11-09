using UnityEngine;
using Leap;
using System;
using System.Collections;

public class GestureTest : MonoBehaviour {

    public HandController HandController;
    public GameObject LeftHand;
    public GameObject RightHand;

    private GameObject LeftIndex;
    private GameObject RightIndex;

    private Controller _controller;

	// Use this for initialization
	void Start ()
    {
        _controller = HandController.leap_controller_;
        _controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!_controller.IsConnected)
            return;

        var frame = _controller.Frame();

        if (frame.Gestures().Count > 0)
        {
            foreach (var g in frame.Gestures())
            {
                if (g.Type == Gesture.GestureType.TYPE_CIRCLE)
                {
                    foreach (var hand in g.Hands)
                    {
                        if (hand.Frame.Id == g.Frame.Id)
                        {
                            
                            var indexFinger = hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];
                            var unityPos = indexFinger.TipPosition.ToUnityTrans();
                            
                            if (hand.IsLeft)
                            {
                                //Debug.Log("Left: " + hand.IsLeft);
                                Debug.Log("left***");
                                Debug.Log(LeftIndex.transform.position);
                            }
                            else if (hand.IsRight)
                            {
                                //Debug.Log("Right: " + hand.IsRight);
                                Debug.Log("right***");
                                Debug.Log(RightIndex.transform.position);
                            }
                        }
                    }
                }
            }
        }
	}

    public void SetRightHand(GameObject hand)
    {
        var handObj = hand.transform.Find("HandContainer");

        if (handObj == null)
            return;

        RightHand = handObj.transform.gameObject;
        var riggedFingers = RightHand.transform.GetComponentsInChildren<RiggedFinger>();

        RightIndex = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_INDEX).gameObject;
    }

    public void SetLeftHand(GameObject hand)
    {
        var handObj = hand.transform.Find("HandContainer");

        if (handObj == null)
            return;

        LeftHand = handObj.transform.gameObject;
        var riggedFingers = LeftHand.transform.GetComponentsInChildren<RiggedFinger>();

        LeftIndex = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_INDEX).gameObject;
    }
}
