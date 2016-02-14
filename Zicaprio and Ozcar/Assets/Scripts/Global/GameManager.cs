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
                        War,
                        none}

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

    /// <summary> The year the game starts </summary>
    [SerializeField]
    private int firstYear = 1990;
    /// <summary> The current year </summary>
    public int currentYear = 1990;
    /// <summary> Max years before game is over </summary>
    public int maxYears = 25;
    /// <summary> Current Trend in specific Genre </summary>
    public Genre currentTrend = Genre.none;
    /// <summary> List of contracts per year </summary>
    public List<ContractGrp> contractByYear = new List<ContractGrp>();
    /// <summary> Benefits from activities </summary>
    public List<ActivityData> activityDefs = new List<ActivityData>();
    /// <summary> yearly trends for genres </summary>
    public List<Genre> yearlyTrend = new List<Genre>();

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

    void Start()
    {
        currentYear = firstYear;
    }

    public int GetFirstYear()
    {
        return firstYear;
    }
}
