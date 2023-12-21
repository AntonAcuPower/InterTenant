

namespace ASCInterCo
{
    using System;
    using PX.Data;
    using PX.Data.ReferentialIntegrity.Attributes;
    using PX.Objects.CR;

    [Serializable]
    [PXCacheName(Messages.ICSiteBAccount)]
    [PXPrimaryGraph(typeof(ASCICSiteBAccountMaint))]
    public class ASCICSiteBAccount : IBqlTable
    {
        #region Keys
        public class PK : PrimaryKeyOf<ASCICSiteBAccount>.By<bAccountID, iCsiteID>
        {
            public static ASCICSiteBAccount Find(PXGraph graph, int bAccountID, int iCsiteID) => FindBy(graph, bAccountID, iCsiteID);
        }

        public static class FK
        {
            public class ASCICSiteFK : ASCICSite.PK.ForeignKeyOf<ASCICSiteBAccount>.By<iCsiteID> { }
            public class BAccountFK : BAccount.PK.ForeignKeyOf<ASCICSiteBAccount>.By<bAccountID> { }
        }
        #endregion

        #region BAccountID
        [PXDBInt(IsKey = true)]
        [PXExtraKey()]
        [PXUIField(DisplayName = "Acct CD", Visibility = PXUIVisibility.SelectorVisible)]
        [PXDimensionSelector("BIZACCT", typeof(Search<BAccount.bAccountID,
            Where<BAccount.type, Equal<BAccountType.customerType>,
                Or<BAccount.type, Equal<BAccountType.branchType>,
                Or<BAccount.type, Equal<BAccountType.vendorType>,
                Or<BAccount.type, Equal<BAccountType.combinedType>>>>>>), typeof(BAccount.acctCD),
            typeof(BAccount.acctCD),
            typeof(BAccount.acctName), typeof(BAccount.type), typeof(BAccount.classID), typeof(BAccount.status), typeof(Contact.phone1), typeof(Address.city), typeof(Address.countryID),
            DescriptionField = typeof(BAccount.acctName))]
        [PXParent(typeof(Select<BAccount,
            Where<BAccount.bAccountID,
            Equal<Current<ASCICSiteBAccount.bAccountID>>>>)
            )]
        public virtual int? BAccountID { get; set; }
        public abstract class bAccountID : IBqlField { }
        #endregion

        #region ICsiteID
        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Site ID", Visibility = PXUIVisibility.SelectorVisible)]
        [PXDimensionSelector("ICSITE", typeof(Search<ASCICSite.iCsiteID,
            Where<ASCICSite.isActive, Equal<True>>>), typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.iCsiteCD),
            typeof(ASCICSite.description),
            DescriptionField = typeof(ASCICSite.description))]
        [PXReferentialIntegrityCheck]
        [PXParent(typeof(Select<ASCICSite,
            Where<ASCICSite.iCsiteID,
            Equal<Current<ASCICSiteBAccount.iCsiteID>>>>)
            )]
        public virtual int? ICsiteID { get; set; }
        public abstract class iCsiteID : IBqlField { }
        #endregion

        #region RemoteAcctCD
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Remote Acct CD")]
        public virtual string RemoteAcctCD { get; set; }
        public abstract class remoteAcctCD : IBqlField { }
        #endregion

        #region IsActive
        [PXDBBool()]
        [PXUIField(DisplayName = "Is Active")]
        public virtual bool? IsActive { get; set; }
        public abstract class isActive : IBqlField { }
        #endregion

    }
}