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
using DomainModel;
using WpfApp1.ViewModels;
using WpfApp1.DataProviders;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for DistrictView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        private UserViewModel _viewModel;
        public UserView()
        {
            InitializeComponent();
            _viewModel = new UserViewModel(new ApiUserDataProvider());
            DataContext = _viewModel;
            Loaded += UsersView_Loaded;
        }
        private async void UsersView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void ButtonMoveNavigation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MoveNavigation();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Add();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Delete();
        }
    }
}
