using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int day;
    // inventaire
    // position objets posés
    // objets ramassés (objets restants sur la map)
    // player status (pénalité)
    // liste des taches (taches apparues, pas faites et faites)

    public SaveData(EndDay.InGameDay saveDay)
    {
        day = saveDay.day;
    }
}
