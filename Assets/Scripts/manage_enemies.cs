using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manage_enemies : MonoBehaviour {

    List<GameObject> enemies;
    public GameObject granny_prefab;

    float timer;
    bool spawned_first = false;
    bool spawned_second = false;

    int score = 0;

	// Use this for initialization
	void Start () {
        enemies = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >= 3 && !spawned_first)
        {
            spawned_first = true;
            enemies.Add(Instantiate(granny_prefab, new Vector3(-6f, 0.5f, -4f), Quaternion.Euler(0, 0, 0)));
        }

        if (timer >= 5 && !spawned_second)
        {
            spawned_second = true;
            enemies.Add(Instantiate(granny_prefab, new Vector3(6f, 0.5f, -5f), Quaternion.Euler(0, 0, 0)));
            enemies.Add(Instantiate(granny_prefab, new Vector3(9f, 0.5f, 2f), Quaternion.Euler(0, 0, 0)));
        }
        
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject.transform.position.y < -20)
            {
                enemies.RemoveAt(i);
                score++;
            }
        }

        if (score == 3)
        {
            //Win condition
            SceneManager.LoadScene("win_scene");
        }
    }
}
