using Biden.Model;
using Biden.View;
using Biden.ViewModel;
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

        public void SetItem(string str)
        {
            if(str == "")
            {
                return "";
            }

            string res = "";

            List<string> list = ModelFunc3.getInstance.StrList;

            if (list.Contains(str) != true) {
                list.Add(str);
            }
            ModelFunc3.getInstance.StrList = list;
            DispatcherService.Invoke((System.Action)(() =>
            {
                // your logic
                //ViewModelFunc3.optionObjectCollection.Add(new ViewModelFunc3.MacroInfo2() { No = "a", Str = "bbb"});
                FuncWindow3.getInstance.SetListView();
            }));


            
            //MessageBox.Show("getsetItem", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            return res;
        }
    }
}
