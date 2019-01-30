using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class navmesh_click_move : MonoBehaviour {

    NavMeshAgent agent;
    Animator anim;
    private bool is_moving;
    private bool pause_toggle = false;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //Handle quitting
        if (Input.GetKey("escape") || Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        //Pause mechanic
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause_toggle = !pause_toggle;
        }
        if (pause_toggle) Time.timeScale = 0;
        else Time.timeScale = 1;

        //Restart mechanic
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Reload the scene
            SceneManager.LoadScene("scene_01");
        }

        //Click, raycast from camera and move agent to position
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }

        //Set animations to transition correctly
        if (agent.velocity != Vector3.zero)
        {
            //Moving
            anim.SetBool("is_walking", true);
        } else
        {
            //Not moving
            anim.SetBool("is_walking", false);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy Collision");
            //Lose condition
            SceneManager.LoadScene("lose_scene");
        }
    }
}
