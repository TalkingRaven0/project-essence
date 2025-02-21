using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EssenceType
{
    Fire,
    Electric,
    Water
}

public class EssencePoolable : BasePoolable
{
    [SerializeField] private EssenceType essenceType;

    [SerializeField] protected GameObject mainObject;
    [SerializeField] protected MeshRenderer orb;
    [SerializeField] protected Light glow;

    public EssenceType EssenceType => essenceType;

    public virtual void OnSpawn(EssenceType type)
    {
        base.OnSpawn();

        this.essenceType = type;
        Color essenceColor = GetEssenceColor(type);

        orb.material.color = essenceColor;
        glow.color = essenceColor;
    }

    public static Color GetEssenceColor(EssenceType type)
    {
        Color essenceColor = Color.white;

        switch (type)
        {
            case EssenceType.Fire:
                essenceColor = Color.red; 
                break;
            case EssenceType.Water: 
                essenceColor = Color.blue;
                break;
            case EssenceType.Electric:
                essenceColor = Color.cyan;
                break;
        }

        return essenceColor;
    }
}
