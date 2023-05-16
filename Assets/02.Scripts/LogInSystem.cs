using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogInSystem : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    public TMP_Text outputText;
    
    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangeState;
        FirebaseAuthManager.Instance.Init();
    }

    private void OnChangeState(bool sign)
    {
        outputText.text = sign ? "로그인" : "로그아웃";
        outputText.text += FirebaseAuthManager.Instance.userID;

    }

    public void Create()
    {
        FirebaseAuthManager.Instance.Create(email.text,password.text);
    }

    public void Login()
    {
        FirebaseAuthManager.Instance.Login(email.text, password.text);
    }

    public void Logout() 
    {
        FirebaseAuthManager.Instance.LogOut();
    }
}
