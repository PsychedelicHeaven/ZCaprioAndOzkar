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

    public SkillData()
    {
        skillType = GameManager.skill.BodyLanguage;
        proficiency = 1;
        skillLevel = 1;
    }
    public SkillData(GameManager.skill _skillType, float _proficiency, int _skillLevel)
    {
        skillType = _skillType;
        proficiency = _proficiency;
        skillLevel = _skillLevel;
    }
}
