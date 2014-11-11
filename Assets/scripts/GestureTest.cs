using UnityEngine;
using Leap;
using System;
using System.Collections;

public class GestureTest : MonoBehaviour {

    public HandController HandController;
    public GameObject LeftHand;
    public GameObject RightHand;

    private GameObject LeftIndex;
    private GameObject LeftThumb;
    private GameObject LeftMiddle;
    private GameObject LeftRing;
    private GameObject LeftPinky;

    private GameObject RightIndex;
    private GameObject RightThumb;
    private GameObject RightMiddle;
    private GameObject RightRing;
    private GameObject RightPinky;

	public GameObject Fireball;

	private bool bCreateLeftFireball = true,
	bCreateRightFireball = true;

    private Controller _controller;

	// Use this for initialization
	void Start ()
    {
        _controller = HandController.leap_controller_;
        _controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);

			_controller.Config.SetFloat("Gesture.Circle.MinRadius", 50.0f);
		_controller.Config.SetFloat("Gesture.Circle.MinArc", .5f);
		_controller.Config.Save();


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
                                
								GameObject createdLeftFireBall = null;

                                Debug.Log("left***");
                                Debug.Log(LeftIndex.transform.position);

								if(bCreateLeftFireball==true){
									
									createdLeftFireBall = Instantiate (Fireball,LeftIndex.transform.position + new Vector3 (0, 0, 4),Quaternion.identity) as GameObject;
									
									if(createdLeftFireBall != null)
										createdLeftFireBall.rigidbody.velocity = transform.TransformDirection (new Vector3 (0, 0, 50));
									
									bCreateLeftFireball=false;
								}
									StartCoroutine (FireBallSwitchLeft());
									
								
                            }
                            else if (hand.IsRight)
                            {
                                
								GameObject createdRightFireBall = null;

                                Debug.Log("right***");
                                Debug.Log(RightIndex.transform.position);

								if(bCreateRightFireball==true){
									
									createdRightFireBall = Instantiate (Fireball,RightIndex.transform.position + new Vector3 (0, 0, 4),Quaternion.identity) as GameObject;
									
									if(createdRightFireBall != null)
										createdRightFireBall.rigidbody.velocity = transform.TransformDirection (new Vector3 (0, 0, 50));
									
									bCreateRightFireball=false;
								}	
									StartCoroutine (FireBallSwitchRight());
									

                            }
                        }
                    }
                }
            }
        }
	}

	public IEnumerator FireBallSwitchLeft(){ //this launches the projectile at the enemy
		
		while (true) {	
			print ("left coroutine running");
			yield return new WaitForSeconds (1.5f);
			bCreateLeftFireball=true;
			return false;	
		}
	}
	
	public IEnumerator FireBallSwitchRight(){ //this launches the projectile at the enemy
		
		while (true) {	
			print ("right coroutine running");
			yield return new WaitForSeconds (1.5f);
			bCreateRightFireball=true;
			return false;	
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
        RightThumb = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_THUMB).gameObject;
        RightMiddle = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_MIDDLE).gameObject;
        RightRing = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_RING).gameObject;
        RightPinky = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_PINKY).gameObject;
    }

    public void SetLeftHand(GameObject hand)
    {
        var handObj = hand.transform.Find("HandContainer");

        if (handObj == null)
            return;

        LeftHand = handObj.transform.gameObject;
        var riggedFingers = LeftHand.transform.GetComponentsInChildren<RiggedFinger>();

        LeftIndex = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_INDEX).gameObject;
        LeftThumb = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_THUMB).gameObject;
        LeftMiddle = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_MIDDLE).gameObject;
        LeftRing = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_RING).gameObject;
        LeftPinky = Array.Find(riggedFingers, x => x.fingerType == Finger.FingerType.TYPE_PINKY).gameObject;
    }
}
