using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    [Activity(Label = "Validar Actividad", MainLauncher = true, Icon = "@drawable/icon")]
    public class ActivityPantalla2 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.layout1);
            var Mail = FindViewById<EditText>(Resource.Id.Mail);
            var Password = FindViewById<EditText>(Resource.Id.Password);
            var Validar = FindViewById<Button>(Resource.Id.Validar);
            var ResultText = FindViewById<EditText>(Resource.Id.TextResult);
            Validar.Click += (sender, e) =>
            {   
                

                validate();
            };
            async void validate()
            {

                SALLab07.ServiceClient ServiceClient = new SALLab07.ServiceClient();
                string StudentEmail = Mail.Text;// correo de registro xamarin 3.0
                string Password1 = Password.Text;// password de registro

                string myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                SALLab07.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password1, myDevice);

                if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    string Mensaje = ($"{Result.Status}\n{Result.Fullname}\n{Result.Token}");
                    var Builder = new Notification.Builder(this)
                        .SetContentTitle("́Validación de la actividad")
                        .SetContentText(Mensaje)
                        .SetSmallIcon(Resource.Drawable.Icon);
                    Builder.SetCategory(Notification.CategoryMessage);
                    var ObjectNotification = Builder.Build();
                    var Manager = GetSystemService(
                        Android.Content.Context.NotificationService) as NotificationManager;
                    Manager.Notify(0, ObjectNotification);
                }
				else
				{ Android.App.AlertDialog.Builder Builder = new AlertDialog.Builder(this);
				  ResultText.Text = ($"{Result.Status}\n{Result.Fullname}\n{Result.Token}");

				}
                    
            }
        }
    }
}