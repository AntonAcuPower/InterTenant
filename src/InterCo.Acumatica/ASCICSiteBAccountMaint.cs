using System;
using PX.Data;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace ASCInterCo
{
    public class ASCICSiteBAccountMaint : PXGraph<ASCICSiteBAccountMaint, ASCICSiteBAccount>
    {
        #region Views
        [PXViewName(Messages.ICSiteBAccount)]
        public PXSelect<ASCICSiteBAccount> ICSiteBAccount;

        public PXSelect<ASCICSiteBAccount, Where<ASCICSiteBAccount.iCsiteID, Equal<Current<ASCICSiteBAccount.iCsiteID>>, And<ASCICSiteBAccount.bAccountID, Equal<Current<ASCICSiteBAccount.bAccountID>>>>> CurrentDocument;

        [PXViewName(Messages.ICSiteBAccountSettings)]
        public PXSelect<ASCICSiteBAccountSettings, Where<ASCICSiteBAccountSettings.sourceSiteID, Equal<Current<ASCICSiteBAccount.iCsiteID>>, And<ASCICSiteBAccountSettings.bAccountID, Equal<Current<ASCICSiteBAccount.bAccountID>>>>> Settings;

        #endregion
    }
}
