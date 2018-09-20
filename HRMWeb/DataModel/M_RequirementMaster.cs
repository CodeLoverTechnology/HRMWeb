//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRMWeb.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class M_RequirementMaster
    {
        public int RequirementID { get; set; }
        public string RequirementTitle { get; set; }
        public string JobTitle_Designation { get; set; }
        public string JobDescription { get; set; }
        public string AnnualCTC { get; set; }
        public string WorkExperience { get; set; }
        public string KeySkills { get; set; }
        public int JobLocationID { get; set; }
        public int NoOfPosition { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string OpeningDate { get; set; }
        public string JoiningDate { get; set; }
        public int RequirementStatusID { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
    
        public virtual M_CommonMasterTable M_CommonMasterTable { get; set; }
        public virtual M_CompanyMasters M_CompanyMasters { get; set; }
        public virtual M_LocationMasters M_LocationMasters { get; set; }
    }
}