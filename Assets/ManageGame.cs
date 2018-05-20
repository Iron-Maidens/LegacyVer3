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
    Sprite[] currentHaveItem = new Sprite[60];
    int[] chkItemList = new int[60];
    int[] chkEventList = new int[20];
    int eventChk;

    int currentIndex = 0, curentNumberItem = 9;
    int[] indexAllInventory = new int[60];

    int level;

    public Text level_text;
    public Text itemNum_text;

    public Text popUpDescription;
    public Text popUpDescription2;
    public Text relationship;

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
    int itemNumber = 8; // count List
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
    string[] itemDescription = new string[60];


    void Start()
    {
        itemDescription[0] = "ดินเป็นวัตถุที่เกิดขึ้นตามธรรมชาติ มีประโยชน์มากมาย เช่น ใช้สำหรับเพาะปลูก";
        itemDescription[1] = "น้ำเป็นสิ่งจำเป็นสำหรับมนุษย์และสิ่งมีชีวิตอื่น";
        itemDescription[2] = "หญ้าเป็นพืชล้มลุกชนิดหนึ่ง สามารถนำมาใช้เพาะปลูก และเลี้ยงสัตว์ได้";
        itemDescription[3] = "มนุษย์คือสิ่งมีชีวิตชนิดหนึ่ง ซึ่งถูกให้ความหมายว่า สัตว์ที่รู้จักให้เหตุ หรือสัตว์ที่มีจิตใจสูง";
        itemDescription[4] = "ควายเป็นสัตว์เลี้ยงที่ใกล้ชิดกับงานเกษตรกรรมของประเทศแถบเอเชีย ชาวนานิยมเลี้ยงควายเป็นแรงงานเพื่อไว้ไถนา ใช้เป็นพาหนะ กินเป็นอาหาร ควายจึงมีประโยชน์หลายประการ";
        itemDescription[5] = "ไก่เป็นสัตว์ปีกจำพวกนก หากินตามพื้นดิน ตกไข่ก่อนแล้วจึงฟักเป็นตัว ไก่นิยมนำมาประกอบอาหารได้หลากหลาย\nรูปแบบ";
        itemDescription[6] = "วัวเป็นสัตว์เลี้ยงขนาดใหญ่ที่สามัญที่สุด วัวถูกเลี้ยงเป็นปศุสัตว์เพื่อเอาเนื้อ นมและผลิตภัณฑ์นมอื่นๆ ";
        itemDescription[7] = "มูลสัตว์เป็นของเหลือที่เกิดจากระบบทางเดินอาหารของสิ่งมีชีวิต สามรถนำมาเป็นปุ๋ยอินทรีย์ในการเพาะปลูกได้";
        itemDescription[8] = "ดินเหนียวเป็นดินที่มีเนื้อละเอียด มีการระบายน้ำและอากาศไม่ดีแต่สามารถอุ้มน้ำได้ ดูดยึดและแลกเปลี่ยนธาตุอาหารพืชได้ดี เหมาะที่จะใช้ทำนาปลูกข้าวเพราะเก็บน้ำได้นาน";
        itemDescription[9] = "ทุ่งนาเป็นบริเวณที่นาที่มีการเพาะปลูกข้าว";
        itemDescription[10] = "ปุ๋ย เป็นผลผลิตทางการเกษตรทีเป็นแหล่งอาหารที่ช่วยให้ผลผลิตทางการเกษตรให้สูงขึ้น ใช้สำหรับใส่ลงในดินเพื่อให้ธาตุอาหารแก่พืช";
        itemDescription[11] = "เมล็ดพืชที่มีชีวิตซึ่งเมื่อนำไปปลูก หรือนำไปขยายพันธุ์แล้วจะได้ต้นที่เจริญงอกงามตรงตามพันธุกรรมของพืชนั้น";
        itemDescription[12] = "ต้นไม้คือสิ่งมีชีวิตในธรรมชาติที่สามารถสังเคราะห์อาหารเพื่อตัวเอง ไม้จากต้นไม้เป็นวัสดุที่ใช้ทั่วไปในการก่อสร้างเช่นทำผนังและโครงสร้าง";
        itemDescription[13] = "รวงข้าว หมายถึง ช่อดอกของข้าว ซึ่งเกิดขึ้นที่ข้อของปล้องอันสุดท้าย ของต้นข้าวรวงข้าวจะถูกเก็บเกี่ยว เมื่อถึงฤดูการเก็บเกี่ยวข้าว";
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
        itemDescription[27] = "ข้าวเหนียวคือข้าวสารเหนียวที่นำมาผ่านกระบวนการนึ่ง เป็นที่นิยมรับประทานในทางภาคตะวันออกเฉียงเหนือ ภาคเหนือของประเทศไทย และประเทศลาว";
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
        itemDescription[49] = "เป็นยานพาหนะที่ใช้เดินทางทางน้ำ เรือโดยทั่วไปโครงสร้างประกอบด้วยตัวเรือเป็นโครงสร้างที่สามารถลอยน้ำได้ เรือเป็นยานพาหนะสำคัญในการแลกเปลี่ยนค้าขาย";

        itemDescription[50] = "ขนมจีนน้ำยา ถือเป็นอาหารของชาวอาเซียนลุ่มน้ำโขงเพราะมีการกินเส้นขนมจีนเหมือนกัน อาจจะต่างกันที่รสชาติน้ำยา";
        itemDescription[51] = "ขมิ้นเป็นพืชล้มลุกในวงศ์ขิง มีถิ่นกำเนิดในเอเชียตะวันออกเฉียงใต้ มีเหง้าอยู่ใต้ดิน มีฤทธิ์ในการฆ่าเชื้อ บรรเทาอาการปวดท้อง ท้องอืด แน่นจุกเสียด แก้โรคผิวหนัง ขับลม แก้ผื่นคัน ท้องร่วง";
        itemDescription[52] = "กระทะเป็นอุปกรณ์ในครัวไว้สำหรับทอด ย่าง และปรุงอาหาร ในสมัยก่อนกระทะสร้างจากเหล็กหล่อ ปัจุบันจะใช้โลหะอย่างเช่น อะลูมิเนียม และเหล็กกล้าไร้สนิม";
        itemDescription[53] = "ปลาร้าเป็นอาหารท้องถิ่นี่สำคัญในเอเชียตะวันออกแยงใต้ โดยแต่ละท้องถิ่นจะมีการทำปลาร้าเป็นเอกลักษณ์ของตน";
        itemDescription[54] = "กระยาสารท เป็นขนมไทย ทำจากถั่ว งา ข้าวคั่ว และน้ำตาล มักทำกันมากในช่วงสารทไทย กระยาสารทนี้เนื่องมาจาก ข้าวมธุปายาส ซึ่งเป็นอาหารอินเดียใช้ข้าว น้ำตาล น้ำนม ผสมกัน";
        itemDescription[55] = "ข้าวจี่ เป็นขนมพื้นบ้านของ สปป.ลาว และไทย ทำจากข้าวเหนียวนึ่งทาเกลือ ปั้นเป็นรูปกลมหรือรีเสียบ ไม้นำไปย่างบนเตาถ่าน";
        itemDescription[56] = "น้ำปลาเป็นเครื่องปรุงรสอย่างหนึ่งที่มีประจำทุกครัวเรือนของไทยและอีกหลายๆประเทศในเอเชีย";
        itemDescription[57] = "ข้าวหมาก อาหารหมักชนิดหนึ่งที่นำข้าวเหนียวมาหมัก ซึ่งเป็นการถนอมอาหารวิธีหนึ่ง เนื่องจากยีสต์จะทำหน้าที่เปลี่ยนน้ำตาลให้เป็นเอทิลแอลกอฮอล์ช่วยชะลอการเน่าเสียของอาหาร";
        itemDescription[58] = "กวนข้าวทิพย์ Htamanè เป็นงานบุญเดือนสิบเอ็ด เรียกว่าเดือนดะโบ๊ะดแว (กลาง ม.ค.- กลาง ก.พ.) คนโบราณเชื่อว่าข้าวทิพย์เป็นอาหารวิเศษ ผู้มีวาสนาได้รับประทานจะพ้นจากโรคภัย ร่างกาย แข็งแรง มีสติปัญญาดี เป็นสิริมงคลแก่ชีวิต";
        itemDescription[59] = "ข้าวหุงกับดอกอัญชันสด มักใช้ทานกับผักสด มีน้ำบูดูที่ได้จากปลาหมัก คล้ายปลาร้าของทางภาคอีสาน แต่กรรมวิธีต่างกัน เคี่ยวกับสมุนไพร พวกข่า ตะไคร้ และใบมะกรูด เยอะๆ เพื่อดับกลิ่น และช่วยให้น้ำบูดูหอมยิ่งขึ้น";
        
        // string f = File.ReadAllText(path);
        // int[] numbers = JsonHelper.getJsonArray<int>(f);
        eventChk = 0;

        paneRecieveItem.active = false;
        paneRecieveItem2.active = false;
        paneUpLevel.active = false;
        for (int i = 0; i < 15; i++)
            eventPane[i].active = false;
        image1 = item1.GetComponent<Image>();
        image2 = item2.GetComponent<Image>();

        if (!File.Exists(Application.persistentDataPath + "list.json"))
        {
            for (int i = 0; i < itemNumber; i++)
            {
                listImgItem = listItem[i].GetComponent<Image>();

                if (i > 6)
                {
                    listImgItem.sprite = listAllImgItem[47];
                }
                else
                {
                    listImgItem.sprite = listAllImgItem[i];
                    currentHaveItem[i] = listAllImgItem[i];
                    indexAllInventory[i] = i;
                }

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

            Debug.Log(Application.persistentDataPath);
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

            for (int i = 0; i < itemNumber; i++)
            {
                listImgItem = listItem[i].GetComponent<Image>();
                if (i > 6)
                {
                    listImgItem.sprite = listAllImgItem[47];
                }
                else
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
        relationship.text = relation[indexCountry] + "";
        imgRelation.sprite = levelRelation[relation[indexCountry]];

        //chkEventPopup();

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
        for (int i = currentIndex; i < currentIndex + itemNumber; i++)
        {
            listImgItem = listItem[count++].GetComponent<Image>();
            listImgItem.sprite = currentHaveItem[i];
        }
    }

    public void downList()
    {
        if (itemNumber + currentIndex < curentNumberItem) currentIndex++;
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
        formula(0, 0, 8);  
        formula(1, 8, 9);
        formula(7, 7, 10);
        formula(0, 2, 11);
        formula(0, 11, 14);
        formula(9, 14, 23);
        formula(10, 23, 13);
        formula(2, 2, 12);
        formula2(13, 17, 15, 16);
        formula(1, 41, 24);
        formula(20, 18, 21);
        formula(12, 18, 25);
        formula(1, 16, 27);
        formula(1, 27, 28);
        formula2(3, 47, 29, 30);
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
                chkEventPopup();
                chkEventLvUp();


                //pop up item
                paneRecieveItem.active = true;
                imgReceivedItem.sprite = listAllImgItem[z];
                popUpDescription.text = ThaiFontAdjuster.Adjust(itemDescription[z]);

                

                //write json
                path = Application.persistentDataPath + "list.json";
                pathCurrentIndexitem = Application.persistentDataPath + "currentIndex.json";

                json = JsonHelper.arrayToJson(indexAllInventory);
                File.WriteAllText(path, json);

                curentNumberItemJson[0] = curentNumberItem;
                curentNumberItemJson[1] = level;
                jsonCurrentIndexitem = JsonHelper.arrayToJson(curentNumberItemJson);
                File.WriteAllText(pathCurrentIndexitem, jsonCurrentIndexitem);

                setNullItem(1);
                setNullItem(2);
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
                popUpDescription.text = ThaiFontAdjuster.Adjust(itemDescription[z]);

                Debug.Log("555555555555");
                paneRecieveItem.active = true;
                imgReceivedItem.sprite = listAllImgItem[z];
         
                //pop up item
            }

        }

        if (chkItemList[k] == 0)
        {
            if (combineItem1 == x && combineItem2 == y)
            {
                currentHaveItem[curentNumberItem] = listAllImgItem[k];
                chkItemList[k] = 1;
                indexAllInventory[curentNumberItem++] = k;
                popUpDescription2.text = ThaiFontAdjuster.Adjust(itemDescription[k]);

                paneRecieveItem2.active = true;
                imgReceivedItem2.sprite = listAllImgItem[k];
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

    public void chkEventPopup()
    {
        if (chkEventList[0] == 0 && chkItemList[13] == 1)
        {
            eventChk = 1;
            //ทุกประเทศ
            for (int i = 0; i < 10; i++)
            {
                if (relation[i] < 3) relation[i] += 1;
            }
            chkEventList[0] = 1;
            //pop up event 0 have rice
            eventPane[0].active = true;
        }

        if (chkEventList[1] == 0 && chkItemList[15] == 1)
        {
            eventChk = 1;
            //เวียดนาม พม่า กัมพูชา
            relation[0] += 1;
            relation[2] += 1;
            relation[6] += 1;
            chkEventList[1] = 1;
            //pop up event 1 แรกนา
            eventPane[1].active = true;
        }

        if (chkEventList[2] == 0 && chkItemList[50] == 1)
        {
            eventChk = 1;
            //พม่า ลาว อินโด เวียดนาม กัมพูชา
            relation[0] += 1;
            relation[2] += 1;
            relation[3] += 1;
            relation[5] += 1;
            relation[6] += 1;
            chkEventList[2] = 1;
            //pop up event 1 ขนมจีน
            eventPane[2].active = true;
        }

        //กัมพูชา 3+ มีข้าวตอก ถั่วลิสง น้ำตาล
        if (chkEventList[3] == 0 && relation[6] == 3 && chkItemList[51] == 1 && chkItemList[52] == 1 && chkItemList[53] == 1)
        {
            eventChk = 1;
            //add กระยาสารท
            currentHaveItem[curentNumberItem] = listAllImgItem[54];
            chkItemList[54] = 1;
            indexAllInventory[curentNumberItem++] = 54;

            chkEventList[3] = 1;
            //pop up event 3 กระยาสารท
            eventPane[3].active = true;
        }

        if (chkEventList[4] == 0 && chkItemList[55] == 1)
        {
            eventChk = 1;
            //เวียดนาม ลาว
            relation[0] += 1;
            relation[5] += 1;

            chkEventList[4] = 1;
            //pop up event 4 ข้าวจี่
            eventPane[4].active = true;
        }

        //พม่า
        if (chkEventList[5] == 0 && relation[2] == 3)
        {
            eventChk = 1;
            currentHaveItem[curentNumberItem] = listAllImgItem[58];
            chkItemList[58] = 1;
            indexAllInventory[curentNumberItem++] = 58;

            chkEventList[5] = 1;
            //pop up event 5 ข้าวมธุปายาท
            eventPane[5].active = true;
        }

        if (chkEventList[6] == 0 && chkItemList[57] == 1)
        {
            eventChk = 1;
            //เวียดนาม ลาว
            relation[1] += 1;
            relation[5] += 1;
            relation[6] += 1;
            relation[9] += 1;

            chkEventList[6] = 1;
            //pop up event 6 ข้าวหมาก
            eventPane[6].active = true;
        }

        if (chkEventList[7] == 0 && chkItemList[28] == 1)
        {
            eventChk = 1;
            //เวียดนาม ลาว
            relation[3] += 1;
            relation[0] += 1;
            relation[7] += 1;
            relation[1] += 1;

            chkEventList[7] = 1;
            //pop up event 7 ข้าวหลาม
            eventPane[7].active = true;
        }

        if (chkEventList[8] == 0 && relation[7] == 3)
        {
            eventChk = 1;
            currentHaveItem[curentNumberItem] = listAllImgItem[59];
            chkItemList[59] = 1;
            indexAllInventory[curentNumberItem++] = 59;

            chkEventList[8] = 1;
            //pop up event 5 นาชิกเกอราบูซัมติง
            eventPane[8].active = true;
        }

        if (chkEventList[9] == 0 && chkItemList[56] == 1)
        {
            eventChk = 1;
            //เวียดนาม ลาว
            relation[9] += 1;
            relation[0] += 1;
            relation[2] += 1;
            relation[5] += 1;
            relation[6] += 1;

            chkEventList[9] = 1;
            //pop up event 9 น้ำปลา
            eventPane[9].active = true;
        }

        if (chkEventList[10] == 0 && chkItemList[43] == 1)
        {
            eventChk = 1;
            //เวียดนาม ลาว
            relation[9] += 1;
            relation[0] += 1;
            relation[5] += 1;
            relation[2] += 1;
            relation[3] += 1;
            relation[6] += 1;
            relation[7] += 1;

            chkEventList[10] = 1;
            //pop up event 10 ปลาร้า
            eventPane[10].active = true;
        }

        if (chkEventList[11] == 0 && level == 3 && chkItemList[26] == 1)
        {
            eventChk = 1;
            //เวียดนาม ลาว

            chkEventList[11] = 1;
            //pop up event 11 พระแม่โพสพ
            eventPane[11].active = true;
        }

        if (chkEventList[12] == 0 && relation[3] == 3)
        {
            eventChk = 1;
            currentHaveItem[curentNumberItem] = listAllImgItem[49];
            chkItemList[49] = 1;
            indexAllInventory[curentNumberItem++] = 49;

            chkEventList[12] = 1;
            //pop up event 12 เรือ
            eventPane[12].active = true;
        }
    }

    public void chkEventLvUp()
    {
        // level up 1
        if (level == 1 && chkEventList[0] == 1)
        {
            eventChk = 1;
            for (int i = 17; i < 21; i++)
            {
                currentHaveItem[curentNumberItem] = listAllImgItem[i];
                chkItemList[i] = 1;
                indexAllInventory[curentNumberItem++] = i;
            }

            for (int i = 52; i < 54; i++)
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
        if (level == 2 && chkEventList[49] == 1)
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


