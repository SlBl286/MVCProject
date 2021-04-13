using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Helpers
{
    public static class GHelper
    {

        public static string XuLyTen(string name)
        {
            string result = "";
            if (name != "" && name != null)
            {
                List<char> arrName = new List<char>(name.ToLower().Trim().ToCharArray());
                for (int i = 0; i < arrName.Count; i++)
                {
                    if (arrName[i] == ' ')
                    {
                        int _i = i + 1;
                        if (arrName[_i] == ' ')
                        {
                            arrName.RemoveAt(i);
                        }
                    }
                }
                arrName[0] = char.ToUpper(arrName[0]);
                for (int i = 0; i < arrName.Count; i++)
                {
                    if (arrName[i] == ' ')
                    {
                        int _i = i + 1;
                        arrName[_i] = char.ToUpper(arrName[_i]);
                    }
                }

                for (int i = 0; i < arrName.Count; i++)
                {
                    result += arrName[i].ToString();
                }
            }
            return result;
        }
        public static string LastStaffID()
        {
            string LastStaffId = DBHelper.GetTheLastID().ToString();
            string str = "";
            if (LastStaffId.Count() <= 4)
                str = new string('0', 4 - LastStaffId.Count());
            else
                str = "";
            return "NV-" + str + LastStaffId;
        }
    }
}
