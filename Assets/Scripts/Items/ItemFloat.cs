using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    public AnimationCurve myCurve;

    GameObject parent;

    private void Start()
    {
        parent = this.gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)) + parent.gameObject.transform.position.y+1, transform.position.z);
    }
}
