using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using XamarinManageErrors.Droid.Infrastructure;
using XamarinManageErrors.Services;
using XamarinManageErrors.Services.Veemer;

namespace XamarinManageErrors.Droid.Activities
{
    [Activity(MainLauncher = true, Icon = "@mipmap/icon")]
    public class LoginActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

#if DEBUG
            GetUserName().Text = "miguel.martin@analyticalways.com";
            GetPassword().Text = "piloto@20";
#endif

            var login = FindViewById<Button>(Resource.Id.login);
            login.Click += Login_OnClick3;
        }

        private async void Login_OnClick3(object sender, EventArgs e)
        {
            Console.WriteLine($"Entrada 1");

            //await Task.Run(async () => await LongDurationOperationWithException3());
            //E1-E2-*
            //No bloquea el hilo principal. Este es un caso específico para Xamarin Android. 
            //"Se fuerza" a liberar el hilo principal ya que depende de estar en PCL y dispositivo móvil
            //Se genera excepción la app, se cae

            //await LongDurationOperationWithException3();
            //E1-E2-*
            //No bloquea el hilo principal
            //Se genera excepción la app, se cae

            //LongDurationOperationWithException3();
            //E1-E2-S1-*
            //La excepción no se genera y la app no se cae
            //Alternativa: 
            RunBackgroundTask(async () =>
            {
                Console.WriteLine($"Entrada 2.");
                await Task.Delay(3000);
                throw new DivideByZeroException();
                Console.WriteLine($"Salida 2.");
            });

            //LongDurationOperationWithException2();
            //E1-E2-S1-*
            //Genera excepción la app se cae

            //LongDurationOperationWithException();
            //E1-E2-*
            //Bloquea el hilo principal
            //Genera excepción la app se cae

            Console.WriteLine($"Salida 1");
        }

        private void RunBackgroundTask(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task LongDurationOperationWithException3()
        {
            Console.WriteLine($"Entrada 2.");
            await Task.Delay(3000);
            throw new DivideByZeroException();
            Console.WriteLine($"Salida 2.");
        }

        private async void LongDurationOperationWithException2()
        {
            Console.WriteLine($"Entrada 2.");
            await Task.Delay(3000);
            throw new DivideByZeroException();
            Console.WriteLine($"Salida 2.");
        }

        private void LongDurationOperationWithException()
        {
            Console.WriteLine($"Entrada 2. Bloqueo hilo");
            Task.Delay(3000).Wait();
            throw new DivideByZeroException();
            Console.WriteLine($"Salida 2. Fin bloqueo hilo.");
        }

        private async void Login_OnClick2(object sender, EventArgs e)
        {
            Console.WriteLine($"Entrada 1");

             await Task.Run(async()=> await LongDurationOperation3());
            //E1-E2-*-S2-S1
            //No bloquea el hilo principal. Este es un caso específico para Xamarin Android. 
            //"Se fuerza" a liberar el hilo principal ya que depende de estar en PCL y dispositivo móvil

            await LongDurationOperation3();
            //E1-E2-*-S2-S1
            //No bloquea el hilo principal

            //LongDurationOperation3();
            //E1-E2-S1-*-S2
            //El compilador avisa

            //LongDurationOperation2();
            //E1-E2-S1-*-S2

            //LongDurationOperation();
            //E1-E2-*-S2-S1
            //Bloquea el hilo principal

            Console.WriteLine($"Salida 1");
        }

        private async Task LongDurationOperation3()
        {
            Console.WriteLine($"Entrada 2.");
            await Task.Delay(3000);
            Console.WriteLine($"Salida 2.");
        }

        private async void LongDurationOperation2()
        {
            Console.WriteLine($"Entrada 2.");
            await Task.Delay(3000);
            Console.WriteLine($"Salida 2.");
        }

        private void LongDurationOperation()
        {
            Console.WriteLine($"Entrada 2. Bloqueo hilo");
            Task.Delay(3000).Wait();
            Console.WriteLine($"Salida 2. Fin bloqueo hilo.");
        }

        private async Task DoSomething()
        {
            Task.Delay(3000).Wait();
            throw new DivideByZeroException();
        }

        private async void Login_OnClick(object sender, EventArgs e)
        {
            ProgressDialog.Show();
            try
            {
                var accountService = new AccountService(new Authorization(GetUserName().Text, GetPassword().Text, PasswordType.Password));
                var loginResponse = await accountService.LoginAsync();
                SharedPreferencesManager.SaveUserName(loginResponse.UserName);
                SharedPreferencesManager.SavePassword(GetPassword().Text);
                SharedPreferencesManager.SaveLoginResponse(JsonConvert.SerializeObject(loginResponse));

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            catch (HttpWebApiException exception)
            {
                Toast.MakeText(this, $"{exception.InnerException.Message}", ToastLength.Long).Show();
            }
            finally
            {
                ProgressDialog.Dismiss();
            }
        }

        private EditText GetUserName()
        {
            return FindViewById<EditText>(Resource.Id.userName);
        }

        private EditText GetPassword()
        {
            return FindViewById<EditText>(Resource.Id.password);
        }
    }

}