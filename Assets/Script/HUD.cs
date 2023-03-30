using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp , Level , Kill , Time , Health } //열거형 타입
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText= GetComponent<Text>();   
        mySlider= GetComponent<Slider>();
    }
    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp: //슬라이더에 적용할 값 : 현재 경험치 / 최대 경험치 
                float curExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp[Mathf.Min(GameManager.Instance.level,GameManager.Instance.nextExp.Length-1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}",GameManager.Instance.level);    
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                break;
            case InfoType.Time: //남은시간 구하기 
                float remainTime = GameManager.Instance.maxGameTime - GameManager.Instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format(("{0:D2}:{1:D2}"),min , sec);
                break;
            case InfoType.Health:
                float curHealth= GameManager.Instance.health;
                float maxHealth = GameManager.Instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
