using System;
using System.Collections;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;
    enum Achive
    {
        UnlockPotato , 
        UnlockBean
    }
    Achive[] achives;
    WaitForSecondsRealtime wait;
    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);
        if(!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }
    //�ʱ�ȭ
    private void Init()
    {
        PlayerPrefs.SetInt("MyData",1);

        foreach(Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }
    private void Start()
    {
        UnlockCharacter();
    }
    private void UnlockCharacter()
    {
        for(int index = 0; index < lockCharacter.Length; index++) 
        {
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }
    private void LateUpdate()
    {
        foreach(Achive achive in achives)
        {
            CheckActiove(achive);
        }
    }
    private void CheckActiove(Achive achive)
    {
        bool isAchive = false;

        switch(achive)
        {
            case Achive.UnlockPotato:
                isAchive = GameManager.Instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                isAchive = GameManager.Instance.gameTime == GameManager.Instance.maxGameTime;
                break;
        }
        //���Ǽ����Ϸ�
        if(isAchive && PlayerPrefs.GetInt(achive.ToString())==0) 
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }
            StartCoroutine(NoticeRoutine());
        }
    }
    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

         yield return wait; 

        uiNotice.SetActive(false);  
    }
}
