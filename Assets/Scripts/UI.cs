using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject MenuUiGM, LevelChoiseUiGm, GameStuff, GameUI1, GameOver,SettingsUI,HowToPlayGm,PauseUI,WinUI;
    [SerializeField] Text ScoreTextLevelChoise, ScoreTextGame;
    [SerializeField] Button NextButtton;
    AudioSource audio;
   [SerializeField] AudioClip menuMusic, GameMusic, click,clice,win,lose;
    int hp = 10;
    LevelManager levelManager;
    [SerializeField] GameObject LevelManagerGm;
    HpManager hpManager;
    [SerializeField] GameObject hpManagerGM;
    public Sprite[] slideSprites; // массив спрайтов для слайдов
    public Image slideImage; // ссылка на компонент изображения для отображения слайдов
    private int currentSlideIndex = 0; // индекс текущего слайда
    void Start()
    {
        levelManager = LevelManagerGm.GetComponent<LevelManager>();
        hpManager = hpManagerGM.GetComponent<HpManager>();
        audio = GetComponent<AudioSource>();
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("score", 0);
        if (PlayerPrefs.GetInt("first") == 0)
        {
            PlayerPrefs.SetInt("first", 1);
            PlayerPrefs.SetInt("MusicEnabled", 1);
            PlayerPrefs.SetInt("SoundEnabled", 1);
            PlayerPrefs.SetInt("VibeEnabled", 1);
        }
        if (PlayerPrefs.GetInt("MusicEnabled")==1)
        {
            audio.clip = menuMusic;
            audio.Play();
        }
        else
        {
            audio.clip = null;
            audio.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTextLevelChoise.text = PlayerPrefs.GetInt("score").ToString();
        ScoreTextGame.text = PlayerPrefs.GetInt("score").ToString();
       
    }
    public void ToLevelChoise()
    {
        UiOchist();
        Time.timeScale = 1;
        LevelChoiseUiGm.SetActive(true);
        PauseUI.SetActive(false);
        WinUI.SetActive(false);
        GameStuff.SetActive(false);
        hpManager.FullHp();
        if (PlayerPrefs.GetInt("MusicEnabled") == 1&&audio.clip!=menuMusic)
        {

            audio.clip = menuMusic;
            audio.Play();

        }
        levelManager.UpdateLevelCosts();
        levelManager.UpdateLevelAvailability();
        
    }
    public void BackMenu()
    {
        UiOchist();
        MenuUiGM.SetActive(true);
        slideImage.sprite = slideSprites[0];
        currentSlideIndex = 0;
    }
    public void ToGameOver()
    {
     
        UiOchist();
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("fruits");
        for (int i = 0; i < fruits.Length; i++)
        {
            Destroy(fruits[i]);
        }
        GameOver.SetActive(true);
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(lose);
        }
    }
    public void ToSettigns()
    {

        UiOchist();
        SettingsUI.SetActive(true);
    }
    public void UiOchist()
    {
        MenuUiGM.SetActive(false);
        LevelChoiseUiGm.SetActive(false);
        GameStuff.SetActive(false);
        GameUI1.SetActive(false);
        GameOver.SetActive(false);
        SettingsUI.SetActive(false);
        HowToPlayGm.SetActive(false);
        if (PlayerPrefs.GetInt("SoundEnabled") == 1 )
        {
            audio.PlayOneShot(click);
        }
        if (PlayerPrefs.GetInt("VibeEnabled") == 1)
        {
            Handheld.Vibrate();
        }
    }
    public void ToHowToPlayMenu()
    {
        UiOchist();
        HowToPlayGm.SetActive(true);
    }
    public void ToPause()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("fruits");
        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].gameObject.SetActive(false);
        }
        Time.timeScale = 0;
       PauseUI.SetActive(true);
        GameStuff.SetActive(false);
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(click);
        }
        if (PlayerPrefs.GetInt("VibeEnabled") == 1)
        {
            Handheld.Vibrate();
        }
    }
    public void BackPause()
    {
      
        GameStuff.SetActive(true);
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(click);
        }
        if (PlayerPrefs.GetInt("VibeEnabled") == 1)
        {
            Handheld.Vibrate();
        }
    }
    public void ToWin()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("fruits");
        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].gameObject.SetActive(false);
        }
        Time.timeScale = 0;
        WinUI.SetActive(true);
        GameStuff.SetActive(false);
        if (PlayerPrefs.GetInt("SoundEnabled") == 1)
        {
            audio.PlayOneShot(win);
        }
        switch (PlayerPrefs.GetInt("levelNumber")+1)
        {
            case 1:
                if (PlayerPrefs.GetInt("score")>=PlayerPrefs.GetInt("LevelCost1"))
                {
                    NextButtton.interactable = true;
                }
                else
                {
                    NextButtton.interactable = false;
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("score") >= PlayerPrefs.GetInt("LevelCost2"))
                {
                    NextButtton.interactable = true;
                }
                else
                {
                    NextButtton.interactable = false;
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("score") >= PlayerPrefs.GetInt("LevelCost3"))
                {
                    NextButtton.interactable = true;
                }
                else
                {
                    NextButtton.interactable = false;
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("score") >= PlayerPrefs.GetInt("LevelCost4"))
                {
                    NextButtton.interactable = true;
                }
                else
                {
                    NextButtton.interactable = false;
                }
                break;
            case 5:
                if (PlayerPrefs.GetInt("score") >= PlayerPrefs.GetInt("LevelCost5"))
                {
                    NextButtton.interactable = true;
                }
                else
                {
                    NextButtton.interactable = false;
                }
                break;
            default:
                NextButtton.interactable = false;
                break;
        }
    }

    public void Restart()
    {
        LoadLevel(1);
    }
    public void LoadLevel(int levelNubmer)
    {
        //Debug.Log("дааааааааа"+levelNubmer.ToString());
        UiOchist();
        GameStuff.SetActive(true);

        if (PlayerPrefs.GetInt("MusicEnabled") == 1)
        {
            audio.clip = GameMusic;
            audio.Play();
        }
        else
        {
            audio.clip = null;
            audio.Play();
        }
        
        switch (levelNubmer)
        {
            case 1:
                GameUI1.SetActive(true);
                PlayerPrefs.SetInt("levelNumber",1);
                break;
            case 2:
                GameUI1.SetActive(true);
                PlayerPrefs.SetInt("levelNumber", 2);
                break;
            case 3:
                GameUI1.SetActive(true);
                PlayerPrefs.SetInt("levelNumber", 3);
                break;
            case 4:
                GameUI1.SetActive(true);
                PlayerPrefs.SetInt("levelNumber", 4);
                break;
            case 5:
                GameUI1.SetActive(true);
                PlayerPrefs.SetInt("levelNumber", 5);
                break;
            default:
                // code block
                break;
                
        }
    }
    public void OnMusic()
    {
        if (audio.clip!= menuMusic)
        {
            audio.clip = menuMusic; audio.Play();
        }
       
    }
    public void OffMusic()
    {
        audio.clip = null; audio.Play();
    }
    public void CliceVol()
    {
        audio.PlayOneShot(clice);
    }
    public void NextLevel()
    {
        UiOchist();
        ToLevelChoise();
        levelManager.LoadNextLevel();
    }
    public void PereLoad()
    {
        UiOchist();
        ToLevelChoise();
        levelManager.LoadThisLevel();
    }
    public void NextSlide()
    {
        // Проверяем, не достигли ли мы последнего слайда
        if (currentSlideIndex < slideSprites.Length - 1)
        {
            // Увеличиваем индекс, чтобы перейти к следующему слайду
            currentSlideIndex++;
            // Устанавливаем спрайт для изображения слайда
            slideImage.sprite = slideSprites[currentSlideIndex];
        }
        else
        {
            slideImage.sprite = slideSprites[0];
            currentSlideIndex = 0;
            // Если мы достигли последнего слайда, вызываем другую функцию
            BackMenu();
        }
    }
}
