using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] float maxRotation;
    //[SerializeField] float flipForce;

    bool isFlipped;
    [SerializeField] float pressWindow;
    [SerializeField] float maxActiveDuration;
    float activeDuration;
    float lastPressedLeft;
    float lastPressedRight;
    Vector3 minRotationPosition;
    Vector3 maxRotationPosition;
    bool leftFlipped;
    bool rightFlipped;




    // Start is called before the first frame update
    void Start()
    {
        activeDuration = maxActiveDuration;
        minRotationPosition = this.transform.localRotation.eulerAngles;
        maxRotationPosition = new Vector3(minRotationPosition.x, minRotationPosition.y + maxRotation, minRotationPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        activeDuration -= Time.deltaTime;
        lastPressedLeft -= Time.deltaTime;
        lastPressedRight -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && lastPressedLeft <= 0) {
            leftFlipped = true;
            lastPressedLeft = pressWindow;
        }

        if (Input.GetMouseButtonDown(1) && lastPressedRight <= 0) {
            rightFlipped = true;
            lastPressedRight = pressWindow;
        }

        if (lastPressedRight <= 0) {
            rightFlipped = false;
        }

        if (lastPressedLeft <= 0) {
            leftFlipped = false;
        }

        if (rightFlipped && leftFlipped) {
            Flip();
            rightFlipped = false;
            leftFlipped = false;
        }

        if (activeDuration <= 0) {
            UnFlip();
        }
    }

    public void Flip() {
        this.transform.localRotation = Quaternion.Euler(maxRotationPosition);
        isFlipped = true;
        activeDuration = maxActiveDuration;
    }

    public void UnFlip() {
        this.transform.localRotation = Quaternion.Euler(minRotationPosition);
        isFlipped = false;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("pog");
        if (other.tag == "ball" && isFlipped) {
            Destroy(other.gameObject);
        }
    }

    
    private void OnCollisionEnter(Collision other) {
        Debug.Log("log");
        if (other.gameObject.tag == "ball" && isFlipped) {
            Destroy(other.gameObject);
        }
    }
}
