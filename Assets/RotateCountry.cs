using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCountry : MonoBehaviour
{
    public Image imgContries;
    void Update()
    {
       // imgContries.transform.Rotate(Vector3.fwd * 2f);
    }
    public void rotateLeft()
    {
        imgContries.transform.Rotate(Vector3.back * 36f);
        if (ManageGame.indexCountry == 0)
            ManageGame.indexCountry = 9;
        else
            ManageGame.indexCountry--;
    }
    public void rotateRight()
    {
        imgContries.transform.Rotate( Vector3.forward * 36f);
        if (ManageGame.indexCountry == 9)
            ManageGame.indexCountry = 0;
        else
            ManageGame.indexCountry++;
    }
}	
