using System;
using PX.Data;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace ASCInterCo
{
    public class ASCICSiteBAccountSettingsMaint : PXGraph<ASCICSiteBAccountSettingsMaint, ASCICSiteBAccountSettings>
    {
        #region Views
        public PXSelect<ASCICSiteBAccountSettings> ASCICSiteBAccountSettings;
        #endregion
    }
}
