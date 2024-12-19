using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorCodeMessage
{
    SUCCESS = 0, //성공

    //AUTH(1000XX)
    USER_NOT_EXIST = 100001, //유저가 존재하지 않음

    //SERVER ERROR(5000XX)
    INTERNAL_SERVER_ERROR = 500000 //서버 내부 에러

}
