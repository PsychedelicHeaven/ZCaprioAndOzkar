using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager instance = null;
    public static UIManager Instance { get { return instance; } }



    //skills

    public GameObject Skill_button_prefab;
    public GameObject New_Skill_button_prefab;

    public List<GameObject> current_active_Skills = new List<GameObject>();
    public List<GameObject> current_new_Skills = new List<GameObject>();

    public RectTransform Skills_active_ParentPanel;
    public RectTransform Skills_new_ParentPanel;

    public Text Skills_description;
    public Text Skill_level;
    public Slider Skill_progression;


    public SkillData Chosen_skill = null;
    public bool skill_is_chosen = false;
    //

    //Satisfaction, Luxury, Motivation
    public Slider Satisfaction_level;
    public Slider Satisfaction_min_level;
    public Slider Luxury_level;
    public Slider Luxury_min_level;
    public Slider Motivation_level;
    public Slider Motivation_min_level;


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
    void Start()
    {
        PopulateSkills();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSliders();
    }


    public void PopulateSkills()
    {
        for (int i = 0; i < current_active_Skills.Count; i++)
        {
            Destroy(current_active_Skills[i]);
        }

        for (int i = 0; i < current_new_Skills.Count; i++)
        {
            Destroy(current_new_Skills[i]);
        }


        for (int i = 0; i < Actor.Instance.currentSkills.Count; i++)
        {
            AddActiveSkillButton(Skills_active_ParentPanel, Actor.Instance.currentSkills[i]);
        }

        List<GameManager.skill> temp = Actor.Instance.NewSkills();
        for (int i = 0; i < temp.Count; i++)
        {
            AddNewSkillButton(Skills_new_ParentPanel, temp[i]);
        }
    }



    void AddActiveSkillButton(RectTransform ParentPanel, SkillData skill)
    {
        GameObject new_skill = (GameObject)Instantiate(Skill_button_prefab);
        new_skill.transform.SetParent(ParentPanel, false);
        new_skill.transform.localScale = new Vector3(1, 1, 1);
        new_skill.GetComponentInChildren<Text>().text = skill.skillType.ToString();
        new_skill.GetComponent<SkillButton>().skill_to_do = skill;
        current_active_Skills.Add(new_skill);
    }

    void AddNewSkillButton(RectTransform ParentPanel, GameManager.skill skill)
    {
        GameObject new_skill = (GameObject)Instantiate(New_Skill_button_prefab);
        new_skill.transform.SetParent(ParentPanel, false);
        new_skill.transform.localScale = new Vector3(1, 1, 1);
        new_skill.GetComponentInChildren<Text>().text = skill.ToString();
        new_skill.GetComponent<SkillButton>().skill_to_do = new SkillData(skill, 1, 1);
        current_new_Skills.Add(new_skill);
    }

    public void ActivateSkill()
    {
        if (skill_is_chosen)
        {
            Actor.Instance.AddSkill(Chosen_skill.skillType);
            PopulateSkills();

            Skill_level.text = "Level " + Chosen_skill.skillLevel;
            Skill_progression.value = Chosen_skill.proficiency / Chosen_skill.ProficiencyReq();
            skill_is_chosen = false;
        }
    }

    public void UpdateSliders()
    {
        Satisfaction_level.value = Actor.Instance.currentSatisfaction / 100;
        Motivation_level.value = Actor.Instance.currentMotivation / 100;
        Luxury_level.value = Actor.Instance.currentLuxury / 100;

        Satisfaction_min_level.value = Actor.Instance.minSatisfaction / 100;
        Motivation_min_level.value = Actor.Instance.minMotivation / 100;
        Luxury_min_level.value = Actor.Instance.minLuxury / 100;
    }

}


