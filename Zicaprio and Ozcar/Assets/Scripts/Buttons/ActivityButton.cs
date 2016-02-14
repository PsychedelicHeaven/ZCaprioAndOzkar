using UnityEngine;
using System.Collections;

public class ActivityButton : MonoBehaviour {

    public ActivityData activity;

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
        UIManager.Instance.activity_time.text = activity.timeReq + " month";
    }

    public void ChoseActivity()
    {
        UIManager.Instance.Chosen_activity = activity;
        UIManager.Instance.activity_is_chosen = true;
    }

}
