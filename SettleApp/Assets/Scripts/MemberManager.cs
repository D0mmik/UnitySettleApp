using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class MemberManager : MonoBehaviour
{
    public List<User> Users = new List<User>();
    public List<User> Payers = new List<User>();
    [SerializeField] GameObject UserGO;
    [SerializeField] Transform UsersParent;
    User userCard;

    [SerializeField] TMP_InputField NameText;
    [SerializeField] TMP_InputField BalanceText;
    float tmpBalance;

    [SerializeField] TMP_InputField PaymentBalanceText;

    [SerializeField] TMP_Text PayedMoneyText;
    float payedMoney;
    public class User
    {
        public string Name { get; set; }
        public float Balance { get; set; }
        public int Id { get; set; }
        public bool Paying { get; set; }
    }


    public void AddMember()
    {
         tmpBalance = string.IsNullOrEmpty(BalanceText.text) ? 0 : float.Parse(BalanceText.text);

         if (string.IsNullOrEmpty(NameText.text))
         {
             NameText.text = $"User#{Random.Range(0,100)}";
         }
         
         User newUser = new User { Name = NameText.text, Balance = (float)Math.Round(tmpBalance, 2), Id = Users.Count, Paying = false};
         Users.Add(newUser);
         UpdateList();
         NameText.text = "";
         BalanceText.text = "";
    }
    public void UpdateList()
    {
        foreach (Transform child in UsersParent) {
            Destroy(child.gameObject);
        }
        
        foreach (var user in Users)
        {
            Debug.Log(user.Name + " " + user.Balance + " " + user.Id);
            GameObject tmpUser = Instantiate(UserGO, UsersParent);
            tmpUser.GetComponent<UserData>().StartCard(user.Name,user.Balance, user.Id, user.Paying);
            Debug.Log("ach");
        }
    }

    public void RemoveMember(int id)
    {
        Users.RemoveAll(x => x.Id == id);
        UpdateList();
    }

    public void CreatePayment()
    {
        Debug.Log("funguje");
        int payers = Users.Count(user => user.Paying);
        foreach (var user in Users.Where(user => user.Paying))
        {
            Debug.Log(user.Name);
            if(string.IsNullOrEmpty(PaymentBalanceText.text)) return;

            float paymentValue = float.Parse(PaymentBalanceText.text);
            user.Balance = user.Balance -= paymentValue / payers;
            payedMoney += paymentValue;
            PayedMoneyText.text = $"Paid in total: {Math.Round(payedMoney, 2).ToString(CultureInfo.InvariantCulture)}$"; 
            user.Paying = false;
        }
        UpdateList();
        //PaymentBalanceText.text = "";
    }

    public void IsPaying(int id)
    {
        foreach (var user in Users.Where(user => user.Id == id))
        {
            user.Paying = true;
        }
    }
}
