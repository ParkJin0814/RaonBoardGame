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


    //로그인, 회원가입 등에 사용
    private FirebaseAuth auth;
    //인증이 완료된 유저 정보
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
                Debug.Log("로그아웃");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if(signed)
            {
                Debug.Log("로그인");
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
                Debug.LogError("회원가입 취소");
                return;
            }
            if (task.IsFaulted)
            {
                //회원가입 실패 이메일 비정상 비밀번호 간단 이미가입
                Debug.LogError("회원가입 실패");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            Debug.LogError("회원가입 완료");
        });
    }
    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                //로그인 실패 이메일 비정상 비밀번호 간단 이미가입
                Debug.LogError("로그인 실패");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            Debug.LogError("로그인 완료");
        });
    }
    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("로그아웃");
    }
}
