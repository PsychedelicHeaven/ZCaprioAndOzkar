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
    /// <summary> Lifestyle Requirements </summary>
    [SerializeField]
    private List<LifestyleData> lifestyleReqs = new List<LifestyleData>();

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
}
