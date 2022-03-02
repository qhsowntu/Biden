using Biden.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Model
{
    class ModelFunc3
    {
        private List<Func3RuleClass> ruleList;
        private List<string> strList;

        private static ModelFunc3 instance = null;
        private bool _isChecked03 = false;


        private string _theSelectedItem;
        private List<string> _source;

        private string selectedString;

        private ModelFunc3()
        {
            RuleList = new List<Func3RuleClass>();
            strList = new List<string>();
            Source = new List<string> {
                "[FIFO] Using The Clipboard as a Queue",
                "[LIFO] Using The Clipboard as a Stack",
                "[Select] Using The Clipboard as a Dictionary"};
            _theSelectedItem = _source.ElementAt(0);
        }

        public static ModelFunc3 getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelFunc3();
                }
                return instance;
            }
        }


        internal List<Func3RuleClass> RuleList { get => ruleList; set => ruleList = value; }
        public bool IsChecked03 { get => _isChecked03; set => _isChecked03 = value; }
        public List<string> StrList { get => strList; set => strList = value; }
        public string TheSelectedItem { get => _theSelectedItem; set => _theSelectedItem = value; }
        public List<string> Source { get => _source; set => _source = value; }
        public string SelectedString { get => selectedString; set => selectedString = value; }
    }
}
