using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager instance = null;
    public static UIManager Instance { get { return instance; } }
    
    public GameObject Skill_button_prefab;

    
    public RectTransform Skills_active_ParentPanel;
    public RectTransform Skills_new_ParentPanel;

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


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddSkillButton(Skills_active_ParentPanel);
        }

    }

    void AddSkillButton(RectTransform ParentPanel)
    {
        GameObject new_skill = (GameObject)Instantiate(Skill_button_prefab);
        new_skill.transform.SetParent(ParentPanel, false);
        new_skill.transform.localScale = new Vector3(1, 1, 1);
    }


}
