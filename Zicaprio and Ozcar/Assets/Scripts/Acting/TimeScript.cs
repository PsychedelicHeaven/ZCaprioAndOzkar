using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour {

    public int year = 0;

    public float date = 0;
    
    public float year_duration = 60;

    public Text date_text;
    public Text year_text;

    public List<GameObject> stop_timer_objects;


    private float month_duration = 0;

    bool temp = true;

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
            date = date + Time.deltaTime;
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
                break;
            default:
                date_text.text = "Bug";
                break;
       } 

    }
}
