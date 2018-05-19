using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class Question : MonoBehaviour {

    string json;
    string path;
    int choiceNumber = 3;
    int[] statusQList = new int[3];
    public List<GameObject> bline;
    public List<GameObject> showHint;
    public List<GameObject> closeQ;
    
    // Use this for initialization
    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "question.json"))
        {
            // write json
            path = Application.persistentDataPath + "question.json";
            json = JsonHelper.arrayToJson(statusQList);
            File.WriteAllText(path, json);
        }
        else
        {
            // read json
            path = Application.persistentDataPath + "question.json";
            string f1 = File.ReadAllText(path);
            int[] numbers = JsonHelper.getJsonArray<int>(f1);
            statusQList = numbers;
        }
        for(int i=0; i< choiceNumber; i++)
        {
            if (statusQList[i] == 0) bline[i].active = true;
            if (statusQList[i] == 1) showHint[i].active = true;
            if (statusQList[i] == -1) closeQ[i].active = true;
        }

    }
	// Update is called once per frame
	void Update () {
       
	}

    public void keepQ1(int ans)
    {
        statusQList[0] = ans;
        path = Application.persistentDataPath + "question.json";
        json = JsonHelper.arrayToJson(statusQList);
        File.WriteAllText(path, json);
    }
    public void keepQ2(int ans)
    {
        statusQList[1] = ans;
        path = Application.persistentDataPath + "question.json";
        json = JsonHelper.arrayToJson(statusQList);
        File.WriteAllText(path, json);
    }
    public void keepQ3(int ans)
    {
        statusQList[2] = ans;
        path = Application.persistentDataPath + "question.json";
        json = JsonHelper.arrayToJson(statusQList);
        File.WriteAllText(path, json);
    }
}
