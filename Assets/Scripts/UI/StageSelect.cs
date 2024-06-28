using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
* StageSelect scene에서의 상호작용
*/
public class StageSelect : MonoBehaviour
{
    public void RetToMain() // 뒤로가기 버튼
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Start()
    {
        //Debug.Log("for문 시작");
        for(int i=0; i<8; i++)
        {
            string stageName = "Stage0" + (i + 1);
            //Debug.Log(stageName);
            //Debug.Log(PlayerPrefs.GetInt(stageName));
            //Debug.Log(i);
            
            if (PlayerPrefs.GetInt(stageName) == 1) transform.GetChild(i).GetComponent<Image>().color = Color.red;
        }
        //Debug.Log("for문 끝!");
    }
    public void StageBtn1()
    {
        Debug.Log("버튼 1 누음");
        Debug.Log(PlayerPrefs.GetInt("Stage01"));
        if (PlayerPrefs.GetInt("Stage01") == 1) SceneManager.LoadScene("Stage01");
    }
    public void StageBtn2()
    {
        if (PlayerPrefs.GetInt("Stage02") == 1) SceneManager.LoadScene("Stage02");
    }
    public void StageBtn3()
    {
        if (PlayerPrefs.GetInt("Stage03") == 1) SceneManager.LoadScene("Stage03");
    }
    public void StageBtn4()
    {
        if (PlayerPrefs.GetInt("Stage04") == 1) SceneManager.LoadScene("Stage04");
    }
    public void StageBtn5()
    {
        if (PlayerPrefs.GetInt("Stage05") == 1) SceneManager.LoadScene("Stage05");
    }
    public void StageBtn6()
    {
        if (PlayerPrefs.GetInt("Stage06") == 1) SceneManager.LoadScene("Stage06");
    }
    public void StageBtn7()
    {
        if (PlayerPrefs.GetInt("Stage07") == 1) SceneManager.LoadScene("Stage07");
    }
    public void StageBtn8()
    {
        if (PlayerPrefs.GetInt("Stage08") == 1) SceneManager.LoadScene("Stage08");
    }
}

