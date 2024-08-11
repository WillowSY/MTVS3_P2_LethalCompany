using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrapData", menuName = "Scriptable Objects/ScrapData", order = 0)]
public class ScrapData : ScriptableObject
{
    [SerializeField] 
    private string scrapName; 
    public string ScrapName
    {
        get { return scrapName; }
    }

    [SerializeField] 
    private int scrapPrice;
    public int ScrapPrice
    {
        get { return scrapPrice; }
    }

    [SerializeField]
    private int scrapWeight;
    public int ScrapWeight
    {
        get { return scrapWeight; }
    }

    [SerializeField] 
    private GameObject scrapPrefab;

    public GameObject ScrapPrefab
    {
        get {
            return scrapPrefab;
        }
    }
    [SerializeField] 
    private Sprite scrapIcon;

    public Sprite ScrapIcon
    {
        get {
            return scrapIcon;
        }
    }

    [SerializeField]
    private bool twoHandItem;

    public bool IsTwoHandItem 
    {
        get { return twoHandItem; }
    }
}
