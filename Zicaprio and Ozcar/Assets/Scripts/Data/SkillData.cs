using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillData
{
    public GameManager.skill skillType;
    public float proficiency = 1;
    public int skillLevel = 1;

    public float ProficiencyReq()
    {
        return Actor.Instance.baseProficiencyReq * skillLevel * (skillLevel + 1) * 0.5f;
    }
}
