﻿using Biden.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biden.View
{
    /// <summary>
    /// FuncWindow1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FuncWindow1 : BaseWindow
    {
        private static FuncWindow1 instance = null;


        private FuncWindow1()
        {
            InitializeComponent();
        }

        public static FuncWindow1 getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FuncWindow1();
                }
                return instance;
            }
        }

    }
}
