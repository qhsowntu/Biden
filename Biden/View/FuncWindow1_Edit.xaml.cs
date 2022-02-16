﻿using Biden.Func;
using Biden.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class FuncWindow1_Edit : BaseWindow
    {
        private static FuncWindow1_Edit instance = null;
        private readonly ViewModelFunc1_Edit _viewModel;

        private FuncWindow1_Edit()
        {
            InitializeComponent();
            _viewModel = new ViewModelFunc1_Edit();
            DataContext = _viewModel;
        }
        public static FuncWindow1_Edit getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FuncWindow1_Edit();
                }
                return instance;
            }
        }

        public void show()
        {
            _viewModel.setInfo();
            base.ShowDialog();
        }


    }
}
