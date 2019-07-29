using System.Drawing;

namespace ImageManipulation
{
    public partial class ImageProcessing
    {
        string _path = "";

        public Bitmap Image { get; private set; }

        public bool LoadFromFile(string path)
        {
            Image = new Bitmap(path);
            _path = path;
            return true;
        }

        public bool SaveToFile(string path)
        {
            Image.Save(path);
            return true;
        }

        public bool Reload()
        {
            return LoadFromFile(_path);
        }
    }
}
