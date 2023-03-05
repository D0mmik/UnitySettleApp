using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] Windows;

    void Start()
    {
        //CloseWindows();
    }

    void CloseWindows()
    {
        foreach( var item in Windows) item.SetActive(false);
    }
    
    void ToggleWindow(string nameOfWindow)
    {
        CloseWindows();
        Windows.Single((x)=> x.name == nameOfWindow).SetActive(true);
    }

    public void AddUser()
    {
        ToggleWindow("AddUserWindow");
    }

    public void AddPayment()
    {
        ToggleWindow("PaymentWindow");
    }

    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
