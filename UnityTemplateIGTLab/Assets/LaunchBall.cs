using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] float launchModifier;
    [SerializeField] float maxHold;
    float startHold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2)) {
            startHold = Time.time;
        } 

        if (Input.GetMouseButtonUp(2)) {
            Launch(launchModifier * (Mathf.Min(Time.time - startHold, maxHold) / maxHold));
        }
    }

    public void Launch(float launchForce) {
        GameObject tempBall = Instantiate(ball, this.transform.position, this.transform.rotation);
        tempBall.GetComponent<Rigidbody>().AddForce(launchForce * this.transform.forward, ForceMode.Impulse);
    }
}
