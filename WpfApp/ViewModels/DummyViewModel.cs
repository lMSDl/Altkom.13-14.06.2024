using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    public class DummyViewModel
    {
        public string? Input { get; set; }
        public string? CopyInput { get; private set; }
        public ICommand CopyCommand { get; }

        public DummyViewModel()
        {
            CopyCommand = new RelayCommand(o => Copy());

        }

        public void Copy()
        {
            CopyInput = Input;
        }
    }
}
