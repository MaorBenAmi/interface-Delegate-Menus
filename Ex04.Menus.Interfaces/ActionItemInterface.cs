using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class ActionItemInterface : IMenuItem
    {
         private readonly string r_ActionName;
        private readonly IMenuItem r_Action;
        private IMenuItem m_PreviousMenu;

        public ActionItemInterface(string i_ActionName, IMenuItem i_Action)
        {
            r_ActionName = i_ActionName;
            r_Action = i_Action;
        }

        public IMenuItem PreviousMenu
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

        public override string ToString()
        {
            return r_ActionName;
        }

        public void Show()
        {
            r_Action.Show();
            Console.WriteLine(string.Format("{0}Press Enter To Continue", Environment.NewLine));
            Console.ReadLine();
        }

        public string Name()
        {
                return this.r_ActionName;
        }
    }
}
