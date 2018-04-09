using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation
{
    public partial class ImageProcessing
    {
        private Bitmap _bitmap;
        string _path = "";

        public Bitmap Image { get { return _bitmap; } }

        public bool LoadFromFile(string path)
        {
            _bitmap = new Bitmap(path);
            _path = path;
            return true;
        }

        public bool SaveToFile(string path)
        {
            _bitmap.Save(path);
            return true;
        }

        public bool Reload()
        {
            return LoadFromFile(_path);
        }
    }
}
