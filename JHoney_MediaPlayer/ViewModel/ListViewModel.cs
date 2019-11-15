using JHoney_MediaPlayer.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JHoney_MediaPlayer.ViewModel
{
    class ListViewModel:BindableBase
    {

        #region 프로퍼티
        public TestCode TestCode
        {
            get { return _testCode; }
            set { _testCode = value; RaisePropertyChanged("_testCode"); }
        }
        private TestCode _testCode;
        public ObservableCollection<string> ButtonAddCommandList
        {
            get { return _buttonAddCommandList; }
            set { _buttonAddCommandList = value; RaisePropertyChanged("ButtonAddCommandList"); }
        }
        private ObservableCollection<string> _buttonAddCommandList = new ObservableCollection<string>();

        public ObservableCollection<string> ButtonRemoveCommandList
        {
            get { return _buttonRemoveCommandList; }
            set { _buttonRemoveCommandList = value; RaisePropertyChanged("ButtonRemoveCommandList"); }
        }
        private ObservableCollection<string> _buttonRemoveCommandList = new ObservableCollection<string>();

        

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; RaisePropertyChanged("SelectedIndex"); }
        }
        private int _selectedIndex = -1;

        #endregion
        #region 커맨드
        public DelegateCommand<RoutedEventArgs> ButtonAddCommand { get; private set; }
        public DelegateCommand<MouseButtonEventArgs> ListDoubleClickCommand { get; private set; }
        #endregion

        #region 초기화
        public ListViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {
            ButtonAddCommandList.Add("Files");
            ButtonAddCommandList.Add("Folder");
            ButtonAddCommandList.Add("Folder-Children");

            ButtonRemoveCommandList.Add("Remove-Selected");
            ButtonRemoveCommandList.Add("Remove-All");

            
        }

        void InitCommand()
        {
            ButtonAddCommand = new DelegateCommand<RoutedEventArgs>((param) => OnButtonAddCommand(param));
            ListDoubleClickCommand = new DelegateCommand<MouseButtonEventArgs>((param) => OnListDoubleClickCommand(param));
        }

        

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트

        private void OnButtonAddCommand(RoutedEventArgs param)
        {
            var a = (MahApps.Metro.Controls.SplitButton)param.Source;
            
            
            if(a.SelectedIndex == 0)
            {
                //Single File

                Microsoft.Win32.OpenFileDialog Dialog = new Microsoft.Win32.OpenFileDialog();
                Dialog.DefaultExt = ".txt";
                Dialog.Filter = "MP3 (*.mp3), MP4 (*.mp4)|*.mp3;*.mp4|All Files (*.*)|*.*";
                Dialog.Multiselect = true;
                bool? result = Dialog.ShowDialog();

                if (result == true)
                {
                    for (int iLoopCount = 0; iLoopCount < Dialog.FileNames.Count(); iLoopCount++)
                    {
                        TestCode.MusicFileList.Add(new MusicFileListModel(Dialog.FileNames[iLoopCount]));
                    }
                }


            }
            else if(a.SelectedIndex == 1)
            {
                //Single Folder
                Console.WriteLine(@"");
            }
            else if (a.SelectedIndex == 2)
            {
                //All Folder
                Console.WriteLine(@"");
            }
        }
        private void OnListDoubleClickCommand(MouseButtonEventArgs param)
        {
            var a = (DataGrid)param.Source;
            MusicFileListModel TempFileIO = a.SelectedCells[0].Item as MusicFileListModel;
            _testCode.OpenFile(TempFileIO.FileName.FileName_Full);
            _testCode.PlayIndex = SelectedIndex;
            _testCode.Play();
        }

        #endregion

    }

}
