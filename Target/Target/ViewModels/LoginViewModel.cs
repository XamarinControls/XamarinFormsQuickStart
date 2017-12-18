using ReactiveUI;
using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Target.ViewModels
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        string labelWelcomeText;
        public string LabelWelcomeText
        {
            get { return labelWelcomeText; }
            private set { this.RaiseAndSetIfChanged(ref labelWelcomeText, value); }
        }

        private readonly ReactiveCommand<Unit, Unit> loginCommand;
        public LoginViewModel()
        {
            labelWelcomeText = "Welcome to " + Constants.AppName + "!";
            Greeting = "Login Page";
            var canLogin = Observable.Return<bool>(true); // you could do some logic here instead
            this.loginCommand = ReactiveCommand.CreateFromObservable(
                this.LoginAsync,
                canLogin);
        }
        public ReactiveCommand<Unit, Unit> LoginCommand => this.loginCommand;
        private IObservable<Unit> LoginAsync() =>
            Observable
                 // this allows the login button to fail/success randomly
                .Return(new Random().Next(0, 2) == 1)
                .Do(
                    success =>
                    {
                        MessagingCenter.Send<ILoginViewModel, bool>(this, "LoginStatus", success);
                    }
                )
                .Select(_ => Unit.Default);
    }
}
