
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
        public readonly User model;

        public UserItemViewModel(User model)
        {
            this.model = model;
        }

        public int Id => model.Id;

        public string? Name
        {
            get => model.Name;
            set
            {
                model.Name = value;
                RaisePropertyChanged();
            }
        }

        public string? Email
        {
            get => model.Email;
            set
            {
                model.Email = value;
                RaisePropertyChanged();
            }
        }
    }
}
