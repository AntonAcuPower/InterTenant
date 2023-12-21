using System;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;

namespace ASCInterCo
{
    [Serializable]
    [PXCacheName("ASCICTranSetup")]

    public class ASCICTranSetup : IBqlTable
    {
        #region Keys
        public class PK : PrimaryKeyOf<ASCICTranSetup>.By<iCTranSetupID>
        {
            public static ASCICTranSetup Find(PXGraph graph, int iCTranSetupID) => FindBy(graph, iCTranSetupID);
        }
        #endregion

        #region ICTranSetupID
        [PXDBIdentity]
        [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible, DisplayName = "Setup ID")]
        public virtual int? ICTranSetupID { get; set; }
        public abstract class iCTranSetupID : IBqlField { }
        #endregion

        #region ICSortOrder
        [PXDBInt()]
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
        [PXDBInt()]
        [PXUIField(DisplayName = "Source ID", Visibility = PXUIVisibility.Visible, Visible = false)]
        [PXDimensionSelector("ICSITE", typeof(Search<ASCICSite.iCsiteID,
            Where<ASCICSite.isActive, Equal<True>>>), typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.description),
            DescriptionField = typeof(ASCICSite.description))]
        [PXReferentialIntegrityCheck]
        [PXParent(typeof(Select<ASCICSite,
            Where<ASCICSite.iCsiteID,
            Equal<Current<ASCICTranSetup.sourceSiteID>>>>)
            )]
        public virtual int? SourceSiteID { get; set; }
        public abstract class sourceSiteID : IBqlField { }
        #endregion

        #region SourceObject
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Source Object")]
        public virtual string SourceObject { get; set; }
        public abstract class sourceObject : IBqlField { }
        #endregion

        #region SourceTranType
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Source Type")]
        public virtual string SourceTranType { get; set; }
        public abstract class sourceTranType : IBqlField { }
        #endregion

        #region DestinationSiteID
        [PXDBInt()]
        [PXUIField(DisplayName = "Destination ID", Visibility = PXUIVisibility.Visible, Visible = false)]
        [PXDimensionSelector("ICSITE", typeof(Search<ASCICSite.iCsiteID,
            Where<ASCICSite.isActive, Equal<True>>>), typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.description),
            DescriptionField = typeof(ASCICSite.description))]
        [PXReferentialIntegrityCheck]
        [PXParent(typeof(Select<ASCICSite,
            Where<ASCICSite.iCsiteID,
            Equal<Current<ASCICTranSetup.destinationSiteID>>>>)
            )]
        public virtual int? DestinationSiteID { get; set; }
        public abstract class destinationSiteID : IBqlField { }
        #endregion

        #region DestinationObject
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Destination Object")]
        public virtual string DestinationObject { get; set; }
        public abstract class destinationObject : IBqlField { }
        #endregion

        #region DestinationTranType
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Destination Type")]
        public virtual string DestinationTranType { get; set; }
        public abstract class destinationTranType : IBqlField { }
        #endregion

        #region Tstamp
        [PXDBTimestamp()]
        [PXUIField(DisplayName = "Tstamp")]
        public virtual byte[] Tstamp { get; set; }
        public abstract class tstamp : IBqlField { }
        #endregion

        #region CreatedByID

        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID { get; set; }
        public abstract class createdByID : IBqlField { }
        #endregion

        #region CreatedByScreenID

        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID { get; set; }
        public abstract class createdByScreenID : IBqlField { }
        #endregion

        #region CreatedDateTime
        [PXDBDate()]
        [PXUIField(DisplayName = "Created Date Time")]
        public virtual DateTime? CreatedDateTime { get; set; }
        public abstract class createdDateTime : IBqlField { }
        #endregion

        #region LastModifiedByID

        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID { get; set; }
        public abstract class lastModifiedByID : IBqlField { }
        #endregion

        #region LastModifiedByScreenID

        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID { get; set; }
        public abstract class lastModifiedByScreenID : IBqlField { }
        #endregion

        #region LastModifiedDateTime
        [PXDBDate()]
        [PXUIField(DisplayName = "Last Modified Date Time")]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        public abstract class lastModifiedDateTime : IBqlField { }
        #endregion
    }
}