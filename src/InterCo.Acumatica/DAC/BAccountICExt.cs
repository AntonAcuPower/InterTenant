//using System;

//using PX.Data;
//using PX.Objects.CR;
//using PX.Objects.CS;
//using PX.Data.ReferentialIntegrity.Attributes;
//using PX.Objects.Common.Attributes;

//using UploadFile = PX.SM.UploadFile;

//namespace ASCInterCo
//{
//    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
//    public class BAccountICExt : PXCacheExtension<BAccount>
//    {

//        #region ASCICSiteID
//        public abstract class aSCICSiteID : PX.Data.IBqlField
//        {
//        }
//        protected Int32? _ASCICSiteID;
//        [PXDBInt()]
//        [PXUIField(DisplayName = "Intercompany Site", Visibility = PXUIVisibility.SelectorVisible)]
//        [PXSelector(typeof(Search<ASCICSite.iCsiteID, Where<ASCICSite.isActive, Equal<boolTrue>>>), DescriptionField = typeof(ASCICSite.iCsiteCD))]
//        public virtual Int32? ASCICSiteID
//        {
//            get
//            {
//                return this._ASCICSiteID;
//            }
//            set
//            {
//                this._ASCICSiteID = value;
//            }
//        }
//        #endregion
        
//    }
//}
