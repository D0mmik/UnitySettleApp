using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserData : MonoBehaviour
{
    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text Balance;
    [SerializeField]int UserId;
    MemberManager memberManager;
    [SerializeField] bool Paying;
    [SerializeField] Image PayingImage;
    public void StartCard(string userName, float balance, int id, bool paying)
    {
        Name.text = userName;
        Balance.text = $"{Math.Round(balance, 2)}$";
        Debug.Log(balance);
        UserId = id;
        Paying = paying;
    }

    public void RemoveUser()
    {
        FindObjectOfType<MemberManager>().RemoveMember(UserId);
    }

    public void PayingUser()
    {
        FindObjectOfType<MemberManager>().IsPaying(UserId);
    }

    public void PayingColor()
    {
        switch (Paying)
        {
            case true:
                PayingImage.color = Color.white;
                Paying = false;
                break;
            case false:
                PayingImage.color = Color.green;
                Paying = true;
                break;
        }
    }
}
