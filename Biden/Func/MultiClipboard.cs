using Biden.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class MultiClipboard
    {
        public MultiClipboard()
        {

        }

        public string GetSetItem(string str)
        {
            string res = "";

            List<string> list = ModelFunc3.getInstance.StrList;
            if (true)
            {

            }
            list.Add(str);

            //MessageBox.Show("getsetItem", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            return res;
        }

    }
}
