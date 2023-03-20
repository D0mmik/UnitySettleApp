using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PaymentData : MonoBehaviour
{
    [SerializeField] TMP_Text PayerNameText;
    [SerializeField] TMP_Text PayerMoneyText;
    [SerializeField] TMP_Text PayerDateText;
    MemberManager memberManager;

    public void StartPaymentCard(string payerNameText, float payerMoney)
    {
        memberManager = FindObjectOfType<MemberManager>();
        PayerNameText.text = $"{payerNameText} paid";
        PayerMoneyText.text = $"{payerMoney}$";
        PayerDateText.text = DateTime.Now.ToString("h:mm:ss   dd.MM.yyyy");
    }

}
