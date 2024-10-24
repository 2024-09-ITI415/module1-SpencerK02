using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    const int LOOKBACK_COUNT = 10;

    [SerializeField]
    private bool _awake = true;
    public bool awake {
        get {return _awake;}
        private set {_awake = value;}
    }

    private Vector3 prevPos;
    //this private list stores the history of projectile's move distance
    private List<float> deltas = new List<float>();
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        awake = true;
        prevPos = new Vector3(1000,1000,0);
        deltas.Add(1000);
    }

    void FixedUpdate(){
        if (rigid.isKinematic || !awake) return;

        Vector3 deltaV3 = transform.position - prevPos;
        deltas.Add(deltaV3.magnitude);
        prevPos = transform.position;

        //limit lookback; one of the very few times that while is used
        while (deltas.Count > LOOKBACK_COUNT){
            deltas.RemoveAt(0);
        }

        //iterate over deltas and find the greatest one
        float maxDelta = 0;
        foreach (float f in deltas){
            if (f > maxDelta) maxDelta = f;
        }

        //if the projectile hasnt moved in more than the sleepthreshold
        if (maxDelta <= Physics.sleepThreshold){
            //set awake to false and put the rigidbody to sleep
            awake = false;
            rigid.Sleep();
        }
    }
}
