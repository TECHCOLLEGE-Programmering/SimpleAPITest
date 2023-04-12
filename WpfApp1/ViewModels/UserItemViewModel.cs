
using DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    internal class UserItemViewModel : BaseViewModel
    {
        private readonly User _model;

        public UserItemViewModel(User model)
        {
            _model = model;
        }

        public int Id => _model.Id;

        public string? Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
            }
        }

        public string? Email
        {
            get => _model.Email;
            set
            {
                _model.Email = value;
                RaisePropertyChanged();
            }
        }
    }
}
