using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel instance { get; private set; }
    public Image dragItemIcon;
    public Text itemDesGUI;

    public Inventory inventory;
    public CharPanel charPanel;

    void Awake()
    {
        instance = this;

        dragItemIcon = transform.GetChild(1).GetComponent<Image>();
    }

    public void SwithGUI(bool b)
    {
        if (b)
        {
            // show inventory gui
            gameObject.SetActive(true);
        }
        else
        {
            // hide inventory gui
            gameObject.SetActive(false);
        }
    }
}