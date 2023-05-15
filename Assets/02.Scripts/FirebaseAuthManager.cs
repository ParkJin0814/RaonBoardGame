using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class FirebaseAuthManager : MonoBehaviour
{
    //로그인, 회원가입 등에 사용
    private FirebaseAuth auth;
    //인증이 완료된 유저 정보
    private FirebaseAuth user;





    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    
    void Create()
    {

    }
}
