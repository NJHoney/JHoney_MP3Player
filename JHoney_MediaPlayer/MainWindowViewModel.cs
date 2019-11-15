using JHoney_MediaPlayer.ViewModel;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_MediaPlayer
{
    class MainWindowViewModel:BindableBase
    {
        

        public TestCode TestCode
        {
            get { return _testCode; }
            set { _testCode = value; RaisePropertyChanged("TestCode"); }
        }
        private TestCode _testCode = new TestCode();

        #region 프로퍼티
        public ControllerViewModel ControllerViewModel
        {
            get { return _controllerViewModel; }
            set { _controllerViewModel = value; RaisePropertyChanged("ControllerViewModel"); }
        }
        private ControllerViewModel _controllerViewModel = new ControllerViewModel();

        public ListViewModel ListViewModel
        {
            get { return _listViewModel; }
            set { _listViewModel = value; RaisePropertyChanged("ListViewModel"); }
        }
        private ListViewModel _listViewModel= new ListViewModel();

        #endregion
        #region 커맨드
        //public RelayCommand<object> MyCommand { get; private set; }
        #endregion

        #region 초기화
        public MainWindowViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {
            ControllerViewModel.TestCode = TestCode;
            ListViewModel.TestCode = TestCode;
            ControllerViewModel.ListViewModel = ListViewModel;
            TestCode.ListViewModel = ListViewModel;
        }

        void InitCommand()
        {
            //MyCommand = new RelayCommand<object>((param) => OnMyCommand(param));
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트
        /*
        private void OnMyCommand(object param)
            {

            }
            */
        #endregion

    }
}
