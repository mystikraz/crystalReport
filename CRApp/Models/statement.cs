//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class statement
    {
        public int Id { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public System.DateTime PostedDate { get; set; }
        public string DescriptionOfTransactions { get; set; }
        public decimal TransactionsAmount { get; set; }
        public decimal BilledAmount { get; set; }
        public string DRCR { get; set; }
        public string CardNumber { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string imagePath { get; set; }
        public string Acnumber { get; set; }
    }
}
