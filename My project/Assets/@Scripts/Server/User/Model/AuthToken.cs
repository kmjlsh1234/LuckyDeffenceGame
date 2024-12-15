using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthToken
{
    public string jwtToken;
    public string refreshToken;

    public AuthToken(string jwtToken, string refreshToken)
    {
        this.jwtToken = jwtToken;
        this.refreshToken = refreshToken;
    }

    public AuthToken() { }
}
