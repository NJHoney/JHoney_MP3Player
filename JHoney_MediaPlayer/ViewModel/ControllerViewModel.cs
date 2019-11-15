using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        public double TempVolume
        {
            get { return _tempVolume; }
            set { _tempVolume = value; RaisePropertyChanged("TempVolume"); }
        }
        private double _tempVolume = 1;

        public PackIconMaterialKind VolumeIcon
        {
            get { return _volumeIcon; }
            set { _volumeIcon = value; RaisePropertyChanged("VolumeIcon"); }
        }
        private PackIconMaterialKind _volumeIcon = PackIconMaterialKind.VolumeHigh;

        public bool IsUseShuffle
        {
            get { return _isUseShuffle; }
            set { _isUseShuffle = value; RaisePropertyChanged("IsUseShuffle"); }
        }
        private bool _isUseShuffle = false;

        public PackIconMaterialKind ShuffleIcon
        {
            get { return _shuffleIcon; }
            set { _shuffleIcon = value; RaisePropertyChanged("ShuffleIcon"); }
        }
        private PackIconMaterialKind _shuffleIcon = PackIconMaterialKind.ShuffleDisabled;

        #endregion
        #region 커맨드
        public DelegateCommand<object> VolumeChangeCommand { get; private set; }
        public DelegateCommand<object> PlayCommand { get; private set; }
        public DelegateCommand<object> StopCommand { get; private set; }
        public DelegateCommand<object> MuteCommand { get; private set; }
        public DelegateCommand<object> ProgressMouseUPCommand { get; private set; }
        public DelegateCommand<object> ForwardBackwardCommand { get; private set; }
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
            MuteCommand = new DelegateCommand<object>((param) => OnMuteCommand(param));
            ProgressMouseUPCommand = new DelegateCommand<object>((param) => OnProgressMouseUPCommand(param));
            ForwardBackwardCommand = new DelegateCommand<object>((param) => OnForwardBackwardCommand(param));
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


        private void OnMuteCommand(object param)
        {
            //TempVolume = TestCode.VolumeCurrent;
            TestCode.MediaPlayer.IsMuted= !TestCode.MediaPlayer.IsMuted;
            if(TestCode.MediaPlayer.IsMuted)
            {
                VolumeIcon = PackIconMaterialKind.VolumeOff;
            }
            else
            {
                VolumeIcon = PackIconMaterialKind.VolumeHigh;
            }

        }

        private void OnProgressMouseUPCommand(object param)
        {
            if(TestCode.Duration=="00:00:00")
            {
                return;
            }
            var MouseCurrent = (MouseButtonEventArgs)param;
            var TempControl = (MetroProgressBar)MouseCurrent.Source;
            Point Temp = MouseCurrent.GetPosition((IInputElement)MouseCurrent.Source);
            double CurrentRatio = (Temp.X / TempControl.ActualWidth);
            
            TestCode.ProgressCurrent = (int)(CurrentRatio * 1000);
            TestCode.MediaPlayer.Position = TimeSpan.FromTicks((long)((double)TestCode.MediaPlayer.NaturalDuration.TimeSpan.Ticks * (double)CurrentRatio));
            //Mouse.GetPosition()
        }


        private void OnForwardBackwardCommand(object param)
        {
            if(param.ToString()=="Forward")
            {
                if(TestCode.MusicFileList.Count>ListViewModel.SelectedIndex+1)
                {
                    TestCode.PlayIndex = ListViewModel.SelectedIndex + 1;
                    ++ListViewModel.SelectedIndex;
                    TestCode.Play(TestCode.MusicFileList[ListViewModel.SelectedIndex].FileName.FileName_Full);
                }
            }
            else
            {
                if (ListViewModel.SelectedIndex>0)
                {
                    TestCode.PlayIndex = ListViewModel.SelectedIndex-1;
                    --ListViewModel.SelectedIndex;
                    TestCode.Play(TestCode.MusicFileList[ListViewModel.SelectedIndex].FileName.FileName_Full);
                }

            }
        }

        #endregion

    }
}
