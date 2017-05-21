using Android.App;
using Android.Widget;
using Android.OS;

namespace PhoneApp
{
    [Activity(Label = "Phone App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly System.Collections.Generic.List<string> PhoneNumbers =
            new System.Collections.Generic.List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);
            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumerText);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
            var Validar = FindViewById<Button>(Resource.Id.Validar);
            var ResultText = FindViewById<EditText>(Resource.Id.TextResult);
            CallButton.Enabled = false;
            var TranslatedNumber = string.Empty;
            TranslateButton.Click += (object sender, System.EventArgs e) =>
              {
                  var Translator = new PhoneTranslator();
                  TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);
                  if (string.IsNullOrWhiteSpace(TranslatedNumber))
                  {
                    //No hay número a llamar
                    CallButton.Text = "Llamar";
                      CallButton.Enabled = false;

                  }
                  else
                  {
                      //Hay un posible numero telefonico a llamar
                      CallButton.Text = $"Llamar al {TranslatedNumber}";
                      CallButton.Enabled = true;

                  }
              };
            CallButton.Click += (object sender, System.EventArgs e) =>
              {
                //intenta marcar el numero telefonico
                var CallDialog = new AlertDialog.Builder(this);
                  CallDialog.SetMessage($"Llamar al número{TranslatedNumber}?");
                  CallDialog.SetNeutralButton("llamar", delegate
                  {
                      //Agregar  el número marcado a la lista de números marcados
                      PhoneNumbers.Add(TranslatedNumber);
                      //Habilitar el boton HistoryButton
                      CallHistoryButton.Enabled = true;

                    //crear un intento para marcar el numero telefonico
                    var CallIntent =
                          new Android.Content.Intent(Android.Content.Intent.ActionCall);
                      CallIntent.SetData(
                          Android.Net.Uri.Parse($"tel:{TranslatedNumber}"));
                      StartActivity(CallIntent);
                  });
                  CallDialog.SetNegativeButton("Cancelar", delegate { });
                  //mostrar el cuadro de dialogo al usuario y esperar una respuesta.
                  CallDialog.Show();
                  //validate();
                  //CallButton.Enabled = false;

                  
              };
            CallHistoryButton.Click += (sender, e) =>
              {
                  var Intent = new Android.Content.Intent(this,
                      typeof(CallHistoryActivity));
                  Intent.PutStringArrayListExtra("phone_numbers",
                      PhoneNumbers);
                  StartActivity(Intent);
              };
            Validar.Click += (sender, e) =>
            {
                SetContentView(Resource.Layout.layout1);
            };
            async void validate()
            {
                SALLab07.ServiceClient ServiceClient = new SALLab07.ServiceClient();
                string StudentEmail = "hernandez.hs@gmail.com";
                string Password = "Jaimeh1982.";
                string myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                SALLab07.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, myDevice);
                Android.App.AlertDialog.Builder Builder = new AlertDialog.Builder(this);
                ResultText.Text = ($"{Result.Status}\n{Result.Fullname}\n{Result.Token}");




            }
        }
        
    }
}

