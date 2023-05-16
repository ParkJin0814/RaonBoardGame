using Firebase.Auth;
using System;
using UnityEngine;

public class FirebaseAuthManager
{
    private static FirebaseAuthManager instance = null;
    public static FirebaseAuthManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FirebaseAuthManager();
            }
            return instance;
        }
    }


    //�α���, ȸ������ � ���
    private FirebaseAuth auth;
    //������ �Ϸ�� ���� ����
    private FirebaseUser user;

    public string userID => user.UserId;

    public Action<bool> LoginState;

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += OnChange;
    }
    private void OnChange(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user != null)
            {
                Debug.Log("�α׾ƿ�");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if(signed)
            {
                Debug.Log("�α���");
                LoginState?.Invoke(true);
            }

        }
    }

    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("ȸ������ ���");
                return;
            }
            if (task.IsFaulted)
            {
                //ȸ������ ���� �̸��� ������ ��й�ȣ ���� �̹̰���
                Debug.LogError("ȸ������ ����");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            Debug.LogError("ȸ������ �Ϸ�");
        });
    }
    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("�α��� ���");
                return;
            }
            if (task.IsFaulted)
            {
                //�α��� ���� �̸��� ������ ��й�ȣ ���� �̹̰���
                Debug.LogError("�α��� ����");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            Debug.LogError("�α��� �Ϸ�");
        });
    }
    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }
}
