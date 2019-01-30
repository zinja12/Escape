using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_enemy : MonoBehaviour {
    
    private Transform target;
    public float speed = 0.5f;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < 5){
            //Moving
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
            
            Debug.DrawRay(transform.position, transform.rotation * Vector3.forward, Color.red);
            transform.position += transform.forward * Time.deltaTime * speed;

            //Set animation transition
            anim.SetBool("is_moving", true);
        } else
        {
            //Not moving
            anim.SetBool("is_moving", false);
            transform.Rotate(0, 0 * Time.deltaTime, 0);
        }

        Debug.DrawRay(transform.position, -transform.up, Color.red);
        if (!on_ground())
        {
            //Debug.Log("Not grounded. Play falling animation");
            anim.SetBool("is_falling", true);
        }
    }

    private bool on_ground()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, -transform.up, out hit, 10))
        {
            return true;
        }
        return false;
    }
}
