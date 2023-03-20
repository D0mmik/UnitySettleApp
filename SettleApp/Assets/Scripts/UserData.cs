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
    [SerializeField] TMP_Text MoneyPayed;
    [SerializeField]int UserId;
    MemberManager memberManager;
    [SerializeField] bool Paying;
    [SerializeField] Image PayingImage;
    [SerializeField] TMP_Text UserIDText;
    public void StartCard(string userName, float balance, float moneyPayed, int id, bool paying)
    {
        memberManager = FindObjectOfType<MemberManager>();
        Name.text = userName;
        Balance.text = $"{Math.Round(balance, 2)}$";
        Debug.Log(balance);
        UserId = id;
        UserIDText.text = UserId.ToString();
        Paying = paying;
        MoneyPayed.text = $"Money payed: {Math.Round(moneyPayed, 2)}$";
    }

    public void RemoveUser()
    {
        memberManager.RemoveMember(UserId);
    }

    public void PayingUser()
    {
        memberManager.IsPaying(UserId);
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
