using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePageGuide : MonoBehaviour {
    public Text page;
    int currentPage = 1;
    public GameObject paperBG;
    public GameObject paperRice;
    public GameObject showRice;
    public GameObject showRice2;
    public List<Sprite> listImg;
    public int chaperter;
    Image img,img2;
    // Use this for initialization
    void Start () {
        img = paperBG.GetComponent<Image>();
        img2 = paperRice.GetComponent<Image>();
        // img.sprite = listAllImgEvent[i];
    }
	
	// Update is called once per frame
	void Update () {
        page.text = "Page: " + currentPage;
        if(chaperter == 1)
        {
            if (currentPage == 3) {
                showRice.active = true;
            }
            else
                showRice.active = false;
        }
        img.sprite = listImg[currentPage-1];

    }
    public void nextPage(int maxPage)
    {
        if(currentPage<maxPage)
        currentPage++;
    }
    public void prevPage()
    {
        if (currentPage > 1)
            currentPage--;
    }
    public void onClickAsianRice(int index)
    {
        img2.sprite = listImg[index];
    }
}
