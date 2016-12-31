using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItemInterface : IMenuItem
    {
        private readonly string r_MenuName;
        private readonly List<IMenuItem> r_SubMenusList;
        private MenuItemInterface m_PreviousMenu;
        private int m_CurrentLevel;

        public List<IMenuItem> SubMenusList
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

        public MenuItemInterface PreviousMenu
        {
            get 
            { 
                return m_PreviousMenu; 
            }

            set 
            { 
                m_PreviousMenu = value;
            }
        }

       public MenuItemInterface(string i_MenuName)
        {
            r_MenuName = i_MenuName;
            r_SubMenusList = new List<IMenuItem>();
            m_PreviousMenu = null;
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

        public void AddSubMenuToList(MenuItemInterface i_SubMenu)
        {
            SubMenusList.Add(i_SubMenu);
            i_SubMenu.CurrentLevel = this.CurrentLevel + 1;
            i_SubMenu.PreviousMenu = this;
        }

        public void AddActionToMenu(ActionItemInterface i_Action)
        {
            this.r_SubMenusList.Add(i_Action);
            i_Action.PreviousMenu = this;
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
                Console.WriteLine(this.Name());
                foreach (IMenuItem subMenu in SubMenusList)
                {
                    Console.WriteLine(indexForMenu + "." + subMenu.Name());
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
                    this.SubMenusList[userChoice - 1].Show();
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

            public string Name()
            {
                return MenuName;
            }
        }
    }
