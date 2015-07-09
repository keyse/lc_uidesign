using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIV2.classes;
using UIV2.Model;

namespace UIV2.ViewModels
{
    public class MainWindowViewModel : Bindable
    {
        public string ContainerName
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

        public string ContainerNumber
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

        public ObservableCollection<Category> Categories
        {
            get
            {
                return Get<ObservableCollection<Category>>();
            }
            set
            {
                Set<ObservableCollection<Category>>(value);
            }
        }

        public IList<Category> ChosenCategories
        {
            get
            {
                return new List<Category>(Categories.Where(c => c.IsChecked));
            }
        }

        public NotifyObservableCollection<Category> InputCategories
        {
            get
            {
                return Get<NotifyObservableCollection<Category>>();
            }
            set
            {
                Set<NotifyObservableCollection<Category>>(value);
            }
        }

        #region temp code

        void InitCategories()
        {
            string basePath = Environment.CurrentDirectory;

            Categories = new NotifyObservableCollection<Category>();
            Category cat1 = new Category("Category1") ;
            cat1.Items.Add(new Item("Jerry",1.5m, string.Format("{0}\\images\\jerry.png",basePath)));
            cat1.Items.Add(new Item("Tom", 1.25m, string.Format("{0}\\images\\tom.png", basePath)));
            Categories.Add(cat1);


            Category cat2 = new Category("Category2");
            cat2.Items.Add(new Item("mario", 2.5m, string.Format("{0}\\images\\mario.png", basePath)));
            cat2.Items.Add(new Item("speedy", 2.75m, string.Format("{0}\\images\\speedy.png", basePath)));
            Categories.Add(cat2);

            Category cat3 = new Category("Category3");
            cat3.Items.Add(new Item("Woody Wood Pecker", 3.5m, string.Format("{0}\\images\\pecker.png", basePath)));
            cat3.Items.Add(new Item("Bugz Bunny", 1.25m, string.Format("{0}\\images\\bugz.png", basePath)));
            Categories.Add(cat3);

            Category cat4 = new Category("Category4");
            cat4.Items.Add(new Item("Burger", 5.5m, string.Format("{0}\\images\\food1.jpg", basePath)));
            cat4.Items.Add(new Item("Strawberry", 2.95m, string.Format("{0}\\images\\food2.jpg", basePath)));
            cat4.Items.Add(new Item("Cherry", 2.15m, string.Format("{0}\\images\\food3.jpg", basePath)));
            Categories.Add(cat4);
        }

        #endregion

        public MainWindowViewModel()
        {
            InitCategories();
            InputCategories = new NotifyObservableCollection<Category>();
            Categories.CollectionChanged += Categories_CollectionChanged;
        }

        void Categories_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IList<Category> chosenCategories = ChosenCategories;
            if (chosenCategories.Count == 0)
            {
                InputCategories.Clear();
                return;
            }


            if (InputCategories.Count > chosenCategories.Count)
            {
                //a category was removed. Find which one, and remove from InputCategories
                foreach(Category cat in Categories.Where(c => !c.IsChecked))
                {
                    Category catToRemove = InputCategories.FirstOrDefault(c => c.Name.Equals(cat.Name));
                    if (catToRemove != null)
                    {
                        InputCategories.Remove(catToRemove);
                        break;
                    }
                }
            }
            else
            {
                foreach (Category cat in chosenCategories)
                {
                    if ((cat.IsChecked) && !InputCategories.Any(existingCat => existingCat.Name.Equals(cat.Name)))
                    {
                        InputCategories.Add(new Category(cat.Name));
                        break;
                    }
                }
            }
            
        }
    }

}
