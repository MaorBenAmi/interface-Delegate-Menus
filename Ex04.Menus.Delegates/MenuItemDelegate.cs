using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MenuItemDelegate
    {
        private readonly string r_MenuName;
        private readonly List<object> r_SubMenusList;
        private int m_CurrentLevel;

        private event MenusDelegate MenusCollector = null;

        public List<object> SubMenusList
        {
            get 
            { 
                return r_SubMenusList; 
            }
        } 

        public string MenuName
        {
            get
            {
                return r_MenuName;
            }
        } 

        public MenuItemDelegate(string i_Name)
        {
            r_MenuName = i_Name;
            r_SubMenusList = new List<object>();
            m_CurrentLevel = 0;
        }

        public int CurrentLevel
        {
            get 
            {
                return m_CurrentLevel; 
            }

            set
            {
                m_CurrentLevel = value;
            }
        }

        public void AddSubMenuToList(MenuItemDelegate i_SubMenu)
        {
            SubMenusList.Add(i_SubMenu);
            i_SubMenu.CurrentLevel = this.CurrentLevel + 1;
            i_SubMenu.MenusCollector += this.UserChoice;
        }

        public void AddActionToMenu(ActionItemDelegate i_Action)
        {
            SubMenusList.Add(i_Action);
            i_Action.MenusCollector += this.UserChoice;
        }

        public void Show()
        {
            int userChoice = 0;
            int indexForMenu = 0;
            string exitOrBack = string.Empty;
            while (true)
            {
                indexForMenu = 1;
                exitOrBack = "Exit";
                Console.Clear();
                Console.WriteLine(this.MenuName);
                foreach (object subMenu in SubMenusList)
                {
                    Console.WriteLine(indexForMenu + "." + subMenu.ToString());
                    indexForMenu++;
                }

                if (this.CurrentLevel != 0)
                {
                    exitOrBack = "Back";
                }

                Console.WriteLine("0." + exitOrBack);
                Console.WriteLine("Please Select Your Choice Or Select " + exitOrBack);
                userChoice = inputFromUserAndCheckIfInRange(0, this.SubMenusList.Count);
                if (userChoice == 0)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    if (SubMenusList[userChoice - 1] is MenuItemDelegate)
                    {
                        (SubMenusList[userChoice - 1] as MenuItemDelegate).MenusCollector.Invoke(SubMenusList[userChoice - 1]);
                    }
                    else
                    {
                        if (SubMenusList[userChoice - 1] is ActionItemDelegate)
                        {
                            (SubMenusList[userChoice - 1] as ActionItemDelegate).Invoke();
                        }
                    }
                }
            }
        }

        private int inputFromUserAndCheckIfInRange(int i_Min, int i_Max)
        {
            int userChoice = 0;
            string userChoiseStr = string.Empty;
            bool isValidInput = false;
            while (!isValidInput)
            {
                userChoiseStr = Console.ReadLine();
                try
                {
                    userChoice = int.Parse(userChoiseStr);
                    isValidInput = userChoice >= i_Min && userChoice <= i_Max;
                    if (!isValidInput)
                    {
                        Console.WriteLine("Input Is Out Of Range, Please Try Again");
                        Console.WriteLine("Input Should be between " + i_Min + " To " + i_Max);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("This Is Not An Integer, Please Try Again");
                }
            }

            return userChoice;
        }

        public override string ToString()
        {
            return MenuName;
        }

        public void UserChoice(object i_UserChoice)
        {
            if (i_UserChoice is MenuItemDelegate)
            {
                (i_UserChoice as MenuItemDelegate).Show();
            }
            else
            {
                if (i_UserChoice is ActionItemDelegate)
                {
                    (i_UserChoice as ActionItemDelegate).Show();
                }
            }
        }
    }
}
