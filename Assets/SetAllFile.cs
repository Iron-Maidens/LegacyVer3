using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SetAllFile : MonoBehaviour {
    string pathEvent,path, pathCurrentIndexitem;
    // Use this for initialization
    public List<Sprite> listAllImgItem;
    public List<Sprite> listAllImgEvent;
    public List<GameObject> listPage;
    public Text pageText;
    int curentNumberItem;
    int[] indexAllInventory = new int[53];
    Sprite[] currentHaveItem = new Sprite[53];
    int currentPage = 1;
    Image listImgItem;
    public int mode;


    int countEvent = 0;
    int[] inventoryEvent = new int[10];
    int pageEvent = 1;
    void Start () {
        path = Application.persistentDataPath + "list.json";
        pathCurrentIndexitem = Application.persistentDataPath + "currentIndex.json";
        pathEvent = Application.persistentDataPath + "chkEventList.json";

        string f1 = File.ReadAllText(path);
        string f2 = File.ReadAllText(pathCurrentIndexitem);
         string f3 = File.ReadAllText(pathEvent);

        int[] numbers = JsonHelper.getJsonArray<int>(f2);
        curentNumberItem = numbers[0];
        numbers = JsonHelper.getJsonArray<int>(f1);

        for (int i = 0; i < 49; i++)
        {
            indexAllInventory[i] = numbers[i];
            currentHaveItem[i] = listAllImgItem[numbers[i]];
            Debug.Log(numbers[i]);

        }
        pageEvent = 1;
        countEvent = 0;
        numbers = JsonHelper.getJsonArray<int>(f3);
        for (int i=0 ; i<10 ; i++)
        {
            if(numbers[i]==1)
            {
                countEvent++;
            }
            inventoryEvent[i] = numbers[i];
        }

        if (mode == 1)
        {   
            updatePageItem();
            Debug.Log("Item : " + curentNumberItem);
        } 
        else if(mode==2)
        {
            Image img;
            img = listPage[0].GetComponent<Image>();
            img.sprite = currentHaveItem[SetAllFile.indexSelected];
            revFomula();
        }
        else if (mode == 3)
        {
            updatePageEvent();
        }
    }

    // Update is called once per frame
    void Update () {
        staticCurrentItem = curentNumberItem;

    }
    public void onClickNextEvent()
    {
        if (pageEvent < countEvent)
        {
            pageEvent++;
            updatePageEvent();
        }
    }
    public void onClickBackEvent()
    {
        if (pageEvent != 1)
        {
            pageEvent--;
            updatePageEvent();
        }
    }
    public void updatePageEvent()
    {
        pageText.text = "Page: " + pageEvent;
        if (countEvent!=0)
        {
            int c = 1;
            for(int i=1;i<10;i++)
            {
                if (inventoryEvent[i] == 1)
                {
                    listImgItem = listPage[0].GetComponent<Image>();
                    listImgItem.sprite = listAllImgEvent[i];
                    c++;
                    if (c == pageEvent) break;
                }
            }
        }
        else
        {
            listImgItem = listPage[0].GetComponent<Image>();
            listImgItem.sprite = listAllImgEvent[1];
        }

    }
    public void onClickNext()
    {
        if (currentPage * 9 < curentNumberItem)
        {
            currentPage++;
            updatePageItem();
        }
    }
    public void onClickBack()
    {
        if (currentPage != 1)
        {
            currentPage--;
            updatePageItem();
        }
    }
    public void updatePageItem()
    {
        pageText.text = "Page: " + currentPage;
        int i,c=0;
        int firstIndex = (currentPage - 1) * 9;
        for (i = firstIndex; i < firstIndex+9; i++)
        {
            listImgItem = listPage[c++].GetComponent<Image>();
            listImgItem.sprite = currentHaveItem[i];
   
            if(i > curentNumberItem-1)
            {
                listImgItem.sprite = listAllImgItem[24];
            }
        }
    
    }
    
    public static int indexSelected = 0;
    public static int staticCurrentItem;
    public void onClickListItem(int i)
    {
        indexSelected = ((currentPage - 1) * 9) + i;
    }
    int firstcome = 0;
    public void revFomula()
    {
        firstcome = 0;
        formula(2, 4, 7);
        formula(0, 1, 8);
        formula(1, 8, 9);
        formula(7, 7, 10);
        formula(0, 2, 11);
        formula(0, 11, 14);
        formula(9, 14, 23);
        formula(10, 23, 13);
        formula(2, 2, 12);
        formula(41, 1, 24);
        formula(20, 18, 21);
        formula(12, 18, 25);
        formula(16, 1, 27);
        formula(27, 1, 28);
        formula(29, 32, 43);
        formula(6, 18, 31);
        formula(47, 48, 32);
        formula(37, 1, 33);
        formula(29, 42, 34);
        formula(5, 5, 36);
        formula(15, 1, 26);
        formula(16, 19, 35);
        formula(3, 3, 22);
    }
    public void formula(int x,int y,int z)
    {
        if(z == indexAllInventory[SetAllFile.indexSelected])
        {
            Debug.Log("SSSSSSSSSSS" + indexAllInventory[SetAllFile.indexSelected]);
            Image img;
            img = listPage[1].GetComponent<Image>();
            img.sprite = listAllImgItem[x];
            img = listPage[2].GetComponent<Image>();
            img.sprite = listAllImgItem[y];
            firstcome = 1;
        }
        else if (firstcome == 0)
        {
            Image img;
            img = listPage[1].GetComponent<Image>();
            img.sprite = listAllImgItem[24];
            img = listPage[2].GetComponent<Image>();
            img.sprite = listAllImgItem[24];
        }
    }
   
}
