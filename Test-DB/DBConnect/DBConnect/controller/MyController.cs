using DBConnect.view;

namespace DBConnect.controller
{
    abstract class MyController
    {
        private MyView myView;
        private DBUtil util;

        public MyController()
        {
        }
        public MyController(MyView myView, DBUtil util)
        {
            this.myView = myView;
            this.util = util;
        }
    }
}
