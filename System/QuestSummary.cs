using UnityEditor;
using UnityEngine;

public class QuestSummary : MonoBehaviour
{
    public static string[] title = { "", "Mask Dude's request" };
    public static string[] description = { "", "Make Dude has a trouble with ladybugs. I can kill 30 ladybugs for him." };
    public static int[] progress = { 0, 0 };
    public static int[] goal = { 0, 30 };
    public static int[] reward = { 0, 100 };
    public static string[] item = { "", "key" };

    public static void Complete(int n)
    {
        PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + reward[n]);
        if (item[n] != "")
        {

        }
    }

}