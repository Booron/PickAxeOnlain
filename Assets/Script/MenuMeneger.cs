using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMeneger : MonoBehaviour
{
    public static MenuMeneger Instance;
    [SerializeField]
    Menu[] menus;
    private void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(string menuname)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuname == menuname)
            {
                OpenMenu(menus[i]);
            }else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }


}
