using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Validation
{
    public partial class Validator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      



        protected void btn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                
                lbl1.Text = "Data Validated Successfully";
                lbl1.ForeColor = System.Drawing.Color.Green;
            }
        }

     




    }
}