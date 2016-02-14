using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour {


    private static TimeScript instance = null;
    public static TimeScript Instance { get { return instance; } }

    public int year = 0;

    public float date = 0;
    
    public float year_duration = 60;

    public float time_scale = 1;


    public Text date_text;
    public Text year_text;

    public List<GameObject> stop_timer_objects;


    private float month_duration = 0;

    bool temp = true;



    //scene
    public RadialScript Radial;
    public GameObject UIBlocker;
    public GameObject SceneActing;
   
    public float time_to_next_scene;
    public int scene_counter;
    public int difficulty;

    public bool scene_is_performing = false;
    public float timer;

    public Text description_text;
    public Text score_text;
    public GameObject CompleteScreen;

    //

    //activity
    public GameObject ActivityWindow;
    public bool activity_is_performing = false;
    public float timer2;
    public float time_to_end;
    //

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }



    // Use this for initialization
    void Start () {

        month_duration = year_duration / 12;
        year_text.text = year.ToString();
    }
	
	// Update is called once per frame
	void Update () {

        
        temp = true;
        for (int i = 0; i < stop_timer_objects.Count; i++)
        {
            if(stop_timer_objects[i].activeSelf)
            {
                temp = false;
                break;
            }
        }

        if (temp)
        {
            date = date + time_scale*Time.deltaTime;

            if (scene_is_performing)
            {
                time_scale = 10;
                timer += time_scale * Time.deltaTime;

                UIBlocker.SetActive(true);
                if (timer > time_to_next_scene)
                {
                    UIBlocker.SetActive(false);
                    timer = 0;
                    StartScene(Actor.Instance.SceneDifficulty(UIManager.Instance.Chosen_contract.scene[scene_counter], UIManager.Instance.Chosen_contract));
                }
            }
            else if(activity_is_performing)
            {
                time_scale = 10;
                timer2 += time_scale * Time.deltaTime;

                UIBlocker.SetActive(true);
                if (timer2 > time_to_end)
                {
                    Actor.Instance.PerformActivity(UIManager.Instance.Chosen_activity.activityType);
                    UIBlocker.SetActive(false);
                    timer2 = 0;
                    time_scale = 1;
                    activity_is_performing = false;
                }
            }


        }

        GetComponent<Image>().fillAmount = date / year_duration;
        GetComponent<Image>().color = Color.Lerp(Color.green, Color.red, date / year_duration);

        switch ((int)(date / month_duration))
        {
            case 0:
                date_text.text = "January";
                break;
            case 1:
                date_text.text = "Febrary";
                break;
            case 2:
                date_text.text = "March";
                break;
            case 3:
                date_text.text = "April";
                break;
            case 4:
                date_text.text = "May";
                break;
            case 5:
                date_text.text = "June";
                break;
            case 6:
                date_text.text = "July";
                break;
            case 7:
                date_text.text = "August";
                break;
            case 8:
                date_text.text = "September";
                break;
            case 9:
                date_text.text = "October";
                break;
            case 10:
                date_text.text = "November";
                break;
            case 11:
                date_text.text = "December";
                break;
            case 12:
                date = 0;
                year++;
                year_text.text = year.ToString();
                UIManager.Instance.PopulateContracts();
                break;
            default:
                date_text.text = "Bug";
                break;
       } 

    }


    public void StartScene(int _difficulty)
    {
        SceneActing.SetActive(true);
        difficulty = _difficulty;
        Radial.SetScene(_difficulty);
        scene_is_performing = false;
        scene_counter++;

        if (scene_counter < UIManager.Instance.Chosen_contract.scene.Count)
        {
            scene_is_performing = true;
            time_to_next_scene = UIManager.Instance.Chosen_contract.contractTime / UIManager.Instance.Chosen_contract.scene.Count;
        }
        else
        {
            scene_counter = 0;
            time_scale = 1;
            description_text.text = UIManager.Instance.Chosen_contract.description;
            score_text.text = "Rating: " + Actor.Instance.EvaluateContract(UIManager.Instance.Chosen_contract);
            SceneActing.SetActive(false);
            CompleteScreen.SetActive(true);

        }

    }



}
