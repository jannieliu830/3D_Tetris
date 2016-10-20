using UnityEngine;
using System.Collections;

public class FingerSwipe : MonoBehaviour {
    
    //public Camera cam;
    private Touch initTouch = new Touch();

    private float rotX = 0f;
    private float rotY = 0f;

    private Vector3 originalRotation;

    public float speed = 0.2f;
    public float dir = -1; // allow user to change direction during touch;
    void Start()
    {
        originalRotation = this.gameObject.transform.eulerAngles;
        rotX = originalRotation.x;
        rotY = originalRotation.y;
    }

    // Update is called once per frame
    void Update () {
        foreach (Touch touch in Input.touches)
        {
            if (Input.touchCount == 1)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    initTouch = touch;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    float deltaX = initTouch.position.x - touch.position.x;
                    float deltaY = initTouch.position.y - touch.position.y;
                    rotX -= deltaY * Time.deltaTime * speed * dir;
                    rotY += deltaX * Time.deltaTime * speed * dir;
                    rotX = Mathf.Clamp(rotX, 0.0f, 150.0f);
                    //rotY = Mathf.Clamp(rotY, -45f, 45f);
                    this.gameObject.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    initTouch = new Touch();
                }
            }
        }
	}
}
