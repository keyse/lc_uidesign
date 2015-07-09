using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using UIV2.classes;
using System.Windows.Input;

namespace UIV2.Model
{
    
    public class Item : Bindable
    {
        public Item(string name, decimal price, string imagePath)
        {
            Name = name;
            Price = price;
            ImagePath = imagePath;
        }

        public Category OwningCategory
        {
            get
            {
                return Get<Category>();
            }
            set
            {
                Set<Category>(value);
            }
        }

        public string Name
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set<string>(value);
            }
        }

        public decimal Price
        {
            get
            {
                return Get<decimal>();
            }
            set
            {
                Set<decimal>(value);
            }
        }

        /// <summary>
        /// removes items from inout category list
        /// </summary>
        public ICommand Remove
        {
            get
            {
                return new RelayCommand(removeMeFromInputCategory);
            }
        }

        public Image Pic
        {
            get
            {
                return Get<Image>();
            }
            private set
            {
                Set<Image>(value);
            }
        }

        public string ImagePath
        {
            get
            {
                return Get<string>();
            }
            private set
            {
                Set<string>(value);
                setImage();
            }
        }

        void setImage()
        {
            if (!File.Exists(ImagePath)) return;
            using (StreamReader sr = new StreamReader(ImagePath))
            {
                Pic = Image.FromStream(sr.BaseStream);
            }
        }

        void removeMeFromInputCategory()
        {
            OwningCategory.Items.Remove(this);
            OnPropertyChanged("OwningCategory.Items");
        }
    }
}
