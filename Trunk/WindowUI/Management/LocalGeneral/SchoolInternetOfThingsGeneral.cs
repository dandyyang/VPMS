using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.Master;

namespace WindowUI.Management.LocalGeneral
{
    class ManagementGeneral
    {
        public ManagementGeneral()
        {

        }

        public static string GetClassroomName(SiteMaster_stm_Info classroomInfo)
        {
            string classroomName = string.Empty;

            if (classroomInfo == null)
            {
                return classroomName;
            }

            if (classroomInfo.BuildingName.Trim() != "")
            {
                classroomName=classroomInfo.BuildingName.Trim() + "--";
            }

            if (classroomInfo.stm_cName.Trim()!="")
            {
                classroomName +=  classroomInfo.stm_cName.Trim();
            }

            return classroomName;
        }
    }
}
