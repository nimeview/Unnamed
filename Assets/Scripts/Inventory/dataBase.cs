using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
}

[System.Serializaibe]

public class Item
{
    public int id;
    public string name;
    public sprite image;
}
