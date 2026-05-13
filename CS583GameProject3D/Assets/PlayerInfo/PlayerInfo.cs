using System.Collections.Generic;

[System.Serializable]
public class PlayerInfo
{
    public static int numFails = 0;
    public static bool firstLoad = true;
    public static bool level2trapTriggered = false;
    public static Dictionary<string, bool> dialogTriggered = new Dictionary<string, bool>();
    public static bool level2WrongButtonTriggered = false;
}
