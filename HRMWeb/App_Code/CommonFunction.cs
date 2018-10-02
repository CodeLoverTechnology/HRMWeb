using HRMWeb.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HRMWeb.App_Code
{
    public class CommonFunction
    {
        private static HRM_DBEntities db = new HRM_DBEntities();
        public static string GenerateEmployeeCode()
        {
            string EmployeeCode = string.Empty;

            M_EmployeeMasters StudentMasterDetails = (from StudentMaster in db.M_EmployeeMasters select StudentMaster).OrderByDescending(x => x.EmployeeID).Take(1).FirstOrDefault();
           // var LastEmployeeCode =(from s in db.M_EmployeeMasters.OrderBy(x => x.EmployeeID).Take(1) select new {s.EmployeeID });
            int LastEmployeeDigit = Convert.ToInt32(StudentMasterDetails.EmployeeID.Substring(3, (StudentMasterDetails.EmployeeID.Length-3)));
            EmployeeCode = Resources.HRMResources.EmployeeCodeFormate + (LastEmployeeDigit+1).ToString(Resources.HRMResources.EmployeeCodeDigit);
            return EmployeeCode;
        }

        public static bool IsFolderExist(string FolderPathFromResource)
        {
            try
            {
                if (!Directory.Exists(Path.GetFullPath(FolderPathFromResource)))
                {
                    Directory.CreateDirectory(Path.GetFullPath(FolderPathFromResource));
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool IsFileExist(string NewFileName)
        {
            try
            {
                if (!File.Exists(Path.GetFullPath(NewFileName)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckUserAuthentication()
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            HttpContext.Current.Response.Cache.SetNoStore();
            if (HttpContext.Current.Session["LoginUserID"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}