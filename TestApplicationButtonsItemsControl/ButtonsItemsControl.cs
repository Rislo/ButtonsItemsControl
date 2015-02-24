using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApplicationButtonsItemsControl
{
    [ContentProperty("Items")] 
    public class ButtonsItemsControl : ItemsControl
    {
        static ButtonsItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonsItemsControl), new FrameworkPropertyMetadata(typeof(ButtonsItemsControl)));
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            RefreshComponent();
            DefineItemsInMenu();
        }


        public void RefreshComponent()
        {
            foreach (FrameworkElement item in Items)
            {
                if (!_listElement.Contains(item))
                {
                    item.Loaded += Item_Loaded;
                    _listElement.Add(item);
                }
            }

            if (Items.Count != _listElement.Count)
            {
                for (int i = 0; i < _listElement.Count; i++)
                {
                    if (!Items.Contains(_listElement[i]))
                    {
                        _listElement[i].Loaded -= Item_Loaded;
                        AdvancedMenuItems.Remove(_listElement[i]);
                        BasedMenuItems.Remove(_listElement[i]);
                        _listElement.RemoveAt(i);
                        DefineItemsInMenu();
                    }
                }
            }
        }

        private void Item_Loaded(object sender, RoutedEventArgs e)
        {
            DefineItemsInMenu();
        }

        private void DefineItemsInMenu()
        {
            double intermediateHeight = 0;
            int nbVisibleElement = 0;

            foreach (FrameworkElement element in _listElement)
            {
                intermediateHeight += element.ActualHeight + BaseButtonsMargin.Bottom + BaseButtonsMargin.Top;

                if (ActualHeight >= intermediateHeight)
                {
                    nbVisibleElement++;
                }
                else
                {
                    break;
                }
            }

            if (_listElement.Count > nbVisibleElement)
            {
                MenuVisibility = Visibility.Visible;
                nbVisibleElement--;
            }
            else
            {
                MenuVisibility = Visibility.Collapsed;
            }


            for (int i = 0; i < _listElement.Count; i++)
            {
                if (i < nbVisibleElement)
                {
                    if (AdvancedMenuItems.Contains(_listElement[i]))
                    {
                        AdvancedMenuItems.Remove(_listElement[i]);
                    }

                    if (!BasedMenuItems.Contains(_listElement[i]))
                    {
                        _listElement[i].Margin = BaseButtonsMargin;
                        FrameworkElement element = _listElement[i];
                        BasedMenuItems.Add(element);
                    }
                }
                else
                {
                    if (BasedMenuItems.Contains(_listElement[i]))
                    {
                        BasedMenuItems.Remove(_listElement[i]);
                    }

                    if (!AdvancedMenuItems.Contains(_listElement[i]))
                    {
                        _listElement[i].Margin = SecondaryButtonsMargin;
                        AdvancedMenuItems.Add(_listElement[i]);
                    }
                }
            }
        }

        private List<FrameworkElement> _listElement = new List<FrameworkElement>();

        public ObservableCollection<FrameworkElement> AdvancedMenuItems
        {
            get { return (ObservableCollection<FrameworkElement>)GetValue(AdvancedMenuItemsProperty); }
            set { SetValue(AdvancedMenuItemsProperty, value); }
        }
        public static readonly DependencyProperty AdvancedMenuItemsProperty =
            DependencyProperty.Register("AdvancedMenuItems", typeof(ObservableCollection<FrameworkElement>), typeof(ButtonsItemsControl),
            new PropertyMetadata(new ObservableCollection<FrameworkElement>()));

        public ObservableCollection<FrameworkElement> BasedMenuItems
        {
            get { return (ObservableCollection<FrameworkElement>)GetValue(BasedMenuItemsProperty); }
            set { SetValue(BasedMenuItemsProperty, value); }
        }
        public static readonly DependencyProperty BasedMenuItemsProperty =
            DependencyProperty.Register("BasedMenuItems", typeof(ObservableCollection<FrameworkElement>), typeof(ButtonsItemsControl),
            new PropertyMetadata(new ObservableCollection<FrameworkElement>()));

        public Thickness BaseButtonsMargin
        {
            get { return (Thickness)GetValue(BaseButtonsMarginProperty); }
            set { SetValue(BaseButtonsMarginProperty, value); }
        }
        public static readonly DependencyProperty BaseButtonsMarginProperty =
            DependencyProperty.Register("BaseButtonsMargin", typeof(Thickness), typeof(ButtonsItemsControl), new FrameworkPropertyMetadata(new Thickness(0, 10, 0, 10), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, BaseButtonsMarginChangedCallback));

        private static void BaseButtonsMarginChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ButtonsItemsControl).DefineItemsInMenu();
        }

        public Thickness SecondaryButtonsMargin
        {
            get { return (Thickness)GetValue(SecondaryButtonsMarginProperty); }
            set { SetValue(SecondaryButtonsMarginProperty, value); }
        }
        public static readonly DependencyProperty SecondaryButtonsMarginProperty =
            DependencyProperty.Register("SecondaryButtonsMargin", typeof(Thickness), typeof(ButtonsItemsControl), new FrameworkPropertyMetadata(new Thickness(5, 0, 5, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SecondaryButtonsMarginChangedCallback));

        private static void SecondaryButtonsMarginChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ButtonsItemsControl).DefineItemsInMenu();
        }

        public Visibility MenuVisibility
        {
            get { return (Visibility)GetValue(MenuVisibilityProperty); }
            set { SetValue(MenuVisibilityProperty, value); }
        }
        public static readonly DependencyProperty MenuVisibilityProperty =
            DependencyProperty.Register("MenuVisibility", typeof(Visibility), typeof(ButtonsItemsControl));
    }
}
