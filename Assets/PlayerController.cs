using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float force;
    private Animator anm;
    private Rigidbody rb;
    private bool isJumping;
    void Start()
    {
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isJumping){
            anm.SetBool("Jumping", true);
            rb.AddForce(UnityEngine.Vector3.up * force, ForceMode.Impulse);
            isJumping = false;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.CompareTag("Plane"))
        {
            isJumping = true;
            anm.SetBool("Jumping", false);
        }
    }
}
