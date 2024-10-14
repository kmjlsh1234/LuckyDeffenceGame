using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enum
{
    public enum SoundType
    {
        SFX = 0,
        BGM = 1,
    }

    public enum SceneName
    {
        SplashScene = 0,
        MainScene = 1,
        GameScene = 2,
    }

    public enum UIType
    {
        UIPopupSplash,
        UIPopupMain,
        UIPopupGame,
    }

    public enum MapType
    {
        Map_City = 0,

    }

    public enum HeroType
    {
        Human = 0,
        Devil =1,
        Elf = 2,
        Skeleton = 3,
    }

    public enum Grade
    {
        Normal = 0,
        Rare = 1,
        SuperRare = 2,
        Epic = 3,
        Legend = 4,
    }

    public enum AttackType
    {
        MELEE = 0, //근거리
        RANGE = 1, //원거리
        AREA = 2, //범위형
    }

    public enum AnimationType
    {
        Idle,
        Run,
        Attack,
        Death,
    }
}
