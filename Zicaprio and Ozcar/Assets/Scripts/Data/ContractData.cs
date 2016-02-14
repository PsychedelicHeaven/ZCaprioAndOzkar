using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ContractData
{
    public GameManager.Contract contractType;
    public List<ContractData> prequel = new List<ContractData>();
    public int contractTime = 6;                                          // In months
    public string description;
    public float baseRating = 1;
    public float baseXP = 50;
    public float baseCash = 100000;
    public int baseSP = 1;
    public List<GenreData> genre = new List<GenreData>();
    public List<SkillReq> skill = new List<SkillReq>();
    public List<SceneData> scene = new List<SceneData>();
    [HideInInspector]
    public float ratingAchieved;
    [HideInInspector]
    public bool completed;
}
