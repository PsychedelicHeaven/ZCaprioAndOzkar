using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    /// <summary> Skills </summary>
    public enum skill { MethodActing,
                        Looks,
                        BodyLanguage,
                        MartialArts,
                        Creativity,
                        Fitness,
                        Memory,
                        OnscreenChemistry,
                        TeamWork,
                        DialogueDelivery,
                        Improvisation,
                        Confidence,
                        Movement}

    /// <summary> Genre </summary>
    public enum Genre { Action,
                        Drama,
                        Comedy,
                        Romance,
                        Thriller,
                        Fantasy,
                        Horror,
                        MartialArts,
                        SciFi,
                        Crime,
                        War}

    /// <summary> Types of contracts </summary>
    public enum Contract { Commercial,
                           TV,
                           Movie,
                           Special}

    /// <summary> Types of activities our actor can do </summary>
    public enum Activity { Workout,
                           KungFu,
                           PlasticSurgery,
                           Therapy,
                           Phsychadelics,
                           ActingClasses,
                           DanceLessons,
                           Vacation,
                           FamilyTime,
                           Cars,
                           Sports,
                           Party,
                           Hobbies,
                           MusicLessons,
                           Steroids,
                           Charity,
                           Property}

    public List<SkillGroup> skillByLevel = new List<SkillGroup>();

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
