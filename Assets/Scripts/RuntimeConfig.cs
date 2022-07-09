using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RuntimeConfig
{
    public static bool MuzeumOpened;
    public static bool IsBuilding;
    public static bool IsMouseLocked;
    public static double Money = 4000;
    public static int Prestige;
    public static int VisitorsTotal;
    public static int LastVisitors;
    

    public static List<Exhibit> OwnedExhibits = new List<Exhibit>();
    public static List<Exhibit> BuyableExhibits = new List<Exhibit>();

    public static double AdultTicketPrice;
    public static double ChildTicketPrice;
}
