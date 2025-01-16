using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RepeatEarth : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 50f;
    private float bcl;
    private UnityEngine.Vector3 startPos;

    void Start()
    {
        bcl = GetComponent<BoxCollider>().size.z/2;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(UnityEngine.Vector3.back * speed * Time.deltaTime);
        if(transform.position.z < -206.2f)
        {
            transform.position = startPos;

        }
    }
}
