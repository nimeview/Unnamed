using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    public GameObject panel;
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false); 
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            HidePanel(panel);
        }
    }
}