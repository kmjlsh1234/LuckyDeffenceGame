using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserJoinParam
{
    public string email;
    public string password;

    public UserJoinParam(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}
