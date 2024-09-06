using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpManager : MonoBehaviour
{
    public Slider healthSlider;
    UI uiscript;
    [SerializeField] GameObject uiScriptGm;
    int FruitsToWin;
    [SerializeField] int FruitToWinLevcel1, FruitToWinLevcel2, FruitToWinLevcel3, FruitToWinLevcel4, FruitToWinLevcel5;
   [SerializeField] Animator playerAnimator;
    // Метод для добавления здоровья
    private void Start()
    {
        uiscript = uiScriptGm.GetComponent<UI>();
       
    }
    private void Update()
    {
        if (healthSlider.value <= 0)
        {
            uiscript.ToGameOver();
            healthSlider.value = 1;
        }
        switch (PlayerPrefs.GetInt("levelNumber"))
        {
            case 1:
                if (FruitsToWin >= FruitToWinLevcel1)
                {
                    Win();
                    FruitsToWin = 0;
                }
                break;
            case 2:
                if (FruitsToWin >= FruitToWinLevcel2)
                {
                    Win();
                    FruitsToWin = 0;
                }
                break;
            case 3:
                if (FruitsToWin >= FruitToWinLevcel3)
                {
                    Win();
                    FruitsToWin = 0;
                }
                break;
            case 4:
                if (FruitsToWin >= FruitToWinLevcel4)
                {
                    Win();
                    FruitsToWin = 0;
                }
                break;
            case 5:
                if (FruitsToWin >= FruitToWinLevcel5)
                {
                    Win();
                    FruitsToWin = 0;
                }
                break;
            default:
                // code block
                break;

        }

        switch (PlayerPrefs.GetInt("levelNumber"))
        {
            case 1:
                healthSlider.value -= 0.05f * Time.deltaTime;
                break;
            case 2:
                healthSlider.value -= 0.08f * Time.deltaTime;
                break;
            case 3:
                healthSlider.value -= 0.1f * Time.deltaTime;
                break;
            case 4:
                healthSlider.value -= 0.15f * Time.deltaTime;
                break;
            case 5:
                healthSlider.value -= 0.25f * Time.deltaTime;
                break;
            default:
                // code block
                break;

        }
   
    }
    public void AddHealth(float amount)
    {
        playerAnimator.SetBool("eat",true);
        FruitsToWin ++;
        healthSlider.value += amount;
        if (healthSlider.value > healthSlider.maxValue)
        {
            healthSlider.value = healthSlider.maxValue;
        }
    }

    // Метод для уменьшения здоровья
    public void DecreaseHealth(float amount)
    {
        playerAnimator.SetBool("angry", true);
       
       
       
            healthSlider.value -= amount;
            if (healthSlider.value < 0)
            {
                healthSlider.value = 0;
            }
        
        
    }

    // Метод для плавного увеличения полоски здоровья до максимума
    public void IncreaseHealthToMax(float duration)
    {
        playerAnimator.SetBool("eat", true);
        
        StartCoroutine(IncreaseHealthOverTime(duration));
    }

    private System.Collections.IEnumerator IncreaseHealthOverTime(float duration)
    {
        float timer = 0f;
        float startValue = healthSlider.value;
        float endValue = healthSlider.maxValue;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(startValue, endValue, timer / duration);
            yield return null;
        }

        healthSlider.value = endValue; // Убедимся, что мы достигли конечного значения
    }
    public void CliceVolPeredach()
    {
        uiscript.CliceVol();
    }
    public void Win()
    {
        uiscript.ToWin();
    }
    public void FullHp()
    {
        healthSlider.value = 1;
    }
}
