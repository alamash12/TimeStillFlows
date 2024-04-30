using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    /*
     * MainSceneManager : ����ȭ�鿡�� �Ͼ�� ���� ��ȯ�� ���� ��ȣ�ۿ���� �����ϴ� Ŭ�����Դϴ�.
     */
    public void StartNewGame() // NewGame�� ���� �� Introȭ������ ���� ��ȯ��.
    {
        SceneManager.LoadScene("Intro"); 
    }
    public void ContinueGame() // Continue�� ���� �� StageSelectȭ������ ���� ��ȯ��.
    {
        SceneManager.LoadScene("StageSelect"); 
    }
    public void GameQuit() // Exit�� ���� �� ���ø����̼� ����
    {
        Application.Quit();
    }

    /*
     * �ɼ� �гΰ���
     */
    public GameObject optionPanel; // �ɼ� �г�
    public GameObject outsideOptionPanel; // �ɼ� �ٱ��г�
    /*public void Start()
    {
        optionPanel.SetActive(false); // ���� ���۽� �ɼ��г��� ��Ȱ��ȭ
        outsideOptionPanel.SetActive(false); // ���� ���۽� �ɼ� �ٱ��г��� ��Ȱ��ȭ
    }*/
    public void ToggleOptionPanel()// �ɼǹ�ư�� ���� �� �ɼ� �гΰ� �ɼ� �ٱ��г��� ���
    {
        if(!optionPanel.activeSelf) optionPanel.SetActive(true);
        if(!outsideOptionPanel.activeSelf) outsideOptionPanel.SetActive(true);
        //optionPanel.SetActive(!optionPanel.activeSelf);
        //outsideOptionPanel.SetActive(!outsideOptionPanel.activeSelf);
    }
    public void CloseOptionPanel()// �ɼ� �ٱ��г��� ���� �� �ɼ� �гΰ� �ɼ� �ٱ��г��� ����
    {
        optionPanel.SetActive (false);
        outsideOptionPanel.SetActive(false);
    }
}
