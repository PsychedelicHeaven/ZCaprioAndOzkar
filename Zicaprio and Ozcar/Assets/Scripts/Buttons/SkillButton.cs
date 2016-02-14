using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    public SkillData skill_to_do;
    public int cost;

    public Text button_text;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    public void ActivateSkill()
    {
        Actor.Instance.AddSkill(skill_to_do.skillType, cost);
    }

    public void ShowDescription()
    {
        UIManager.Instance.Skills_description.text = skill_to_do.skillType.ToString();
    }
    public void ShowLevel()
    {
        UIManager.Instance.Skill_level.text = "Level " + skill_to_do.skillLevel;
        UIManager.Instance.Skill_progression.value = skill_to_do.proficiency/skill_to_do.ProficiencyReq();
    }

    public void HideButton()
    {
        UIManager.Instance.choose_skill.interactable = false;
    }

    public void OpenButton()
    {
        UIManager.Instance.choose_skill.interactable = true;
    }

    public void ChoseSkill()
    {
        UIManager.Instance.Chosen_skill = skill_to_do;
        UIManager.Instance.skill_is_chosen = true;
        UIManager.Instance.cost = cost;
    }
}
