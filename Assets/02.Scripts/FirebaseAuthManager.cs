using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class FirebaseAuthManager : MonoBehaviour
{
    //�α���, ȸ������ � ���
    private FirebaseAuth auth;
    //������ �Ϸ�� ���� ����
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
