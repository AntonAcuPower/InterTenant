//using System;
//using PX.Data;
//using System.Collections;
//using System.Collections.Generic;
//using PX.Common;
//using PX.Objects.CM;
//using PX.Objects.GL;
//using PX.Objects.CR;
//using PX.Objects.EP;
//using PX.Objects.IN;
//using UploadFile = PX.SM.UploadFile;
//using System.Linq;
//using PX.Objects;
//using PX.Objects.CS;


//namespace ASCInterCo
//{
//    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod 
//    // because this extension should be always enabled
//    public class ASCBranchMaintICExt : PXGraphExtension<BranchMaint>
//    {

//        public PXSelect<ASCICSite, Where<ASCICSite.iCsiteID, Equal<Current<BAccountICExt.aSCICSiteID>>>> CurrentICSite;

//        #region Event Handlers

//        #endregion
//    }
//}