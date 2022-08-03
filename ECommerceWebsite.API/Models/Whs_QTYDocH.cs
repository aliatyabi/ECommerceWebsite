//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECommerceWebsite.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Whs_QTYDocH
    {
        public Whs_QTYDocH()
        {
            this.Whs_QTYDocD = new HashSet<Whs_QTYDocD>();
        }
    
        public decimal Code { get; set; }
        public string DocNo { get; set; }
        public Nullable<short> TransType { get; set; }
        public Nullable<decimal> CompanyID { get; set; }
        public string DocDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Atf { get; set; }
        public string Note { get; set; }
        public Nullable<double> DocSum { get; set; }
        public Nullable<double> Discount { get; set; }
        public string DocTime { get; set; }
        public Nullable<int> StationID { get; set; }
        public Nullable<int> SalesType { get; set; }
        public Nullable<double> Sarbar { get; set; }
        public Nullable<double> ReceivedPrice { get; set; }
        public Nullable<int> PayType { get; set; }
        public string Base { get; set; }
        public Nullable<int> Branch { get; set; }
        public Nullable<int> Branch2 { get; set; }
        public Nullable<decimal> RelatedCode { get; set; }
        public Nullable<double> PFishReceived { get; set; }
        public Nullable<double> PFishPay { get; set; }
        public Nullable<double> TAX { get; set; }
        public Nullable<double> RoundX { get; set; }
        public Nullable<double> RoundD { get; set; }
        public Nullable<int> PosTypeID { get; set; }
        public Nullable<double> PosPrice { get; set; }
        public Nullable<decimal> PI_Code { get; set; }
        public Nullable<decimal> CS_Code { get; set; }
        public string BN { get; set; }
        public Nullable<int> VendorID { get; set; }
        public Nullable<int> Send { get; set; }
        public int ID { get; set; }
        public Nullable<double> P_Fee { get; set; }
        public Nullable<int> P_Qty { get; set; }
        public Nullable<int> ChangeID { get; set; }
        public Nullable<int> Member_Send_status { get; set; }
        public Nullable<int> I_Stat { get; set; }
        public Nullable<int> U_Stat { get; set; }
        public Nullable<int> LU_State { get; set; }
        public Nullable<int> SMS { get; set; }
        public Nullable<int> Chk { get; set; }
        public string EN_DATE { get; set; }
        public Nullable<int> EN_Date_N { get; set; }
        public Nullable<int> EN_Day { get; set; }
        public Nullable<int> EN_DayofWeek { get; set; }
        public Nullable<int> EN_Month { get; set; }
        public Nullable<int> En_Quarter { get; set; }
        public Nullable<int> En_Week { get; set; }
        public Nullable<int> En_Week_Jan { get; set; }
        public Nullable<int> EN_YEAR { get; set; }
        public Nullable<int> EN_Year_Feb_Base { get; set; }
        public Nullable<int> EN_YEAR_WEEK { get; set; }
        public Nullable<int> PrePaid { get; set; }
        public string Second_Note { get; set; }
        public Nullable<int> SHIFT_ID { get; set; }
        public Nullable<int> Issend { get; set; }
    
        public virtual ICollection<Whs_QTYDocD> Whs_QTYDocD { get; set; }
    }
}
