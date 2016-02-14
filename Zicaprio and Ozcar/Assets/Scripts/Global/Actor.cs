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
    /// <summary> Max level the player can reach </summary>
    public int maxLevel = 10;
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

    /// <summary> Proficiency required to advance from level 1 to 2 of any skill </summary>
    public float baseProficiencyReq = 100;
    /// <summary> XP required to advance actor from level 1 to 2 </summary>
    public float baseXPReq = 100;
    /// <summary> modifier for non dominant genres </summary>
    public float secondaryGenreWeight = 0.3f;
    /// <summary> modifier skill bonus </summary>
    public float skillBonusWeight = 0.3f;
    /// <summary> Effect of skill bonuses on movie rating</summary>
    public float skillRatingMultiplier = 0.5f;
    /// <summary> Difficulty of doing a sequel </summary>
    public float sequelDifficulty = 10;
    /// <summary> Fans to Ratings multiplier </summary>
    public int fansMultiplier = 1;
    // <summary> Bonus fans due to popular trend </summary>
    public int fanTrendMultiplier = 1;
    // <summary> Depression Modifier for satisfaction, luxury etc. </summary>
    public int depressionModifier = 1;
    // <summary> Depression Time Modifier </summary>
    public int depressionTimeModifier = 1;

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
        ret += (int)(sequelDifficulty * _contract.prequel.Count);
        if(ret <= 1)
        {
            ret = 1;
        }
        else if(ret > 100)
        {
            ret = 100;
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
        cash += ((Mathf.Max(0, rating - 5) / 2.5f) + 1) * _contract.baseCash;
        XP += ((Mathf.Max(0, rating - 5) / 2.5f) + 1) * _contract.baseXP;
        SP += ((int)(Mathf.Max(0, rating - 5) / 1.6f) + 1) * _contract.baseSP;
        fans += ((int)(rating * rating) - 24) * fansMultiplier * ((int)Mathf.Sqrt(level));
        foreach(GenreData g in _contract.genre)
        {
            if(g.genreType == GameManager.Instance.currentTrend)
            {
                fans += fanTrendMultiplier;
            }
        }
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

    public void AddMotivation(float amount)
    {
        currentMotivation += amount;
        if (currentMotivation > 100)
        {
            currentMotivation = 100;
        }
    }

    public void AddLuxury (float amount)
    {
        currentLuxury += amount;
        if (currentLuxury > 100)
        {
            currentLuxury = 100;
        }
    }

    public void AddSatisfaction(float amount)
    {
        currentSatisfaction += amount;
        if (currentSatisfaction > 100)
        {
            currentSatisfaction = 100;
        }
    }

    public void AddDepression(float amount)
    {
        depression += amount;
        if (depression > 100)
        {
            depression = 100;
        }
        else if(depression < 0)
        {
            depression = 0;
        }
    }

    /// <summary>
    /// Rewards due to completion of activity
    /// </summary>
    /// <param name="_activity"></param>
    public void PerformActivity(GameManager.Activity _activity)
    {
        ActivityData _data = new ActivityData();
        foreach(ActivityData ad in GameManager.Instance.activityDefs)
        {
            if(ad.activityType == _activity)
            {
                _data = ad;
            }
        }
        AddSatisfaction(_data.satisfactionBonus);
        AddMotivation(_data.motivationBonus);
        AddLuxury(_data.luxuryBonus);
        AddDepression(-_data.depressionBonus);
        if (_data != null)
        {
            foreach (SkillBonus sb in _data._skillBonus)
            {
                IncreaseSkill(sb.skillType, sb.proficiencyAdd);
            }
        }
    }

    /// <summary>
    /// Evaluate end of year
    /// </summary>
    public void EvaluateYear()
    {
        float dep = 0;
        if(currentLuxury < minLuxury)
        {
            dep += minLuxury - currentLuxury;
        }
        if (currentMotivation < minMotivation)
        {
            dep += minMotivation - currentMotivation;
        }
        if (currentSatisfaction < minSatisfaction)
        {
            dep += minSatisfaction - currentSatisfaction;
        }
        dep += depressionModifier * dep;
        GameManager.Instance.currentYear++;
        if(GameManager.Instance.yearlyTrend.Count > GameManager.Instance.currentYear - GameManager.Instance.GetFirstYear())
        {
            GameManager.Instance.currentTrend = GameManager.Instance.yearlyTrend[GameManager.Instance.currentYear - GameManager.Instance.GetFirstYear()];
        }
        minLuxury = 10 * level;
        minMotivation = 10 * level;
        minSatisfaction = 10 * level;
    }

    /// <summary>
    /// Time required for contracts, activities, modified by depression
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public int actualTime(int time)
    {
        return (int)((depressionTimeModifier * depression + 1) * time);
    }
}
