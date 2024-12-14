using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginViewModel
{
    public string email;
    public string password;

    public LoginViewModel(string email, string password) 
    {
        this.email = email;
        this.password = password;
    }

    public LoginViewModel() { }
}
