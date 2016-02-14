using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {


    private static ImageManager instance = null;
    public static ImageManager Instance { get { return instance; } }

    public Image image;

    public Sprite Action;
    public Sprite Drama;
    public Sprite Comedy;
    public Sprite Romance;
    public Sprite Thriller;
    public Sprite Fantasy;
    public Sprite Horror;
    public Sprite MartialArts;
    public Sprite SciFi;
    public Sprite Crime;
    public Sprite War;
    public Sprite none;


    public Sprite default_image; 


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

    public void setDefaultBackground()
    {
        image.sprite = default_image;
    }

    public void setBackground()
    {
        if (UIManager.Instance.Chosen_contract.genre.Count > 0)
        {
            switch (UIManager.Instance.Chosen_contract.genre[0].genreType)
            {
                case GameManager.Genre.Action:
                    image.sprite = Action;
                    break;
                case GameManager.Genre.Drama:
                    image.sprite = Drama;
                    break;
                case GameManager.Genre.Comedy:
                    image.sprite = Comedy;
                    break;
                case GameManager.Genre.Romance:
                    image.sprite = Romance;
                    break;
                case GameManager.Genre.Thriller:
                    image.sprite = Thriller;
                    break;
                case GameManager.Genre.Fantasy:
                    image.sprite = Fantasy;
                    break;
                case GameManager.Genre.Horror:
                    image.sprite = Horror;
                    break;
                case GameManager.Genre.MartialArts:
                    image.sprite = MartialArts;
                    break;
                case GameManager.Genre.SciFi:
                    image.sprite = SciFi;
                    break;
                case GameManager.Genre.Crime:
                    image.sprite = Crime;
                    break;
                case GameManager.Genre.War:
                    image.sprite = War;
                    break;
                case GameManager.Genre.none:
                    image.sprite = none;
                    break;
                default:
                    break;
            }
        }
        else
        {
            image.sprite = default_image;
        }



    }


}
