using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoanBroker
{
    public partial class LoanRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(tb_Amount.Text);
            int loanDuration = Convert.ToInt32(tb_LoanDuration.Text);
            string CPR = tb_CPR.Text;

            //       int creditScore = creditService.creditScore(CPR);

            LoanRequestService1.LoanRequestServiceSoapClient lrs = new LoanRequestService1.LoanRequestServiceSoapClient();
            lrs.StartLoanRequest(amount, loanDuration, CPR);
        }
    }
}