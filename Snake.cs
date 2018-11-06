using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour {

    Vector2 dir = Vector2.right;
    List<Transform> tail = new List<Transform>();
    bool ate = false;
    public GameObject tailPrefab;
    float time = 0.3f;
    float speed = 0.3f;

    public Text score;
    int scorecount = 0;

    public Text lifes;
    int lifescount = 3;

    int velocity;

    // Use this for initialization
    void Start () {

        InvokeRepeating("Move", time, speed);

        lifescount = PlayerPrefs.GetInt("lives", 0);
        if(lifescount  < 0) {
            lifescount = 3;
        }
        lifes.text = "Lifes: " + lifescount;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (Input.GetKey(KeyCode.RightArrow)){
            dir = Vector2.right;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            dir = -Vector2.up;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            dir = -Vector2.right;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            dir = Vector2.up;
        }
    }

    void SpeedUp() {
        CancelInvoke();
        InvokeRepeating("Move", time, speed);
    }
    
    void Move() {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (ate) {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0) {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        // Food?
        if (coll.name.StartsWith("food")) {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
            SpawnFood.foodCount = 0;
            scorecount++;
            score.text = "Score: " + scorecount;
            speed = speed - 0.01f;
            SpeedUp();
        }
        // Collided with Tail or Border
        else {
            if(lifescount >= 0) {
                lifescount--;
                lifes.text = "Lifes: " + lifescount;

                PlayerPrefs.SetInt("lives", lifescount);
                PlayerPrefs.Save();
                
                SceneManager.LoadScene("Game");
            }
            if (lifescount < 0) {
                SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
            }
            
        }
    }
    
}
