using WpfApp1.ViewModels;
using WpfApp1.DataProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public enum NavigationSide
    {
        Left,
        Right
    }
    internal class UserViewModel : BaseViewModel
    {
        public UserViewModel(IUserDataProvider userDataProvider)
        {
            _userDataProvider = userDataProvider;
            //AddCommand = new DelegateCommand(Add);
            //MoveNavigationCommand = new DelegateCommand(MoveNavigation);
            //DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }
        private readonly IUserDataProvider _userDataProvider;
        private UserItemViewModel? _selectedUser;
        private NavigationSide _navigationSide;
        public ObservableCollection<UserItemViewModel> UsersObserver { get; } = new();
        public UserItemViewModel? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                RaisePropertyChanged();
            }
        }
        public NavigationSide NavigationSide
        {
            get => _navigationSide;
            private set
            {
                _navigationSide = value;
                RaisePropertyChanged();
            }
        }
        public async override Task LoadAsync()
        {
            if (UsersObserver.Any())
            {
                return;
            }

            var users = await _userDataProvider.GetAllAsync();
            if (users != null)
            {
                foreach (var user in users)
                {
                    UsersObserver.Add(new UserItemViewModel(user));
                }
            }
        }
        internal void Delete()
        {
            if(_selectedUser != null)
            {
                var toBeDeleted = _selectedUser;
                UsersObserver.Remove(toBeDeleted);
                _userDataProvider.Delete(toBeDeleted.model);
            }

        }
        internal void Add()
        {
            throw new NotImplementedException();
            //var district = new District { Name = "New" };
            //var viewModel = new DistrictItemViewModel(district);
            //DistrictsObserver.Add(viewModel);
            //SelectedDistrict = viewModel;
        }

        internal void MoveNavigation()
        {
            NavigationSide = NavigationSide == NavigationSide.Left
            ? NavigationSide.Right
              : NavigationSide.Left;
        }
    }
}
