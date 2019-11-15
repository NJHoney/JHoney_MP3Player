using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHoney_MediaPlayer.ViewModel
{
    class ControllerViewModel : BindableBase
    {
        public ListViewModel ListViewModel;
        public TestCode TestCode
        {
            get { return _testCode; }
            set { _testCode = value; RaisePropertyChanged("_testCode"); }
        }
        private TestCode _testCode;
        #region 프로퍼티

        public int MyVariable
        {
            get { return _myVariable; }
            set { _myVariable = value; RaisePropertyChanged("MyVariable"); }
        }
        private int _myVariable;

        #endregion
        #region 커맨드
        public DelegateCommand<object> VolumeChangeCommand { get; private set; }
        public DelegateCommand<object> PlayCommand { get; private set; }
        public DelegateCommand<object> StopCommand { get; private set; }
        #endregion

        #region 초기화
        public ControllerViewModel()
        {
            InitData();
            InitCommand();
            InitEvent();
        }

        void InitData()
        {


        }

        void InitCommand()
        {
            VolumeChangeCommand = new DelegateCommand<object>((param) => OnVolumeChangeCommand(param));
            PlayCommand = new DelegateCommand<object>((param) => OnPlayCommand(param));
            StopCommand = new DelegateCommand<object>((param) => OnStopCommand(param));
        }

        void InitEvent()
        {

        }
        #endregion

        #region 이벤트

        private void OnVolumeChangeCommand(object param)
        {
            TestCode.SetVolume((double)param);
        }

        private void OnPlayCommand(object param)
        {
            if(TestCode.MusicFileList.Count<1)
            {
                return;
            }

            if(ListViewModel.SelectedIndex==-1)
            {
                TestCode.Play(TestCode.MusicFileList[0].FileName.FileName_Full);
                TestCode.PlayIndex = 0;
            }
            else
            {
                TestCode.Play(TestCode.MusicFileList[ListViewModel.SelectedIndex].FileName.FileName_Full);
                TestCode.PlayIndex = ListViewModel.SelectedIndex;
            }
        }

        private void OnStopCommand(object param)
        {
            if(param.ToString()=="Pause")
            {
                TestCode.Pause();
                
            }
            else if(param.ToString()=="Stop")
            {
                TestCode.Stop();
            }
        }

        #endregion

    }
}
