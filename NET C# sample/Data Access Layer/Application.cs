//------------------------------------------------------------------------------
//  Name: Application Object Class
//  Last modified by:
//  Date:
//  Notes:
//     
// 
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseMgmtWeb.Models
{
    public class Application
    {
        //Object properties (table fields)
        public int Id { get; set; }

        [Display(Name = "EPR Id")]
        public string EPRId { get; set; }

        [Display(Name = "Application Name")]
        public string ApplicationName { get; set; }

        [Display(Name = "Application Alias")]
        public string ApplicationAlias { get; set; }
        
        public int CreatedBy { get; set; }
        
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Updated By")]
        public int UpdatedBy { get; set; }

        [Display(Name = "Updated Date")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime UpdatedDate { get; set; }

        //Navigation properties (foreign keys)
        [ForeignKey("CreatedBy")]
        [InverseProperty("Application")]
        public virtual Users Users { get; set; }

        [ForeignKey("UpdatedBy")]
        [InverseProperty("Application1")]
        public virtual Users Users1 { get; set; }

        public virtual ICollection<ApplicationRelease> ApplicationRelease { get; set; }

        //Create display name for dropdown menu in app release views
        [Display(Name = "EPR Id/Name")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", EPRId, ApplicationName);
            }
        }
    }
}
