using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RadialScript : MonoBehaviour {

    public RadialSet orange;
    public RadialSet green;
    public RotateScript arrow;


    public int difficulty;

    public float min_orange = 20;
    public float min_green = 10;

    public float min_orange_angle = 30;
    public float min_green_angle = 10;


    public float length_orange;
    public float length_green;


    public float score;


    public int amount_of_scenes;

    

    // Use this for initialization
    void Start () {
        ClearScene();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetScene(difficulty);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Actor.Instance.EvaluateScene(GetScore());
            transform.parent.gameObject.SetActive(false);
        }

    }

    public void ClearScene() // _difficulty : 0 - 100
    {
        score = 0;
        SetScene(difficulty);
    }

    public void SetScene(int _difficulty) // _difficulty : 0 - 100
    {
        orange.minangle = Random.Range(min_orange, 180 - 2*min_orange - 100 + difficulty);
        orange.maxangle = orange.minangle + 100 + min_orange - difficulty;

        length_orange = orange.maxangle - orange.minangle;

        green.minangle = Random.Range(orange.minangle + min_green, orange.maxangle - 2 * min_green - 50 + difficulty/2f);
        green.maxangle = green.minangle + 50 + min_green - difficulty/2f;

        length_green = green.maxangle - green.minangle;
    }

    public float GetScore() // _difficulty : 0 - 100
    {
        if(arrow.current_angle > green.minangle && arrow.current_angle < green.maxangle)
        {
            return (1f);
        }
        else if(arrow.current_angle > orange.minangle && arrow.current_angle < orange.maxangle)
        {
            return (0.0f);
        }
        else
        {
            return (-1.0f);
        }
    }
}
