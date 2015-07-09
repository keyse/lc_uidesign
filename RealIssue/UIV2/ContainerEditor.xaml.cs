using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TileView;
using UIV2.Model;
using UIV2.ViewModels;

namespace UIV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            VM = new MainWindowViewModel();
        }

        public MainWindowViewModel VM
        {
            get
            {
                return this.DataContext as MainWindowViewModel;
            }
            private set
            {
                this.DataContext = value;
            }
        }

    #region "drag and drop code"

        Point? startPoint = null;

        bool okToDrag(Point starting, Point current)
        {
            Vector diff = starting - current;
            return diff.LengthSquared > 75;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(outerGrid);
            if (!((FrameworkElement)sender).IsMouseCaptured) ((FrameworkElement)sender).CaptureMouse();
        }

        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            try
            {
                if (okToDrag(startPoint.Value, e.GetPosition(outerGrid)))
                {
                    ((FrameworkElement)sender).ReleaseMouseCapture();
                    RadTreeView tree = sender as RadTreeView;
                    Item item = FindTreeItem(e.OriginalSource);

                    if (item == null) return;
                    else
                    {
                        DataObject dragData = new DataObject(typeof(Item), item);
                        DragDrop.DoDragDrop(categoryTree, dragData, DragDropEffects.Copy);
                    }
                    //startPoint = null;
                    //if (((FrameworkElement)sender).IsMouseCaptured) ((FrameworkElement)sender).ReleaseMouseCapture();
                }
                else
                {
                    ((FrameworkElement)sender).ReleaseMouseCapture();
                    //startPoint = null;
                }
                //((FrameworkElement)sender).ReleaseMouseCapture();
                startPoint = null;
            }
            finally
            {
                
            }
        }

        private static Item FindTreeItem(object current)
        {
            do
            {
                if (current is RadTreeViewItem) return ((RadTreeViewItem)current).Item as Item;
                else current = VisualTreeHelper.GetParent((DependencyObject)current);
            } while (current != null);
            return null;
        }

        private static Category FindInputCategory(object current)
        {
            if (current == null) return null;
            do
            {
                //if (current == null) return null;
                if (current is RadTileViewItem) return ((RadTileViewItem)current).DataContext as Category;
                else current = VisualTreeHelper.GetParent((DependencyObject)current);
            } while (current != null);
            return null;
        }

        private void RadFluidContentControl_Drop(object sender, DragEventArgs e)
        {
            Item item = (Item)e.Data.GetData(typeof(Item));//item we dragged
            Category inputCategory = FindInputCategory(Mouse.DirectlyOver);//the category over which we dragged
            if (inputCategory != null) inputCategory.AddItem(item);
        }

    #endregion

    }
}
