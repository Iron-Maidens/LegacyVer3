using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DetEvent : MonoBehaviour {
    public GameObject cardDet;
    public List<Sprite> listImg;

    public List<Sprite> listImg2;
    Image img555;
    int i;
    // Use this for initialization
    void Start () {
  
        string path;
        path = Application.persistentDataPath + "page.json";
        string f = File.ReadAllText(path);
        int[] numbers = JsonHelper.getJsonArray<int>(f);
        i = numbers[0];
        Debug.Log("******"+i);
        showCardDet();
        // img.sprite = listAllImgEvent[i];
    }

    // Update is called once per frame
    void Update () {
		//showCardDet()
	}
    void showCardDet()
    {
        Debug.Log(">>>>>>>" + i);
        img555 = cardDet.GetComponent<Image>();
        img555.sprite = listImg[i-1];
       
    }


}
