using PX.Data.ReferentialIntegrity.Attributes;

namespace ASCInterCo
{
    using System;
    using PX.Data;

    using PX.Data.EP;
    using PX.Objects.CR.MassProcess;

    [System.SerializableAttribute()]
    [PXPrimaryGraph(typeof(ASCICTranEntry))]
    [PXCacheName(Messages.ICTranView)]
    public partial class ASCICTranView : IBqlTable
    {
        #region Selected
        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected { get; set; }
        public abstract class selected : PX.Data.IBqlField { }
        #endregion

        #region Ictranid
        [PXDBIdentity(IsKey = true)]
        [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible, DisplayName = "IC Tran ID")]
        public virtual int? ICtranid { get; set; }
        public abstract class iCtranid : IBqlField { }
        #endregion

        #region SourceSiteID
        [PXDBInt()]
        //[PXParent(typeof(Select<ASCICSite,
        //    Where<ASCICSite.iCsiteID,
        //    Equal<Current<ASCICTran.sourceSiteID>>>>)
        //    )]
        [PXUIField(DisplayName = "Source Site", Visibility = PXUIVisibility.SelectorVisible)]
        [PXDimensionSelector("ICSITE", typeof(Search<ASCICSite.iCsiteID,
            Where<ASCICSite.isActive, Equal<True>>>), typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.description),
            DescriptionField = typeof(ASCICSite.description))]
        public virtual Int32? SourceSiteID { get; set; }
        public abstract class sourceSiteID : IBqlField { }
        #endregion

        #region SourceObject
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Source Object")]
        public virtual string SourceObject { get; set; }
        public abstract class sourceObject : IBqlField { }
        #endregion

        #region SourceType
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Source Type")]
        public virtual string SourceType { get; set; }
        public abstract class sourceType : IBqlField { }
        #endregion

        #region SourceRef
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Source Ref Nbr.")]
        public virtual string SourceRef { get; set; }
        public abstract class sourceRef : IBqlField { }
        #endregion

        #region DestinationSiteID
        [PXDBInt()]
        [PXDBDefault]
        //[PXParent(typeof(Select<ASCICSite,
        //    Where<ASCICSite.iCsiteID,
        //    Equal<Current<ASCICTran.destinationSiteID>>>>)
        //    )]
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

        #region RemoteAcctCD
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Remote Acct CD")]
        public virtual string RemoteAcctCD { get; set; }
        public abstract class remoteAcctCD : IBqlField { }
        #endregion

        #region DestinationRef
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Destination Ref Nbr.")]
        public virtual string DestinationRef { get; set; }
        public abstract class destinationRef : IBqlField { }
        #endregion

        #region Complete
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Complete")]
        public virtual bool? Complete { get; set; }
        public abstract class complete : IBqlField { }
        #endregion

        #region FailedAttempts
        [PXDBInt()]
        [PXUIField(DisplayName = "Failed Attempts")]
        public virtual int? FailedAttempts { get; set; }
        public abstract class failedAttempts : IBqlField { }
        #endregion

        #region CompletedDateTime
        [PXDBDate()]
        [PXUIField(DisplayName = "Completed Date Time")]
        public virtual DateTime? CompletedDateTime { get; set; }
        public abstract class completedDateTime : IBqlField { }
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