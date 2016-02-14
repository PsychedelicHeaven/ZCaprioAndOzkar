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


    public SkillData Chosen_skill;
    public bool skill_is_chosen = false;
    //

    //Satisfaction, Luxury, Motivation
    public Slider Satisfaction_level;
    public Slider Satisfaction_min_level;
    public Slider Luxury_level;
    public Slider Luxury_min_level;
    public Slider Motivation_level;
    public Slider Motivation_min_level;
    //

    //Activities
    public GameObject activity_button_prefab;

    public RectTransform activity_ParentPanel;

    public Text activity_description;
    public Text activity_cost;
    public Text activity_time;


    public ActivityData Chosen_activity;
    public bool activity_is_chosen = false;
    //

    //Money

    public Text money_text;
    public Text expenditure_text;

    //

    // Contract
    public GameObject contract_button_prefab;

    public RectTransform contract_ParentPanel;

    public Text contract_description;
    public Text contract_cost;
    public Text contract_time;


    public ContractData Chosen_contract;
    public bool contract_is_chosen = false;

    public List<GameObject> contracts = new List<GameObject>(); 
    //

    int start_year = 0;

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
        start_year = TimeScript.Instance.year;

        PopulateSkills();
        PopulateActivities();
        PopulateContracts();

        
    }


    
    // Update is called once per frame
    void Update()
    {
        UpdateSliders();
        UpdateMoney();
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


    public void PopulateActivities()
    {
        for (int i = 0; i < GameManager.Instance.activityDefs.Count; i++)
        {
            AddNewActivityButton(activity_ParentPanel, GameManager.Instance.activityDefs[i]);
        }
    }

    void AddNewActivityButton(RectTransform ParentPanel, ActivityData _activity)
    {
        GameObject new_activity = (GameObject)Instantiate(activity_button_prefab);
        new_activity.transform.SetParent(ParentPanel, false);
        new_activity.transform.localScale = new Vector3(1, 1, 1);
        new_activity.GetComponentInChildren<Text>().text = _activity.activityType.ToString();
        new_activity.GetComponent<ActivityButton>().activity = _activity;
    }

    public void PerformActivity()
    {
        if (activity_is_chosen)
        {
            
            TimeScript.Instance.time_to_end = Chosen_activity.timeReq;
            TimeScript.Instance.activity_is_performing = true;
            activity_description.text = " ";
            activity_cost.text = "0";
            activity_time.text = " ";

            activity_is_chosen = false;
        }
    }

    public void UpdateMoney()
    {
        money_text.text = Actor.Instance.cash.ToString();
    }


    public void PopulateContracts()
    {

        for (int i = 0; i < contracts.Count; i++)
        {
            Destroy(contracts[i]);
        }

            
        for (int i = 0; i < GameManager.Instance.contractByYear[TimeScript.Instance.year - start_year].contractGrp.Count; i++)
        {
            AddNewContractButton(contract_ParentPanel, GameManager.Instance.contractByYear[TimeScript.Instance.year - start_year].contractGrp[i]);
        }
    }

    void AddNewContractButton(RectTransform ParentPanel, ContractData _contract)
    {
        GameObject new_contract = (GameObject)Instantiate(contract_button_prefab);
        new_contract.transform.SetParent(ParentPanel, false);
        new_contract.transform.localScale = new Vector3(1, 1, 1);
        new_contract.GetComponentInChildren<Text>().text = _contract.contractType.ToString();
        new_contract.GetComponent<ContractButton>().contract = _contract;
        contracts.Add(new_contract);
    }

    public void PerformContract()
    {
        if (contract_is_chosen)
        {
            TimeScript.Instance.StartScene((int)Actor.Instance.EvaluateContract(Chosen_contract));

            contract_description.text = " ";
            contract_cost.text = "0";
            contract_time.text = " ";

            contract_is_chosen = false;
        }
    }


}


