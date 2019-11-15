using JHoney_MediaPlayer.Model;
using JHoney_MediaPlayer.ViewModel;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace JHoney_MediaPlayer
{
    class TestCode:BindableBase
    {
        DispatcherTimer chkTime;
        public TestCode()
        {
            chkTime = new DispatcherTimer();
            chkTime.Tick += new EventHandler(GetCurrentDuration);
            chkTime.Interval = new TimeSpan(1000);
            chkTime.Start();
        }
        public ListViewModel ListViewModel;
        
        public MediaPlayer MediaPlayer
        {
            get { return _mediaPlayer; }
            set { _mediaPlayer = value; RaisePropertyChanged("MediaPlayer"); }
        }
        private MediaPlayer _mediaPlayer = new MediaPlayer();

        public ObservableCollection<MusicFileListModel> MusicFileList
        {
            get { return _musicFileList; }
            set { _musicFileList = value; RaisePropertyChanged("MusicFileList"); }
        }
        private ObservableCollection<MusicFileListModel> _musicFileList = new ObservableCollection<MusicFileListModel>();

        public string CurrentPlayName
        {
            get { return _currentPlayName; }
            set { _currentPlayName = value; RaisePropertyChanged("CurrentPlayName"); }
        }
        private string _currentPlayName = "";

        public int PlayIndex=-1;

        public string Duration
        {
            get { return _duration; }
            set { _duration = value; RaisePropertyChanged("Duration"); }
        }
        private string _duration = "00:00:00";

        public string CurrentDuration
        {
            get { return _currentDuration; }
            set { _currentDuration = value; RaisePropertyChanged("CurrentDuration"); }
        }
        private string _currentDuration = "00:00:00";

        public int ProgressCurrent
        {
            get { return _progressCurrent; }
            set { _progressCurrent = value; RaisePropertyChanged("ProgressCurrent"); }
        }
        private int _progressCurrent = 0;

        public double VolumeCurrent
        {
            get { return _volumeCurrent; }
            set { _volumeCurrent = value; RaisePropertyChanged("VolumeCurrent"); }
        }
        private double _volumeCurrent = 1;

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set { _isPlaying = value; RaisePropertyChanged("IsPlaying"); }
        }
        private bool _isPlaying = false;

        public void OpenFile(string FileFullPath)
        {
            MediaPlayer.Open(new Uri(FileFullPath));
            Duration = MediaPlayer.NaturalDuration.ToString().Substring(0, 8);
            
        }

        public void SetVolume(double SetVolume)
        {
            MediaPlayer.Volume = SetVolume;
        }

        public void Play()
        {
            Duration = "00:00:00";
            MediaPlayer.Play();
            IsPlaying = true;
            CurrentPlayName = MediaPlayer.Source.OriginalString.Substring(MediaPlayer.Source.OriginalString.LastIndexOf("\\") + 1, MediaPlayer.Source.OriginalString.LastIndexOf(".") - MediaPlayer.Source.OriginalString.LastIndexOf("\\") - 1);
            while (true)
            {
                Duration = MediaPlayer.NaturalDuration.ToString().Substring(0, 8);
                if (Duration !="Automati")
                {
                    break;
                }
            }
            
        }

        public void Play(string FileFullPath)
        {
            if(MediaPlayer.Source!=null)
            {
                if (MediaPlayer.Source.LocalPath == FileFullPath)
                {
                    if (MediaPlayer.Position.Ticks == 0)
                    {
                        MediaPlayer.Open(new Uri(FileFullPath));
                        Duration = "00:00:00";
                        MediaPlayer.Play();
                        IsPlaying = true;
                        CurrentPlayName = MediaPlayer.Source.OriginalString.Substring(MediaPlayer.Source.OriginalString.LastIndexOf("\\") + 1, MediaPlayer.Source.OriginalString.LastIndexOf(".") - MediaPlayer.Source.OriginalString.LastIndexOf("\\") - 1);
                        while (true)
                        {
                            Duration = MediaPlayer.NaturalDuration.ToString().Substring(0, 8);
                            
                            if (Duration != "Automati")
                            {
                                break;
                            }
                        }
                        
                    }
                    else
                    {
                        MediaPlayer.Play();
                        IsPlaying = true;
                        CurrentPlayName = MediaPlayer.Source.OriginalString.Substring(MediaPlayer.Source.OriginalString.LastIndexOf("\\") + 1, MediaPlayer.Source.OriginalString.LastIndexOf(".") - MediaPlayer.Source.OriginalString.LastIndexOf("\\") - 1);
                    }
                }
                else
                {
                    MediaPlayer.Open(new Uri(FileFullPath));
                    Duration = "00:00:00";
                    MediaPlayer.Play();                    
                    IsPlaying = true;
                    CurrentPlayName = MediaPlayer.Source.OriginalString.Substring(MediaPlayer.Source.OriginalString.LastIndexOf("\\") + 1, MediaPlayer.Source.OriginalString.LastIndexOf(".") - MediaPlayer.Source.OriginalString.LastIndexOf("\\") - 1);
                }
            }
            else
            {
                MediaPlayer.Open(new Uri(FileFullPath));
                Duration = "00:00:00";
                MediaPlayer.Play();
                CurrentPlayName = MediaPlayer.Source.OriginalString.Substring(MediaPlayer.Source.OriginalString.LastIndexOf("\\") + 1, MediaPlayer.Source.OriginalString.LastIndexOf(".") - MediaPlayer.Source.OriginalString.LastIndexOf("\\") - 1);
                IsPlaying = true;
                while (true)
                {
                    Duration = MediaPlayer.NaturalDuration.ToString().Substring(0, 8);
                    
                    if (Duration != "Automati")
                    {
                        break;
                    }
                }
                
            }
            
            
        }

        private void GetCurrentDuration(object sender, EventArgs e)
        {
            CurrentDuration = MediaPlayer.Position.ToString().Substring(0, 8);
            if(CurrentDuration!="Automati" && MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                ProgressCurrent = (int)((double)1000 * ((double)MediaPlayer.Position.Ticks / (double)MediaPlayer.NaturalDuration.TimeSpan.Ticks));
            }

            if(ProgressCurrent==1000)
            {
                //Next
                int NowIndex = MusicFileList.IndexOf(MusicFileList.Where(x => x.FileName.FileName_Full == MediaPlayer.Source.LocalPath).First());
                if(MusicFileList.Count==NowIndex+1)
                {
                    //리스트 마지막이었음 이후 처리
                }
                else if(MusicFileList.Count>NowIndex+1)
                {
                    ProgressCurrent = 0;
                    //리스트 아직 남아있음
                    ListViewModel.SelectedIndex = NowIndex+1;
                    Play(MusicFileList[NowIndex + 1].FileName.FileName_Full);
                }
            }
            
        }

        public void Pause()
        {
            IsPlaying = false;
            MediaPlayer.Pause();
        }

        public void Stop()
        {
            IsPlaying = false;
            MediaPlayer.Stop();
            chkTime.Stop();
            ProgressCurrent = 0;
            CurrentDuration = "00:00:00";
        }
    }
}
