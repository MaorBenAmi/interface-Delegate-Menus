using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public delegate void MenusDelegate(object i_Item);

    public delegate void ActionsDelegate();

    public class ActionItemDelegate
    {
        private readonly string r_MethodName = string.Empty;

        public event MenusDelegate MenusCollector;

        public event ActionsDelegate ActionActivator;

        public ActionItemDelegate(string i_Name)
        {
            r_MethodName = i_Name;
        }

        public void Invoke()
        {
            if (MenusCollector != null)
            {
                MenusCollector.Invoke(this);
            }
        }

        public override string ToString()
        {
            return r_MethodName;
        }

        public void Show()
        {
            if (ActionActivator != null)
            {
                ActionActivator.Invoke();
            }

            Console.WriteLine(string.Format("{0}Press Enter To Continue", Environment.NewLine));
            Console.ReadLine();
        }
    }
}
