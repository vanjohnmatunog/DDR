using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cl_DR_DMGTransactionObject
/// </summary>
public class cl_DR_DMGTransactionObject
{
    public string RefID { get; set; }
    public string Status { get; set; }
    public string IssuedBy { get; set; }
    public string Local { get; set; }
    public string AffectedBU { get; set; }
    public string NatureOfDiscrepancy { get; set; }
    public string DamageNote { get; set; }
    public string InvoiceNo { get; set; }
    public string ItemCode { get; set; }
    public string Supplier { get; set; }
    public string Description { get; set; }
    public string Forwarder { get; set; }
    public string InvoiceQty { get; set; }
    public string BlAwhNo { get; set; }
    public string QtyAffected { get; set; }
    public DateTime ReceivedDate { get; set; }
    public string NoOfBoxAffected { get; set; }
    public string Remarks { get; set; }
    public string SendToQA { get; set; }
    public string SendToMPD { get; set; }
    public string CopyTo { get; set; }
    public string ApprovedBy { get; set; }
    public string ApproverName { get; set; }
    public string Attributes1 { get; set; }
    public string Attributes2 { get; set; }
    public string Attributes3 { get; set; }
    public string EmpNoQA { get; set; }
    public string EmpNameQA { get; set; }
    public string DeptQA { get; set; }
    public string EmpNoMPD { get; set; }
    public string EmpNameMPD { get; set; }
    public string DeptMPD { get; set; }
    public string EmpNoCopy { get; set; }
    public string EmpNameCopy { get; set; }
    public string DeptCopy { get; set; }
    public string UserName { get; set; }
    public Boolean isAdmin { get; set; }
    public Boolean isLeader { get; set; }
}