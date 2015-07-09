using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIV2.classes;

namespace UIV2.Model
{
    public class Category : Bindable
    {

        public Category(string name)
        {
            Name = name;
            Items = new ObservableCollection<Item>();
        }

        public bool IsChecked
        {
            get
            {
                return Get<bool>();
            }
            set
            {
                Set<bool>(value);
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

        public ObservableCollection<Item> Items
        {
            get
            {
                return Get<ObservableCollection<Item>>();
            }
            set
            {
                Set<ObservableCollection<Item>>(value);
            }
        }

        public void AddItem(Item item) 
        {
            item.OwningCategory = this;
            Items.Add(item);
        }
    }
}
