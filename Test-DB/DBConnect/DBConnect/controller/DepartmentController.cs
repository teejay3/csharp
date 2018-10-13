using DBConnect.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnect.controller
{
    class DepartmentController
    {
        private DepartmentsView deptView;
        public DepartmentController(DepartmentsView deptView)
        {
            this.deptView = deptView;
            Init();
        }

        private void Init()
        {

        }
    }
}
