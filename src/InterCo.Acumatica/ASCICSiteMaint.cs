using System;
using PX.Data;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace ASCInterCo
{
    public class ASCICSiteMaint : PXGraph<ASCICSiteMaint, ASCICSite>
    {

        #region Views
        [PXViewName(Messages.ICSite)]
        public PXSelect<ASCICSite> ICSite;
        #endregion

        #region Event Handlers

        #endregion
    }
}