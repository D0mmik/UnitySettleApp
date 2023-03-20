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
    [SerializeField] TMP_Dropdown UserDropdown;
    [SerializeField] TMP_InputField PaymentBalanceText;
    [SerializeField] TMP_Text AllPayedMoneyText;
    [SerializeField] TMP_Text Datetext;
    public float AllPayedMoney = 0;
    float paymentValue;
    int payers;
    string dropdownValueText;
    [SerializeField] Transform PaymentParent;
    [SerializeField] GameObject PaymentGO;
    public class User
    {
        public string Name { get; set; }
        public float Balance { get; set; }
        
        public float MoneyPayedUser { get; set; }
        public int Id { get; set; }
        public bool Paying { get; set; }
        
    }

    public void Start()
    {
        UserDropdown.value = 0;
        Datetext.text = DateTime.Now.ToString("dd.MM.yyyy");
    }

    public void AddMember()
    {
         tmpBalance = string.IsNullOrEmpty(BalanceText.text) ? 0 : float.Parse(BalanceText.text);

         if (string.IsNullOrEmpty(NameText.text))
         {
             NameText.text = $"User#{Random.Range(0,100)}";
         }
         
         User newUser = new User { Name = NameText.text, Balance = (float)Math.Round(tmpBalance, 2), MoneyPayedUser = 0, Id = Users.Count, Paying = false};
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
        UserDropdown.options.Clear();

        foreach (var user in Users)
        {
            Debug.Log(user.Name + " " + user.Balance + " " + user.Id);
            GameObject tmpUser = Instantiate(UserGO, UsersParent);
            tmpUser.GetComponent<UserData>().StartCard(user.Name,user.Balance, user.MoneyPayedUser ,user.Id, user.Paying);
            UserDropdown.options.Add(new TMP_Dropdown.OptionData(text: user.Name));
        }
    }

    public void RemoveMember(int id)
    {
        Users.RemoveAll(x => x.Id == id);
        UpdateList();
    }

    public void CreatePayment()
    {
        payers = Users.Count(user => user.Paying);
        if(string.IsNullOrEmpty(PaymentBalanceText.text) || payers == 0)
            return;
        paymentValue = float.Parse(PaymentBalanceText.text);
        dropdownValueText = UserDropdown.options[UserDropdown.value].text;

        foreach (var user in Users.Where(user => user.Name == dropdownValueText))
        {
            user.MoneyPayedUser += paymentValue;

            if(user.Paying)
                user.Balance = user.Balance += paymentValue / payers * (payers - 1);
            else
                user.Balance = user.Balance += paymentValue;

            user.Paying = false;
        }
    
        AllPayedMoney += paymentValue;
        AllPayedMoneyText.text = $"Paid in total: {Math.Round(AllPayedMoney, 2).ToString(CultureInfo.InvariantCulture)}$";
        
        foreach (var user in Users.Where(user => user.Paying))
        {
            user.Balance = user.Balance -= paymentValue / payers;
            user.Paying = false;
        }
        UpdateList();

        GameObject tmpPayment = Instantiate(PaymentGO, PaymentParent);
        tmpPayment.GetComponent<PaymentData>().StartPaymentCard(dropdownValueText, paymentValue);
    }

    public void IsPaying(int id)
    {
        foreach (var user in Users.Where(user => user.Id == id))
        {
            user.Paying = true;
        }
    }
    
}
