using frame8.ScrollRectItemsAdapter.Classic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ManageGame : MonoBehaviour
{

    public Sprite nullImg;
    public GameObject item1;
    public GameObject item2;
    public List<GameObject> listItem;
    public List<Sprite> listAllImgItem;
    Sprite[] currentHaveItem = new Sprite[53];
    int[] chkItemList = new int[53];
    int[] chkEventList = new int[20];
    int eventChk;

    int currentIndex = 0, curentNumberItem = 9;
    int[] indexAllInventory = new int[53];

    int level;

    public Text level_text;
    public Text itemNum_text;

    int[] relation = new int[10];
    public List<string> countries;
    public Text countryText;
    public List<Sprite> levelRelation;
    public Image imgRelation;

    public GameObject paneRecieveItem;
    public GameObject paneRecieveItem2;
    public Image imgReceivedItem2;
    public Image imgReceivedItem2r;
    public Image imgReceivedItem;

    public List<GameObject> eventPane;
    public GameObject paneUpLevel;
    static public int indexCountry;

    Image image1, image2, listImgItem;
    bool statusItem1 = false;
    bool statusItem2 = false;

    // json var
    string json;
    string path;
    string jsonCurrentIndexitem;
    string pathCurrentIndexitem;
    string jsonCurrentIndexAllinventory;
    string pathCurrentIndexAllinventory;
    string jsonChkEventList;
    string pathChkEventList;
    string jsonRelation;
    string pathRelation;

    void Start()
    {

        // string f = File.ReadAllText(path);
        // int[] numbers = JsonHelper.getJsonArray<int>(f);
        eventChk = 0;

        paneRecieveItem.active = false;
        paneRecieveItem2.active = false;
        paneUpLevel.active = false;
        for (int i = 0; i < 7; i++)
            eventPane[i].active = false;
        image1 = item1.GetComponent<Image>();
        image2 = item2.GetComponent<Image>();

        if (!File.Exists(Application.persistentDataPath + "list.json"))
        {
            for (int i = 0; i < 6; i++)
            {
                listImgItem = listItem[i].GetComponent<Image>();
                listImgItem.sprite = listAllImgItem[i];
                currentHaveItem[i] = listAllImgItem[i];
                indexAllInventory[i] = i;
            }
            currentHaveItem[6] = listAllImgItem[6];
            currentHaveItem[7] = listAllImgItem[47];
            currentHaveItem[8] = listAllImgItem[48];
            indexAllInventory[6] = 6;
            indexAllInventory[7] = 47;
            indexAllInventory[8] = 48;
            level = 1;

            pathChkEventList = Application.persistentDataPath + "chkEventList.json";
            pathRelation = Application.persistentDataPath + "relation.json";

            json = JsonHelper.arrayToJson(chkEventList);
            File.WriteAllText(pathChkEventList, json);

            json = JsonHelper.arrayToJson(relation);
            File.WriteAllText(pathRelation, json);

            path = Application.persistentDataPath + "list.json";
            pathCurrentIndexitem = Application.persistentDataPath + "currentIndex.json";

            json = JsonHelper.arrayToJson(indexAllInventory);
            File.WriteAllText(path, json);

            curentNumberItemJson[0] = curentNumberItem;
            curentNumberItemJson[1] = level;
            jsonCurrentIndexitem = JsonHelper.arrayToJson(curentNumberItemJson);
            File.WriteAllText(pathCurrentIndexitem, jsonCurrentIndexitem);

        }
        else
        {
            //read json
            path = Application.persistentDataPath + "list.json";
            pathCurrentIndexitem = Application.persistentDataPath + "currentIndex.json";
            pathChkEventList = Application.persistentDataPath + "chkEventList.json";
            pathRelation = Application.persistentDataPath + "relation.json";


            string f1 = File.ReadAllText(path);
            string f2 = File.ReadAllText(pathCurrentIndexitem);
            string f3 = File.ReadAllText(pathChkEventList);
            string f4 = File.ReadAllText(pathRelation);

            int[] numbers = JsonHelper.getJsonArray<int>(f2);
            curentNumberItem = numbers[0];
            level = numbers[1];

            numbers = JsonHelper.getJsonArray<int>(f1);
            for (int i = 0; i < 53; i++)
            {
                indexAllInventory[i] = numbers[i];
                currentHaveItem[i] = listAllImgItem[numbers[i]];
            }

            for (int i = 0; i < 6; i++)
            {
                listImgItem = listItem[i].GetComponent<Image>();
                listImgItem.sprite = listAllImgItem[i];
            }

            numbers = JsonHelper.getJsonArray<int>(f3);
            for (int i = 0; i < 20; i++)
            {
                chkEventList[i] = numbers[i];
            }

            numbers = JsonHelper.getJsonArray<int>(f4);
            for (int i = 0; i < 10; i++)
            {
                relation[i] = numbers[i];
            }
        }

        for (int i = 0; i < 53; i++)
        {
            if (indexAllInventory[i] != 0)
            {
                chkItemList[indexAllInventory[i]] = 1;
            }

        }
        indexCountry = 0;

    }

    // Update is called once per frame
    void Update()
    {
        level_text.text = "Level " + level;
        itemNum_text.text = "Item " + curentNumberItem + " / 50";
        countryText.text = countries[indexCountry];
        imgRelation.sprite = levelRelation[relation[indexCountry]];

        // level up 1
        if (level == 1 && chkEventList[2] == 1)
        {
            eventChk = 1;
            for (int i = 17; i < 21; i++)
            {
                currentHaveItem[curentNumberItem] = listAllImgItem[i];
                chkItemList[i] = 1;
                indexAllInventory[curentNumberItem++] = i;
            }

            level += 1;
            //pop up event
            paneUpLevel.active = true;


        }
        // level up 2

        if (level == 2 && relation[6] >= 3)
        {
            eventChk = 1;
            for (int i = 37; i < 42; i++)
            {
                currentHaveItem[curentNumberItem] = listAllImgItem[i];
                chkItemList[i] = 1;
                indexAllInventory[curentNumberItem++] = i;
            }

            level += 1;
            //pop up event
            paneUpLevel.active = true;

        }
       
        if (chkEventList[0] == 0 && chkItemList[31] == 1 && chkItemList[39] == 1 && relation[6] >= 3)
        {
            eventChk = 1;
            currentHaveItem[curentNumberItem] = listAllImgItem[44];
            chkItemList[44] = 1;
            indexAllInventory[curentNumberItem++] = 44;

            chkEventList[0] = 1;

            //pop up event 0
            eventPane[0].active = true;
        }

        if (chkEventList[1] == 0 && chkItemList[30] == 1 && chkItemList[39] == 1 && chkItemList[41] == 1 && relation[8] >= 3)
        {
            eventChk = 1;
            currentHaveItem[curentNumberItem] = listAllImgItem[45];
            chkItemList[45] = 1;
            indexAllInventory[curentNumberItem++] = 45;
            chkEventList[1] = 1;

            //pop up event 1
            eventPane[1].active = true;
        }

        if (chkEventList[2] == 0 && chkItemList[13] == 1)
        {
            eventChk = 1;
            for (int i = 0; i < 10; i++)
            {
                if (relation[i] < 3) relation[i] += 1;
            }
            chkEventList[2] = 1;
            //pop up event 2 rice
            eventPane[2].active = true;
        }

        if (chkEventList[3] == 0 && chkItemList[43] == 1)
        {
            eventChk = 1;
            if (relation[2] < 3) relation[2] += 1;
            if (relation[6] < 3) relation[6] += 1;
            chkEventList[3] = 1;
            //pop up event 3 pra ra
            eventPane[3].active = true;
        }

        if (chkEventList[4] == 0 && chkItemList[34] == 1 && chkItemList[35] == 1 && relation[1] == 3)
        {
            eventChk = 1;
            currentHaveItem[curentNumberItem] = listAllImgItem[46];
            chkItemList[46] = 1;
            indexAllInventory[curentNumberItem++] = 46;

            chkEventList[4] = 1;
            //pop up event 4
            eventPane[4].active = true;
        }

        if (chkEventList[5] == 0 && chkItemList[43] == 1 && false)
        {
            eventChk = 1;
            if (relation[0] < 3) relation[0] += 1;
            //pop up event 5 corpse
            chkEventList[5] = 1;
            eventPane[5].active = true;
        }

        if (chkEventList[6] == 0 && chkItemList[4] == 1 && chkItemList[15] == 1)
        {
            eventChk = 1;
            if (relation[6] < 3) relation[6] += 1;
            //pop up event 5 buffalo
            chkEventList[6] = 1;
            eventPane[6].active = true;
        }

        if (eventChk == 1)
        {
            //write json
            pathChkEventList = Application.persistentDataPath + "chkEventList.json";
            pathRelation = Application.persistentDataPath + "relation.json";

            json = JsonHelper.arrayToJson(chkEventList);
            File.WriteAllText(pathChkEventList, json);

            json = JsonHelper.arrayToJson(relation);
            File.WriteAllText(pathRelation, json);

            path = Application.persistentDataPath + "list.json";
            pathCurrentIndexitem = Application.persistentDataPath + "currentIndex.json";

            json = JsonHelper.arrayToJson(indexAllInventory);
            File.WriteAllText(path, json);

            curentNumberItemJson[0] = curentNumberItem;
            curentNumberItemJson[1] = level;
            jsonCurrentIndexitem = JsonHelper.arrayToJson(curentNumberItemJson);
            File.WriteAllText(pathCurrentIndexitem, jsonCurrentIndexitem);

            eventChk = 0;
        }

    }

    public void upDateList()
    {
        int count = 0;
        for (int i = currentIndex; i < currentIndex + 6; i++)
        {
            listImgItem = listItem[count++].GetComponent<Image>();
            listImgItem.sprite = currentHaveItem[i];
        }
    }

    public void downList()
    {
        if (6 + currentIndex < curentNumberItem) currentIndex++;
        upDateList();
    }

    public void upList()
    {
        if (currentIndex > 0) currentIndex--;
        upDateList();
    }

    public void setNullItem(int i)
    {
        if (i == 1)
        {
            image1.sprite = nullImg;
            statusItem1 = false;
        }
        if (i == 2)
        {
            image2.sprite = nullImg;
            statusItem2 = false;
        }
    }

    int combineItem1, combineItem2;
    public void insertItem(int i)
    {
        if (!statusItem1)
        {
            image1.sprite = currentHaveItem[i];
            combineItem1 = indexAllInventory[i];
            statusItem1 = true;
        }
        else if (!statusItem2)
        {
            image2.sprite = currentHaveItem[i];
            combineItem2 = indexAllInventory[i];
            statusItem2 = true;
            combineItem();

        }
    }
    public void combineItem()
    {

        int swip;
        if (combineItem2 < combineItem1)
        {
            swip = combineItem2;
            combineItem2 = combineItem1;
            combineItem1 = swip;
        }
        for (int u = 0; u < 15; u++)
        {
            // Debug.Log(u + " " + chkItemList[u] + "\n");
        }
        formula(2, 4, 7);
        formula(0, 1, 8);
        formula(1, 8, 9);
        formula(7, 7, 10);
        formula(0, 2, 11);
        formula(0, 11, 14);
        formula(9, 14, 23);
        formula(10, 23, 13);
        formula(2, 2, 12);
        formula2(13, 17, 15, 16);
        formula(41, 1, 24);
        formula(20, 18, 21);
        formula(12, 18, 25);
        formula(16, 1, 27);
        formula(27, 1, 28);
        formula2(3, 47, 29, 30);
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
    int[] curentNumberItemJson = new int[2];
    public void formula(int x, int y, int z)
    {

        if (chkItemList[z] == 0)
        {
            if (combineItem1 == x && combineItem2 == y)
            {
                currentHaveItem[curentNumberItem] = listAllImgItem[z];
                chkItemList[z] = 1;
                indexAllInventory[curentNumberItem++] = z;
                paneRecieveItem.active = true;
                imgReceivedItem.sprite = listAllImgItem[z];

                //write json
                path = Application.persistentDataPath + "list.json";
                pathCurrentIndexitem = Application.persistentDataPath + "currentIndex.json";

                json = JsonHelper.arrayToJson(indexAllInventory);
                File.WriteAllText(path, json);

                curentNumberItemJson[0] = curentNumberItem;
                curentNumberItemJson[1] = level;
                jsonCurrentIndexitem = JsonHelper.arrayToJson(curentNumberItemJson);
                File.WriteAllText(pathCurrentIndexitem, jsonCurrentIndexitem);

            }
        }
    }

    public void formula2(int x, int y, int z, int k)
    {

        if (chkItemList[z] == 0)
        {
            if (combineItem1 == x && combineItem2 == y)
            {
                currentHaveItem[curentNumberItem] = listAllImgItem[z];
                chkItemList[z] = 1;
                indexAllInventory[curentNumberItem++] = z;
                Debug.Log("555555555555");
                paneRecieveItem2.active = true;
                imgReceivedItem2.sprite = listAllImgItem[z];
                imgReceivedItem2r.sprite = listAllImgItem[k];
            }
           
        }

        if (chkItemList[k] == 0)
        {
            if (combineItem1 == x && combineItem2 == y)
            {
                currentHaveItem[curentNumberItem] = listAllImgItem[k];
                chkItemList[k] = 1;
                indexAllInventory[curentNumberItem++] = k;
            }
        }

        //write json
        path = Application.persistentDataPath + "list.json";
        pathCurrentIndexitem = Application.persistentDataPath + "currentIndex.json";

        json = JsonHelper.arrayToJson(indexAllInventory);
        File.WriteAllText(path, json);

        curentNumberItemJson[0] = curentNumberItem;
        curentNumberItemJson[1] = level;
        jsonCurrentIndexitem = JsonHelper.arrayToJson(curentNumberItemJson);
        File.WriteAllText(pathCurrentIndexitem, jsonCurrentIndexitem);

    }

    public void onClickList(int indexList)
    {
        insertItem(indexList + currentIndex);
    }

}


public class JsonHelper
{
    //Usage:
    //YouObject[] objects = JsonHelper.getJsonArray<YouObject> (jsonString);
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = json;
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    //Usage:
    //string jsonString = JsonHelper.arrayToJson<YouObject>(objects);
    public static string arrayToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}


