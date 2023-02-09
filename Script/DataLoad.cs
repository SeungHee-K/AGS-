using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataLoad : MonoBehaviour
{

    bool savefile =false;
    public bool NewGameBool = false;

    public GameObject WarringdeletPanel;

    public StoreManager storeManager;

    void Start()
    {
            if (File.Exists(Datamanager.instance.path + $"0"))	// save0 �����Ͱ� �ִ� ���
            {
                savefile = true;			// �ش� ���� ��ȣ�� bool�迭 true�� ��ȯ
                Datamanager.instance.nowSlot = 0;	// ���� ��ȣ ����
                Datamanager.instance.LoadData();	// �ش� ���� ������ �ҷ���
            }
            else	// �����Ͱ� ���� ���
            {
            Debug.Log("������ ����");

            }
        Datamanager.instance.DataClear();
        start();
    }

    public void start()	
    {
        Datamanager.instance.nowSlot = 0;
        if (savefile)	// bool �迭���� ���� true��� = ������ �����Ѵٴ� ��
        {
            Datamanager.instance.LoadData();	// �����͸� �ε��ϰ�
            GoGame();	
        }
        else	// bool �迭���� ���� false��� �����Ͱ� ���ٴ� ��
        {
            GoGame();
        }
    }

    public void DeleteDataPanel()
    {
        Datamanager.instance.nowSlot = 0;	

        if (savefile)	// bool �迭���� ���� true��� = ������ �����Ѵٴ� ��
        {
            Datamanager.instance.deleteReady = true;
            Datamanager.instance.deletenum = 0;
            WarringdeletPanel.SetActive(true);
        }
        else
        {
            Debug.Log("������ �����Ͱ� �����ϴ�.");
        }
    }

    public void DeleteSaveFile()// ������ ����
    {
        Datamanager.instance.DeleteData();
        savefile = false;
        Datamanager.instance.deletenum = -1;
        start();
    }

    public void GoGame()	// ���Ӿ����� �̵�
    {
        if (!savefile)
        {
            Datamanager.instance.SaveData(); // ���� ������ ������.
        }
        if (SceneManager.GetActiveScene().name=="Lobby")
        {
            storeManager.Load();
        }
        
       // SceneManager.LoadScene(Datamanager.instance.nowPlayer.); // ���Ӿ����� �̵�
    }




}
