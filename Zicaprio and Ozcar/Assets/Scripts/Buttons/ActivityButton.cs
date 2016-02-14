using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivityButton : MonoBehaviour {

    public ActivityData activity;
    public Text button_text;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowDescription()
    {
        UIManager.Instance.activity_description.text = activity.description;
    }
    public void ShowCost()
    {
        UIManager.Instance.activity_cost.text = activity.cost + "$";
        UIManager.Instance.activity_time.text = activity.timeReq + " months + " + (Actor.Instance.actualTime(activity.timeReq)- activity.timeReq);
    }

    public void ChoseActivity()
    {
        UIManager.Instance.Chosen_activity = activity;
        UIManager.Instance.activity_is_chosen = true;
    }

}
