using UnityEngine;
using System.Collections;

public class RadialScript : MonoBehaviour {

    public RadialSet orange;
    public RadialSet green;


    public int difficulty;

    public float min_orange = 20;
    public float min_green = 10;

    public float min_orange_angle = 30;
    public float min_green_angle = 10;


    public float length_orange;
    public float length_green;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetScene(difficulty);
        }
        
    }

    void SetScene(int _difficulty) // _difficulty : 0 - 100
    {
        orange.minangle = Random.Range(min_orange, 180 - 2*min_orange - 100 + difficulty);
        orange.maxangle = orange.minangle + 100 + min_orange - difficulty;

        length_orange = orange.maxangle - orange.minangle;

        green.minangle = Random.Range(orange.minangle + min_green, orange.maxangle - 2 * min_green - 50 + difficulty/2f);
        green.maxangle = green.minangle + 50 + min_green - difficulty/2f;

        length_green = green.maxangle - green.minangle;
    }
}
