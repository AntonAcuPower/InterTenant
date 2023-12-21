

namespace ASCInterCo
{
    using System;
    using PX.Data;
    using PX.Data.ReferentialIntegrity.Attributes;
    using PX.Objects.CR;

    [Serializable]
    [PXCacheName(Messages.ICSiteBAccountSettings)]
    [PXPrimaryGraph(typeof(ASCICSiteBAccountSettingsMaint))]
    public class ASCICSiteBAccountSettings : IBqlTable
    {


        #region Keys

        public class PK : PrimaryKeyOf<ASCICSiteBAccountSettings>.By<bAccountID, iCTranSetupID>
        {
            public static ASCICSiteBAccountSettings Find(PXGraph graph, int bAccountID, int iCTranSetupID) => FindBy(graph, bAccountID, iCTranSetupID);
        }

        public static class FK
        {
            public class ASCICSiteFK : ASCICSite.PK.ForeignKeyOf<ASCICSiteBAccountSettings>.By<iCTranSetupID> { }
            public class BAccountFK : BAccount.PK.ForeignKeyOf<ASCICSiteBAccountSettings>.By<bAccountID> { }
            public class ASCICSiteBAccountFK : ASCICSiteBAccount.PK.ForeignKeyOf<ASCICSiteBAccountSettings>.By<bAccountID, sourceSiteID> { }
        }
        #endregion

        #region ICTranSetupID

        [PXDBIdentity]
        [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible, DisplayName = "Setup ID")]
        public virtual int? ICTranSetupID { get; set; }
        public abstract class iCTranSetupID : IBqlField { }
        #endregion

        #region BAccountID
        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Acct CD", Visible = false, Enabled = false)]
        [PXDBDefault(typeof(ASCICSiteBAccount.bAccountID), DefaultForUpdate = false)]
        [PXParent(typeof(FK.ASCICSiteBAccountFK))]
        public virtual int? BAccountID { get; set; }
        public abstract class bAccountID : IBqlField { }
        #endregion

        #region ICSortOrder
        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Sort Order")]
        public virtual int? ICSortOrder { get; set; }
        public abstract class iCSortOrder : IBqlField { }
        #endregion

        #region IsActive
        [PXDBBool()]
        [PXUIField(DisplayName = "Active")]
        public virtual bool? IsActive { get; set; }
        public abstract class isActive : IBqlField { }
        #endregion

        #region SourceSiteID
        [PXDBInt(IsKey = true)]
        [PXDBDefault(typeof(ASCICSiteBAccount.iCsiteID), DefaultForUpdate = false)]
        [PXParent(typeof(FK.ASCICSiteBAccountFK))]
        [PXUIField(DisplayName = "Source Site", Visible = false, Enabled = false)]
        [PXDimensionSelector("ICSITE", typeof(Search<ASCICSite.iCsiteID,
            Where<ASCICSite.isActive, Equal<True>>>), typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.description),
            DescriptionField = typeof(ASCICSite.description))]
        public virtual int? SourceSiteID { get; set; }
        public abstract class sourceSiteID : IBqlField { }
        #endregion

        #region SourceObject
        [PXDBString(30, IsKey = true, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Source Object")]
        public virtual string SourceObject { get; set; }
        public abstract class sourceObject : IBqlField { }
        #endregion

        #region SourceType
        [PXDBString(30, IsKey = true, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Source Type")]
        public virtual string SourceType { get; set; }
        public abstract class sourceType : IBqlField { }
        #endregion

        #region DestinationSiteID
        [PXDBInt(IsKey = true)]
        [PXParent(typeof(FK.ASCICSiteBAccountFK))]
        [PXUIField(DisplayName = "Destination Site", Visibility = PXUIVisibility.SelectorVisible)]
        [PXDimensionSelector("ICSITE", typeof(Search<ASCICSite.iCsiteID,
            Where<ASCICSite.isActive, Equal<True>>>), typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.description),
            DescriptionField = typeof(ASCICSite.description))]
        public virtual int? DestinationSiteID { get; set; }
        public abstract class destinationSiteID : IBqlField { }
        #endregion

        #region DestinationObject
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Destination Object")]
        public virtual string DestinationObject { get; set; }
        public abstract class destinationObject : IBqlField { }
        #endregion

        #region DestinationType
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Destination Type")]
        public virtual string DestinationType { get; set; }
        public abstract class destinationType : IBqlField { }
        #endregion

    }
}