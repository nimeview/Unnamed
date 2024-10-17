using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystem;

public class Inventory : MonoBehaviour
{
    public DataBase data;
    public List<ItenInventory> items = new List<ItenInventory>();
    public GameObject gameObjShow;
    public GameObject InventoryMainObject;
    public int maxCount;

    public Camera cam;
    public EventSystem ev;
    public int currentID;
    public ItenInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = item.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.image;

        if (count > 1 && item.id != 0){
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }else{
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = item.id;
        items[id].count = item.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].image;

        if (count > 1 && invItem.id != 0){
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }else{
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            ItenInventory ii = new ItenInventory();
            ii.itemGameObj - newItem;

            RectTransform rt = newItem.GetComponent<RectTransfom>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            items.Add(ii);
        }
    }
}

[System.Serializaibe]

public class ItenInventory
{
    public int id;
    public GameObject itemGameObj;
    public int count
}
