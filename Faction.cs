using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction {

    public enum Factions { None, Environment, Player, Enemy, Drone };

    private List<Factions> factionList;

    public Faction(List<Factions> factionList)
    {
        factionList = new List<Factions>(factionList);
    }

    public static bool isEnemy(Factions f1, Factions f2)
    {
        if (f1 == Factions.Player && f2 == Factions.Enemy) return true;
        if (f1 == Factions.Player && f2 == Factions.Drone) return true;
        if (f1 == Factions.Enemy && f2 == Factions.Drone) return true;
        return false;
    }

    public static bool isNeutral(Factions f1, Factions f2)
    {
        if (f1 == Factions.Player && f2 == Factions.Environment) return true;
        return false;
    }



}
