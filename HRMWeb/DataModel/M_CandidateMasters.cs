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
    
    public partial class M_CandidateMasters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M_CandidateMasters()
        {
            this.T_CandidateFollowUpDetails = new HashSet<T_CandidateFollowUpDetails>();
            this.T_FinalCandidatePlacementDetails = new HashSet<T_FinalCandidatePlacementDetails>();
        }
    
        public string CandidateID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string ContactNo { get; set; }
        public int RoleID { get; set; }
        public int LocationID { get; set; }
        public string DesiredCity { get; set; }
        public Nullable<int> DesignationID { get; set; }
        public string KeySkills { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string CurrentCTC { get; set; }
        public string AspectedCTC { get; set; }
        public string TotalExperience { get; set; }
        public string CV { get; set; }
        public Nullable<int> CandidateStatusID { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
    
        public virtual M_CommonMasterTable M_CommonMasterTable { get; set; }
        public virtual M_CompanyMasters M_CompanyMasters { get; set; }
        public virtual M_LocationMasters M_LocationMasters { get; set; }
        public virtual M_RoleMaster M_RoleMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_CandidateFollowUpDetails> T_CandidateFollowUpDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_FinalCandidatePlacementDetails> T_FinalCandidatePlacementDetails { get; set; }
    }
}
