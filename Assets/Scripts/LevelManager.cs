using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons; // ������ ������ �������
    public Text[] levelCostTexts; // ������ ������� ��� ����������� ��� �� ������� �������
    public int[] levelCosts; // ������ ���������� ������� ������
    public int startingMoney = 0; // ��������� ���������� �����
    UI UiScript;
    [SerializeField] GameObject UiScdriptGm;

    private int currentMoney; // ������� ���������� �����
    private const string moneyPlayerPrefsKey = "score"; // ���� ��� ���������� ���������� ����� � PlayerPrefs

    void Start()
    {
        currentMoney = PlayerPrefs.GetInt(moneyPlayerPrefsKey, startingMoney);
        UiScript = UiScdriptGm.GetComponent<UI>();
        UpdateLevelCosts();
        UpdateLevelAvailability();
        PlayerPrefs.SetInt("LevelCost1", levelCosts[0]);
        PlayerPrefs.SetInt("LevelCost2", levelCosts[1]);
        PlayerPrefs.SetInt("LevelCost3", levelCosts[2]);
        PlayerPrefs.SetInt("LevelCost4", levelCosts[3]);
        PlayerPrefs.SetInt("LevelCost5", levelCosts[4]);
    }

    // ���������� ������� ��������� �������
    public void UpdateLevelCosts()
    {
        currentMoney = PlayerPrefs.GetInt(moneyPlayerPrefsKey, startingMoney);
        for (int i = 0; i < levelCostTexts.Length; i++)
        {
            levelCostTexts[i].text = levelCosts[i].ToString();
        }
    }

    // ���������� ����������� ������� � ������� ������
    public void UpdateLevelAvailability()
    {
        currentMoney = PlayerPrefs.GetInt(moneyPlayerPrefsKey, startingMoney);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool levelUnlocked = PlayerPrefs.GetInt("Level" + (i + 1), i == 0 ? 1 : 0) == 1;
            if (levelUnlocked)
            {
                levelButtons[i].interactable = true;
                levelCostTexts[i].text = "Play"; // �������� ����� �� "Play", ���� ������� ��� ������
            }
            else if (currentMoney >= levelCosts[i])
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    // ����� ��� ��������� ������� ������ ������
    public void LoadLevel(int levelIndex)
    {
        currentMoney = PlayerPrefs.GetInt(moneyPlayerPrefsKey, startingMoney);
        if (PlayerPrefs.GetInt("Level" + (levelIndex + 1)) != 1)
        {
            if (currentMoney >= levelCosts[levelIndex])
            {
                currentMoney -= levelCosts[levelIndex];
                PlayerPrefs.SetInt(moneyPlayerPrefsKey, currentMoney);
            }
            else
            {
                return;
            }
        }

        PlayerPrefs.SetInt("Level" + (levelIndex + 1), 1);
        PlayerPrefs.Save();

        UpdateLevelAvailability();

        //Debug.Log("Load Level " + levelIndex);
        UiScript.LoadLevel(levelIndex + 1);
    }

    public bool CheckLevelAvailability(int levelIndex)
    {
        bool levelUnlocked = PlayerPrefs.GetInt("Level" + (levelIndex + 1), levelIndex == 0 ? 1 : 0) == 1;
        return levelUnlocked || currentMoney >= levelCosts[levelIndex];
    }
     public void  LoadNextLevel()
    {
        LoadLevel(PlayerPrefs.GetInt("levelNumber"));
    }
    public void LoadThisLevel()
    {
        LoadLevel(PlayerPrefs.GetInt("levelNumber")-1);
    }

}
