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
    
    public partial class T_EmployeeLeave
    {
        public int LeaveID { get; set; }
        public string EmployeeID { get; set; }
        public string LeaveReason { get; set; }
        public System.DateTime LeaveFromDate { get; set; }
        public System.DateTime LeaveToDate { get; set; }
        public double NoOfLeave { get; set; }
        public int TypeOfLeaveID { get; set; }
        public string ApproverManagerID { get; set; }
        public string ComponsetReason { get; set; }
        public string LeaveFileAttachment { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
        public Nullable<int> LeaveStatus { get; set; }
    
        public virtual M_CommonMasterTable M_CommonMasterTable { get; set; }
        public virtual M_EmployeeMasters M_EmployeeMasters { get; set; }
        public virtual M_EmployeeMasters M_EmployeeMasters1 { get; set; }
    }
}
