using AutoParking.Services;

using System;

namespace AutoParking.Commands
{
	class SwitchAdminPageCommand : Command
    {
        public override bool CanExecute(object parameter) => UserManager.AccountType == AccountType.Admin;

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
