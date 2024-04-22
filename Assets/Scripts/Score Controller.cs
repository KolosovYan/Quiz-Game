using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static int Score = 0;

    public static void AddScore() => Score++;

    public static void ResetScore() => Score = 0;
}
