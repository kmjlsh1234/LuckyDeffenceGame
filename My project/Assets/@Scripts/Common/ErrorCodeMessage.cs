using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorCodeMessage
{
    SUCCESS = 0, //����

    //AUTH(1000XX)
    USER_NOT_EXIST = 100001, //������ �������� ����

    //SERVER ERROR(5000XX)
    INTERNAL_SERVER_ERROR = 500000 //���� ���� ����

}
