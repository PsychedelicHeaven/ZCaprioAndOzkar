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

    /// <summary> Lifestyle requirements </summary>
    public enum lifeStyle { luxury,
                            leisure,
                            satisfaction}

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
