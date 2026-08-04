using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public static class Constants
{
    public const string CharacterTag = "Character";
    
    public const string LayerBlockWall = "BlockWall";

    public const string AnimatorIdle = "Idle";
    public const string AnimatorRun = "Run";
    public const string AnimatorFall = "Fall";
    public const string AnimatorWin = "Win";
    public const string AnimatorLose = "Lose";
    public static List<string> listTriggerAnimator = new()
    {
        AnimatorIdle,
        AnimatorRun,
        AnimatorFall,
        AnimatorWin,
        AnimatorLose
    };


    public const string LayerDefault = "Default";
    public const string LayerRed = "Red";
    public const string LayerBlue = "Blue";
    public const string LayerGreen = "Green";
    public const string LayerYellow = "Yellow";
    public const string LayerBlack = "Black";
    public static List<String> listColorLayerName = new()
    {
        null, 
        LayerBlue,
        LayerRed,
        LayerYellow,
        LayerGreen,
        LayerBlack
    };
    public const string GAMEDATA = "GAME_DATA";
}