using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Madeby : MonoBehaviour
{
    public GameObject madeByPanel; // 옵션 패널

    public void OpenPanel()// 옵션버튼을 누를 시 옵션 패널 생성
    {
        madeByPanel.SetActive(!madeByPanel.activeSelf);
    }
    public void ClosePanel()// 옵션 x 버튼을 누를 시 옵션 패널 없앰
    {
        madeByPanel.SetActive(false);
    }
}
