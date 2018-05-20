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
    public Text descriptionText;
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
        else if (mode == 2)
        {
            Image img;
            img = listPage[0].GetComponent<Image>();
            img.sprite = currentHaveItem[SetAllFile.indexSelected];
            revFomula();

            setDescription();
            // indexSelected
            descriptionText.text = ThaiFontAdjuster.Adjust(itemDescription[indexAllInventory[indexSelected]]);
        }
        else if (mode == 3)
        {
            updatePageEvent();
        }
    }
    string[] itemDescription = new string[60];
    void setDescription()
    {
        itemDescription[0] = "ดินเป็นวัตถุที่เกิดขึ้นตามธรรมชาติ มีประโยชน์มากมาย เช่น ใช้สำหรับเพาะปลูก";
        itemDescription[1] = "น้ำเป็นสิ่งจำเป็นสำหรับมนุษย์และสิ่งมีชีวิตอื่น";
        itemDescription[2] = "หญ้าเป็นพืชล้มลุกชนิดหนึ่ง สามารถนำมาใช้เพาะปลูก และเลี้ยงสัตว์ได้";
        itemDescription[3] = "มนุษย์คือสิ่งมีชีวิตชนิดหนึ่ง ซึ่งถูกให้ความหมายว่า สัตว์ที่รู้จักให้เหตุ หรือสัตว์ที่มีจิตใจสูง";
        itemDescription[4] = "ควายเป็นสัตว์เลี้ยงที่ใกล้ชิดกับงานเกษตรกรรมของประเทศแถบเอเชีย ชาวนานิยมเลี้ยงควายเป็นแรงงานเพื่อไว้ไถนา ใช้เป็นพาหนะ กินเป็นอาหาร ควายจึงมีประโยชน์หลายประการ";
        itemDescription[5] = "ไก่เป็นสัตว์ปีกจำพวกนก หากินตามพื้นดิน ตกไข่ก่อนแล้วจึงฟักเป็นตัว ไก่นิยมนำมาประกอบอาหารได้หลากหลาย\nรูปแบบ";
        itemDescription[6] = "วัวเป็นสัตว์เลี้ยงขนาดใหญ่ที่สามัญที่สุด วัวถูกเลี้ยงเป็นปศุสัตว์เพื่อเอาเนื้อ นมและผลิตภัณฑ์นมอื่นๆ ";
        itemDescription[7] = "มูลสัตว์เป็นของเหลือที่เกิดจากระบบทางเดินอาหารของสิ่งมีชีวิต สามรถนำมาเป็นปุ๋ยอินทรีย์ในการเพาะปลูกได้";
        itemDescription[8] = "\tดินเหนียวเป็นดินที่มีเนื้อละเอียด มีการระบายน้ำและอากาศไม่ดีแต่สามารถอุ้มน้ำได้ ดูดยึดและแลกเปลี่ยนธาตุอาหารพืชได้ดี เหมาะที่จะใช้ทำนาปลูกข้าวเพราะเก็บน้ำได้นาน";
        itemDescription[9] = "ทุ่งนาเป็นบริเวณที่นาที่มีการเพาะปลูกข้าว";
        itemDescription[10] = "ปุ๋ย เป็นผลผลิตทางการเกษตรทีเป็นแหล่งอาหารที่ช่วยให้ผลผลิตทางการเกษตรให้สูงขึ้น ใช้สำหรับใส่ลงในดินเพื่อให้ธาตุอาหารแก่พืช";
        itemDescription[11] = "เมล็ดพืชที่มีชีวิตซึ่งเมื่อนำไปปลูก หรือนำไปขยายพันธุ์แล้วจะได้ต้นที่เจริญงอกงามตรงตามพันธุกรรมของพืชนั้น";
        itemDescription[12] = "ต้นไม้คือสิ่งมีชีวิตในธรรมชาติที่สามารถสังเคราะห์อาหารเพื่อตัวเอง ไม้จากต้นไม้เป็นวัสดุที่ใช้ทั่วไปในการก่อสร้างเช่นทำผนังและโครงสร้าง";
        itemDescription[13] = "รวงข้าว หมายถึง ช่อดอกของข้าว ซึ่งเกิดขึ้นที่ข้อของปล้องอันสุดท้าย ของต้นข้าวรวงข้าวจะถูกเก็บเกี่ยวเมื่อถึงฤดูการเก็บเกี่ยวข้าว";
        itemDescription[14] = "ต้นกล้าคือต้นอ่อนของข้าวหรือพืชที่เพาะไว้สำหรับย้ายไปปลูกที่อื่น";
        itemDescription[15] = "ข้าว ที่สีซ้อมเอาเปลือกและรำออกหมดแล้ว นิยมนำมาประกอบอาหารเพื่อการบริโภค";
        itemDescription[16] = "ข้าวสารเหนียวเป็นข้าวที่มีลักษณ์เด่นคือการติดกันเหมือนกาวของเมล็ดข้าวที่สุกแล้ว ปลูกมากทางภาคตะวันออกเฉียงเหนือของประเทศไทยและประเทศลาว";
        itemDescription[17] = "ครกสีข้าวเป็นภูมิปัญญาท้องถิ่นของไทยในการสีข้าว สีด้วยการโยกจากแรงงานคน บางพื้นที่ก็ใช้แรงงานสัตว์เช่นวัวและควาย โดยให้เดินไปรอบ ๆ ครกลาดคัดครกเพื่อให้ครกหมุนสีข้าวเปลือก";
        itemDescription[18] = "มีดเป็นเครื่องมือตัดเฉือนชนิดมีคมสำหรับใช้ สับ หั่น เฉือน ปาด ที่เกี่ยวข้องสัมพันธ์กับแทบทุกกิจกรรมในการดำเนินชีวิต";
        itemDescription[19] = "เป็นเครื่องครัวประเภทหนึ่ง ซึ่งใช้ในการทำอาหารประเภท น้ำพริกส้มตำ หรืออาหารประเภทใดก็ตามที่ต้องการความแหลก";
        itemDescription[20] = "ไผ่เป็นไม้พุ่มชนิดหนึ่ง ผลผลิตจากไผ่ที่สำคัญคือ หน่อไม้ นอกจากนี้ไผ่ยังมีคุณสมบัติทั้งทางด้านความแข็งแรงและความยืดหยุ่น จึงนิยมในการทำเครื่องมือเครื่องใช้หลายประเภท";
        itemDescription[21] = "ไม้ไผ่เป็นสิ่งที่ได้มาจากต้นไผ่สามารถนำมาทำประโยชน์ด้านหลากหลาย เช่น นำมาทำผลิตภัณฑ์หัตถกรรมและอุตสาหกรรม นำมาทำเป็นกระบอกไม้ไผ่ เป็นต้น";
        itemDescription[22] = "ทารก คือ เด็กแบเบาะ หรือ เด็กเล็กๆ โดยทั่วไปหมายถึงเด็กอายุระหว่าง 1 เดือนขึ้นไป จนถึง 1-2 ปี";
        itemDescription[23] = "เมล็ดข้าวที่มีความยาวมากกว่าข้าวหักของแต่ละชั้นคุณภาพ  แต่ไม่ถึงความยาวของข้าวเต็มเมล็ด  และให้รวมถึงเมล็ดข้าวแตกเป็นซีกที่มีเนื้อที่เหลืออยู่ตั้งแต่ 80% ของเมล็ดขึ้นไป";
        itemDescription[24] = "ทองคำเป็นธาตุกลุ่มโลหะชนิดหนึ่ง เป็นธาตุโลหะทรานซิชันสีเหลืองทองมันวาวเนื้ออ่อนนุ่ม สามารถยืดและตีเป็นแผ่นได้";
        itemDescription[25] = "ขอนไม้เป็นส่วนลำต้นที่ถูกตัดจากต้นไม้ สามารถนำมาใช้ประโยชน์ได้หลากหลาย เช่น นำมาใช้ในการก่อสาร้าง นำมาทำยานพาหนะในสมัยโบราณ นำมาทำวัสดุอุปกรณ์";
        itemDescription[26] = "ไข้าวสวย หมายถึงข้าวที่ผ่านกระบวนการนึ่ง หรือการต้มให้เดือดอย่างใดอย่างหนึ่ง ข้าวสวยเป็นอาหารหลักในประเทศในหลายประเทศ";
        itemDescription[27] = "ข้าวเหนียว";
        itemDescription[28] = "ข้าวหลาม ใช้ไผ่ข้าวหลาม หรือไม้ป้างเป็นกระบอกใส่ข้าวหลาม ข้าวหลามแบบชาวบ้าน ใช้ข้าวสารเหนียวกับน้ำเปล่า และเกลือเท่านั้น";
        itemDescription[29] = "ปลาเป็นสัตว์ที่อาศัยอยู่ในแหล่งน้ำ เป็นสัตว์เลือดเย็นหายใจด้วยเหงือกและมีกระดูกสันหลัง ปลาถูกใช้เป็นอาหารหลักและเป็นสัตว์สำคัญทางเศรษฐกิจ";
        itemDescription[30] = "กุ้งเป็นสัตว์น้ำ หายใจด้วยเหงือก ลำตัวแบ่งเป็นปล้องๆ มีเปลือกห่อหุ้ม กุ้งถูกนำมาประกอบอาหารและแปรรูปเพื่อการส่งออก";
        itemDescription[31] = "เนื้อโค หรือ เนื้อวัว จัดว่าเป็นแหล่งของสารอาหารโปรตีนที่มีคุณภาพดีมาเป็นเวลาช้านาน โปรตีนในเนื้อวัวเป็นโปรตีนที่มีค่าทางชีวภาพสูง";
        itemDescription[32] = "เกลือผลิตจากเหมืองเกลือหรือการระเหยน้ำทะเล เกลือจำเป็นต่อชีวิตสัตว์ ความเค็มเป็นรสชาติพื้นฐานของสิ่งมีชีวิตเกลือเป็นเครื่องปรุงรสที่มีที่เก่าแก่ที่สุดและหาง่ายที่สุด";
        itemDescription[33] = "น้ำขิงมีฤทธิ์ร้อน มีสรรพคุณมากมาย เช่น ช่วยทำให้อุณหภูมิในร่างกายเหมาะสม ช่วยให้ขับถ่ายดี บรรเทาอาการของดรคภูมิแพ้เป็นต้น";
        itemDescription[34] = "ปลาทอด เป็นการนำปลามาประกอบอาหารโดยกระบวนการทอด นิยมนำมาประกอบอาหารหลากหลายประเภท ";
        itemDescription[35] = "แป้งเป็นกลุ่มอาหารประเภทคาร์โบไฮเดรต ใช้สำหรับประกอบอาหารหลากหลายชนิด";
        itemDescription[36] = "ไข่เป็น ไซโกตที่เกิดจากการปฏิสนธิของโอวุล มีพัฒนาการเปลือกหรือเมือกหุ้ม ฟักตัวนอกร่างกายของสัตว์ นิยมนำมาใช้ประกอบอาหาร";
        itemDescription[37] = "ขิง เป็นพืชล้มลุก มีเหง้าใต้ดิน เปลือกนอกสีน้ำตาลแกมเหลือง ขิงยิ่งแก่จะยิ่งเผ็ดร้อนและมีใยอาหารมาก ขิงนำมาทำอาหารได้หลากหลาย ขิงอ่อนใช้เป็นผักจิ้ม ใช้ทำผัดขิง ใส่ในยำ";
        itemDescription[38] = "พริกเป็นเครื่องเทศที่สำคัญชนิดหนึ่ง มีคุณสมบัติเป็นยาสมุนไพรด้วย พริกมีวิตามินซี สูง เป็นแหล่งของกรด ascorbic ช่วยร่างกายขับถ่าย ของเสียและนำธาตุอาหารไปยังเนื้อเยื่อของร่างกาย";
        itemDescription[39] = "พริกไทย  เป็นพืชที่ได้รับการยอมรับว่าเป็นราชาของเครื่องเทศที่มีกลิ่นฉุน และเป็นเครื่องเทศที่ให้รสเผ็ดร้อน สามารถนำมาทำพริกไทยแห้งเป็นเครื่องปรุงอาหาร";
        itemDescription[40] = "อบเชย เป็นเครื่องเทศที่มีกลิ่นหอม ได้มาจากเปลือกไม้ชั้นในที่แห้งแล้วของต้นอบเชย นิยมใช้อบเชยในการทำเครื่องแกง";
        itemDescription[41] = "ขมิ้นเป็นพืชล้มลุกในวงศ์ขิง มีถิ่นกำเนิดในเอเชียตะวันออกเฉียงใต้ มีเหง้าอยู่ใต้ดิน มีฤทธิ์ในการฆ่าเชื้อ บรรเทาอาการปวดท้อง ท้องอืด แน่นจุกเสียด แก้โรคผิวหนัง ขับลม แก้ผื่นคัน ท้องร่วง";
        itemDescription[42] = "กระทะเป็นอุปกรณ์ในครัวไว้สำหรับทอด ย่าง และปรุงอาหาร ในสมัยก่อนกระทะสร้างจากเหล็กหล่อ ปัจุบันจะใช้โลหะอย่างเช่น อะลูมิเนียม และเหล็กกล้าไร้สนิม";
        itemDescription[43] = "ปลาร้าเป็นอาหารท้องถิ่นี่สำคัญในเอเชียตะวันออกแยงใต้ โดยแต่ละท้องถิ่นจะมีการทำปลาร้าเป็นเอกลักษณ์ของตน";
        itemDescription[44] = "ลกลักเป็นอาหารประจำชาติกัมพูชา ที่นำเอาเนื้อวัวมาหั่นเป็นชิ้นพอดีคำ ผัดแบบแห้งหรือมีน้ำขลุกขลิกใส่พริกไทยดำ ถือเป็นวัฒนธรรมการกินที่กัมพูชาได้รับมาจากฝรั่งเศส";
        itemDescription[45] = "ลักซาเป็นอาหารประจำชาติสิงคโปร์ มัลักษณะเป็นอาการประเภทก๋วยเตี๋ยว เป็นอาหารดั้งเดิมของชาวเปอรานากัน กลุ่มลูกครึ่งมลายู-จัน";
        itemDescription[46] = "อัมบูยัตเป็นอาหารพื้นเมืองบรูไน เป็นแป้งกวนทำจากต้นสาคูที่เรียกว่าอัมบูลุง มาผสมกับน้ำร้อนจนเหนียว เสิร์ฟร้อนตักกินโดยใช้แท้งไม้ไผ่สองง่าม ม้วนแป้ง จุ่มน้ำจิ้มกินกับเครื่องเคียง";
        itemDescription[47] = "เป็นแหล่งน้ำเค็มขนาดใหญ่ที่ล้อมรอบด้วยพื้นดินทั้งหมดหรือบางส่วน ทะเลบรรเทาภูมิอากาศของโลก และมีบทบาทสำคัญในวัฏจักรน้ำ และวัฏจักรคาร์บอน ";
        itemDescription[48] = "แสงแดดเป็นรังสีแม่เหล็กที่ปล่อยออกจากดวงอาทิตย์ เป็นพลังงานรูปแบบหนึ่งที่มีความสำคัญในการสังเคราะห์ด้วยแสงสร้างชีวมวล (Biomass) และมีส่วนทำให้พืชและสัตว์ดำรงชีวิตอยู่บนโลกได้";
        itemDescription[49] = "เรือ";
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
           // listImgItem.sprite = listAllImgEvent[1];
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
        formula(0, 0, 8);
        formula(1, 8, 9);
        formula(7, 7, 10);
        formula(0, 2, 11);
        formula(0, 11, 14);
        formula(9, 14, 23);
        formula(10, 23, 13);
        formula(2, 2, 12);
        formula(13, 17, 15);
        formula(13, 17, 16);
        formula(1, 41, 24);
        formula(20, 18, 21);
        formula(12, 18, 25);
        formula(1, 16, 27);
        formula(1, 27, 28);
        formula(3, 47, 29);
        formula(3, 47, 30);
        formula(29, 32, 43);
        formula(6, 18, 31);
        formula(47, 48, 32);
        formula(1, 37, 33);
        formula(29, 42, 34);
        formula(5, 5, 36);
        formula(1, 15, 26);
        formula(16, 19, 35);
        formula(3, 3, 22);
        formula(1, 26, 50);
        formula(13, 42, 51);
        formula(26, 36, 55);
        formula(1, 29, 56);
        formula(1, 27, 57);



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
