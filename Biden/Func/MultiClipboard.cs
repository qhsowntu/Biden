using Biden.Model;
using Biden.View;
using Biden.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class MultiClipboard
    {

        private static FuncWindow3_ClipList tempWindow;
        public MultiClipboard()
        {

        }


        //클립보드 ListView의 Item을 추가 
        public void SetItem(string str)
        {
            if (str == "")
            {
                return;
            }


            List<string> list = ModelFunc3.getInstance.StrList;

            if (ModelFunc3.getInstance.IsCheckedDupOpt == true && list.Contains(str) == true)
            {
            }
            else
            {
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
        }

        //클립보드 ListView의 Item을 추출
        public static string GetItem()
        {
            string res = "";
            if (ModelFunc3.getInstance.TheSelectedItem == ModelFunc3.getInstance.Source.ElementAt(0)) //큐
            {
                res = ModelFunc3.getInstance.StrList.ElementAt(0);
                ModelFunc3.getInstance.StrList.RemoveAt(0);
            }
            else if (ModelFunc3.getInstance.TheSelectedItem == ModelFunc3.getInstance.Source.ElementAt(1)) //스택
            {
                res = ModelFunc3.getInstance.StrList.ElementAt(ModelFunc3.getInstance.StrList.Count - 1);
                ModelFunc3.getInstance.StrList.RemoveAt(ModelFunc3.getInstance.StrList.Count - 1);
            }
            else if (ModelFunc3.getInstance.TheSelectedItem == ModelFunc3.getInstance.Source.ElementAt(2))  //선택창
            {
                if (tempWindow == null)
                {
                    tempWindow = FuncWindow3_ClipList.getInstance;
                }
                //바인딩 문제로 창 위치 임시방편 처리
                FuncWindow3_ClipList.getInstance.setPos();
                //tempWindow.ShowDialog();
                res = ModelFunc3.getInstance.SelectedString;
                //MessageBox.Show("2", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                return res;
                //MessageBox.Show("선택창");
            }

            
            //ModelFunc3.;

            return res;
        }
    }
}
