using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ActivityData
{
    public GameManager.Activity activityType;
    public List<SkillBonus> _skillBonus = new List<SkillBonus>();
    public float motivationBonus;
    public float satisfactionBonus;
    public float luxuryBonus;
    public float depressionBonus;
    public float cost = 100;
    public int timeReq = 1;
    public string description;
}
