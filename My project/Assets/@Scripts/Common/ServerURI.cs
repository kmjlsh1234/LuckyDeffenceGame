using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServerURI
{
    //AUTH
    public const string AUTH_LOGIN_REQUEST = "/auth/login";
    public const string AUTH_JOIN_REQUEST = "/api/v1/user/join";
    public const string AUTH_TOKEN_REFRESH = "/api/v1/token/refresh";
    //PROFILE
    public const string GET_PROFILE_REQUEST = "/api/v1/profile";
    public const string ADD_PROFILE_REQUEST = "/api/v1/profile";
    //CURRENCY
    public const string GET_GOLD_REQUEST = "/api/v1/golds";
    //STAT
    public const string GET_STAT_REQUEST = "/api/v1/stats";

}
