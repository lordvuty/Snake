using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour , Interface {

    public GameObject foodPrefab;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    public static int foodCount = 1;

    // Use this for initialization
    void Start () {
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {

        if(foodCount <= 0) {
            Spawn();
            foodCount++;
        }

		
	}

    // Spawn one piece of food
    void Spawn() {

        int x = (int)Random.Range(borderLeft.position.x + 5, borderRight.position.x - 5);
        int y = (int)Random.Range(borderBottom.position.y - 5, borderTop.position.y + 5);
        
        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity); // default rotation
    }

}
