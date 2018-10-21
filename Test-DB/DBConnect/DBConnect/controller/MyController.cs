using DBConnect.view;
using System;
using System.Collections.Generic;

namespace DBConnect.controller
{
    public abstract class MyController
    {
        private List<MyView> views;
        //MyView Views { get; set; }
        //event EvenHandler<MyviewEventArgs> ProjectUpdated;
        //DBUtil Util { get; set; }

        public MyController()
        {
            views = new List<MyView>();
        }
        public virtual void UpdatedDept()
        {
            Console.WriteLine("Send update event");
            if (views.Count == 0) return;
            foreach (var item in views)
            {
                Console.WriteLine(item.Name);
                if (item != null)
                    item.DeptUpdate();
            }
        }
        public virtual void UpdatedEmp()
        {
            if (views.Count == 0) return;
            foreach (var item in views)
            {
                if (item != null)
                    item.EmpUpdate();
            }
        }
        public void AttachView(MyView view)
        {
            views.Add(view);
        }
    }

    public class MyviewEventArgs : EventArgs
    {
        public MyView MyView { get; set; }

        public MyviewEventArgs(MyView myView)
        {
            this.MyView = myView;
        }
    }
}
