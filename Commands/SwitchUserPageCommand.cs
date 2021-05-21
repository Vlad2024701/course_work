using AutoParking.Services;

using System;

namespace AutoParking.Commands
{
	class SwitchUserPageCommand : Command
    {
        public override bool CanExecute(object parameter) => UserManager.AccountType == AccountType.User;

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
