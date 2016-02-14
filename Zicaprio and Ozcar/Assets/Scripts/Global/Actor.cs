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
    /// <summary> Actors current Skills </summary>
    public List<SkillData> currentSkills = new List<SkillData>();
    /// <summary> Motivation </summary>
    public float minMotivation;
    public float currentMotivation;

    /// <summary> Luxury </summary>
    public float minLuxury;
    public float currentLuxury;

    /// <summary> Satisfaction </summary>
    public float minSatisfaction;
    public float currentSatisfaction;

    //VARIABLE PROPERTIES

    /// <summary> modifier for non dominant genres </summary>
    public float secondaryGenreWeight = 0.3f ;
    /// <summary> modifier skill bonus </summary>
    public float skillBonusWeight = 0.3f ;
    /// <summary> Addiction level, dependency on drugs </summary>
    public int addictionLevel = 0;

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
}
