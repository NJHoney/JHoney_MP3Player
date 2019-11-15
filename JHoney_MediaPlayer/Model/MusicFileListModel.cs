using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JHoney_MediaPlayer.Model
{
    class MusicFileListModel:BindableBase
    {
        
        public MusicFileListModel(string FileFullPath)
        {
            FileName.FileName_Full = FileFullPath;
            FileName.MakeProperty();
        }

        public FileIOModel FileName
        {
            get { return _fileName; }
            set { _fileName = value; RaisePropertyChanged("FileName"); }
        }
        private FileIOModel _fileName = new FileIOModel();

    }
}
