using UnityEngine;
using System.Collections;

public class SkillButton : MonoBehaviour {

    GameManager.skill skill_to_do;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    void ActivateSkill()
    {
        Actor.Instance.AddSkill(skill_to_do);
    }
    
}
