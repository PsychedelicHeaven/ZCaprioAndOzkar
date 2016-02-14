using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    private static Actor instance = null;
    public static Actor Instance { get { return instance; } }

    // ACTOR PROPERTIES

    /// <summary> Actor level </summary>
    public int level = 1;
    /// <summary> Number of Fans </summary>
    public int fans = 0;
    /// <summary> $$$ </summary>
    public float cash = 100;
    /// <summary> Depression meter </summary>
    public float depression = 0;
    /// <summary> Yearly Expenditure </summary>
    public float expenditure = 0;
    /// <summary> Current SP </summary>
    public int SP = 0;
    /// <summary> Current XP </summary>
    public float XP = 0;
    /// <summary> Actors current Skills </summary>
    public List<SkillData> currentSkills = new List<SkillData>();
    /// <summary> List of completed contracts </summary>
    public List<ContractData> completedContracts = new List<ContractData>();

    /// <summary> Motivation </summary>
    public float minMotivation;
    public float currentMotivation;

    /// <summary> Luxury </summary>
    public float minLuxury;
    public float currentLuxury;

    /// <summary> Satisfaction </summary>
    public float minSatisfaction;
    public float currentSatisfaction;

    /// <summary> modifier for non dominant genres </summary>
    public float secondaryGenreWeight = 0.3f ;
    /// <summary> modifier skill bonus </summary>
    public float skillBonusWeight = 0.3f ;
    /// <summary> Effect of skill bonuses on movie rating</summary>
    public float skillRatingMultiplier = 0.5f;
    /// <summary> Proficiency required to advance from level 1 to 2 of any skill </summary>
    public float baseProficiencyReq = 100;
    /// <summary> XP required to advance actor from level 1 to 2 </summary>
    public float baseXPReq = 100;

    /// PRIVATE PROPERTIES

    /// <summary> Skill progression by level </summary>
    [SerializeField]
    private List<SkillGroup> skillByLevel = new List<SkillGroup>();
    /// <summary> Temp Rating bonus </summary>
    private float tempBonus;

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

    /// <summary>
    /// Calculate the difficulty of a certain scene w.r.t a certain contract
    /// </summary>
    /// <param name="_scene"></param>
    /// <param name="_contract"></param>
    /// <returns></returns>
    public int SceneDifficulty(SceneData _scene, ContractData _contract)
    {
        int ret = _scene.baseDifficulty;
        GenreData highestGenre = _contract.genre[0];
        foreach (GenreData g in _contract.genre)
        {
            if (g.genreDifficulty > highestGenre.genreDifficulty)
            {
                highestGenre = g;
            }
        }
        ret += highestGenre.genreDifficulty;
        int sum = 0;
        foreach (GenreData g in _contract.genre)
        {
            if (g != highestGenre)
            {
                sum += g.genreDifficulty;
            }
        }
        ret += (int)(secondaryGenreWeight * sum);
        int diff = 0;
        foreach(SkillReq s in _contract.skill)
        {
            foreach(SkillData sk in currentSkills)
            {
                if(s.skillType == sk.skillType && sk.skillLevel > s.minReq)
                {
                    diff += sk.skillLevel - s.minReq;
                }
            }
        }
        ret -= (int)(skillBonusWeight * diff);
        if(ret <= 1)
        {
            ret = 1;
        }
        return ret;
    }

    /// <summary>
    /// Add accomplishements of each scene to the bonus
    /// </summary>
    /// <param name="result"></param>
    public void EvaluateScene (float result)
    {
        tempBonus += result;
    }

    /// <summary>
    /// Evaluate the rating for the given contract
    /// </summary>
    /// <param name="_contract"></param>
    /// <returns> Rating </returns>
    public float EvaluateContract(ContractData _contract)
    {
        float rating = 0;
        int baseRating = 0;
        foreach (SkillReq s in _contract.skill)
        {
            foreach (SkillData sk in currentSkills)
            {
                if (s.skillType == sk.skillType && sk.skillLevel > s.minReq)
                {
                    baseRating += sk.skillLevel - s.minReq;
                }
            }
        }
        rating += skillRatingMultiplier * baseRating;
        rating += tempBonus;
        _contract.ratingAchieved = rating;
        _contract.completed = true;
        return rating;
    }

    /// <summary>
    /// Increase a certain skill by a certain amount
    /// </summary>
    /// <param name="_skill"></param>
    /// <param name="amount"></param>
    public void IncreaseSkill(GameManager.skill _skill, float amount)
    {
        foreach(SkillData sd in currentSkills)
        {
            if(sd.skillType == _skill)
            {
                sd.proficiency += amount;
                if (sd.proficiency > baseProficiencyReq * sd.skillLevel * (sd.skillLevel + 1) * 0.5f)
                {
                    sd.proficiency -= baseProficiencyReq * sd.skillLevel * (sd.skillLevel + 1) * 0.5f;
                    sd.skillLevel++;
                }
            }
        }
    }

    /// <summary>
    /// Add a new skill
    /// </summary>
    /// <param name="_skill"></param>
    public bool AddSkill(GameManager.skill _skill)
    {
        bool found = false;
        foreach(SkillData sd in currentSkills)
        {
            if(sd.skillType == _skill)
            {
                found = true;
            }
        }
        if(!found)
        {
            SkillData sd = new SkillData();
            sd.skillType = _skill;
            sd.proficiency = 1;
            sd.skillLevel = 1;
            currentSkills.Add(sd);
        }
        return !found;
    }

    /// <summary>
    /// Give player some XP
    /// </summary>
    /// <param name="amount"></param>
    public void AddXP(float amount)
    {
        XP += amount;
        if (XP > baseXPReq * level * (level + 1) * 0.5f)
        {
            XP -= baseXPReq * level * (level + 1) * 0.5f;
            level++;
        }
    }

    /// <summary>
    /// Returns a List of availible skills the player hasnt purchased yet
    /// </summary>
    /// <returns></returns>
    public List<GameManager.skill> NewSkills()
    {
        List<GameManager.skill> _new = new List<GameManager.skill>();
        for (int i = 0; i < level; i++)
        {
            foreach(GameManager.skill s in skillByLevel[i].skillGrp)
            {
                bool found = false;
                foreach(SkillData sd in currentSkills)
                {
                    if(sd.skillType == s)
                    {
                        found = true;
                    }
                }
                if(!found)
                {
                    _new.Add(s);
                }
            }
        }
        return _new;
    }
}
