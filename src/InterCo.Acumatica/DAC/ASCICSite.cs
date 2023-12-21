


namespace ASCInterCo
{
    using System;
    using PX.Data;
    using PX.Data.ReferentialIntegrity.Attributes;
    using PX.Data.EP;
    using PX.Objects.CR.MassProcess;

    [Serializable]
    [PXCacheName(Messages.ICSite)]
    [PXPrimaryGraph(typeof(ASCICSiteMaint))]
    public class ASCICSite : IBqlTable
    {
        #region Keys
        public class PK : PrimaryKeyOf<ASCICSite>.By<iCsiteID>
        {
            public static ASCICSite Find(PXGraph graph, int iCsiteID) => FindBy(graph, iCsiteID);
        }
        #endregion

        #region ICsiteID

        [PXDBIdentity]
        [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible, DisplayName = "Site ID")]
        [PXReferentialIntegrityCheck]
        public virtual Int32? ICsiteID { get; set; }
        public abstract class iCsiteID : PX.Data.IBqlField { }
        #endregion

        #region ICsiteCD
        [PXDimensionSelector("ICSITE", typeof(ASCICSite.iCsiteCD), typeof(ASCICSite.iCsiteCD), DescriptionField = typeof(ASCICSite.description))]
        [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
        [PXDefault()]
        [PXUIField(DisplayName = "Site ID", Visibility = PXUIVisibility.SelectorVisible)]
        [PXFieldDescription]
        public virtual String ICsiteCD { get; set; }
        public abstract class iCsiteCD : PX.Data.IBqlField { }
        #endregion

        #region Description
        [PXDBString(60, IsUnicode = true)]
        [PXDefault()]
        [PXUIField(DisplayName = "Site Name", Visibility = PXUIVisibility.SelectorVisible)]
        [PXFieldDescription]
        [PXMassMergableField]
        public virtual String Description { get; set; }
        public abstract class description : PX.Data.IBqlField { }
        #endregion

        #region IsActive
        [PXDBBool()]
        [PXUIField(DisplayName = "Active")]
        public virtual bool? IsActive { get; set; }
        public abstract class isActive : IBqlField { }
        #endregion

        #region Url
        [PXDBString(256, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Url")]
        public virtual string Url { get; set; }
        public abstract class url : IBqlField { }
        #endregion

        #region Login
        [PXDBString(256, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Login")]
        public virtual string Login { get; set; }
        public abstract class login : IBqlField { }
        #endregion

        #region CompanyKey
        [PXDBString(128, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Company Key")]
        public virtual string CompanyKey { get; set; }
        public abstract class companyKey : IBqlField { }
        #endregion

        #region BranchCD
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Branch CD")]
        public virtual string BranchCD { get; set; }
        public abstract class branchCD : IBqlField { }
        #endregion
       
        #region Password
        public abstract class password : IBqlField { }
        [PXRSACryptStringAttribute(IsUnicode = true)]
        [PXUIField(DisplayName = "Password", Visibility = PXUIVisibility.Visible)]
        [PXDefault]
        public virtual string Password { get; set; }
        #endregion

        #region VersionSegment
        [PXDBString(256, IsUnicode = true, InputMask = "")]
        [PXDefault("/entity/Default/17.200.001/")]
        [PXUIField(DisplayName = "Version Segment")]
        public virtual string VersionSegment { get; set; }
        public abstract class versionSegment : IBqlField { }
        #endregion

        #region MaxAttempts
        [PXDBInt(MinValue = 1, MaxValue = 5)]
        [PXUIField(DisplayName = "Max Attempts")]
        public virtual Int32? MaxAttempts { get; set; }
        public abstract class maxAttempts : PX.Data.IBqlField { }
        #endregion

        #region Timeout
        [PXDBInt(MinValue = 0, MaxValue = 3600)]
        [PXUIField(DisplayName = "Call Timeout")]
        public virtual Int32? Timeout { get; set; }
        public abstract class timeout : PX.Data.IBqlField { }
        #endregion

    }
}