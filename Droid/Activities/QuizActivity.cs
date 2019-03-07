using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace XamarinGeoQuiz.Droid
{
    [Activity(Label = "XamarinGeoQuiz", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme.NoActionBar")]
    public class QuizActivity : AppCompatActivity
    {
        private Button _trueButton;
        private Button _falseButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our buttons from the layout resource,
            _trueButton = FindViewById<Button>(Resource.Id.true_button);
            _falseButton = FindViewById<Button>(Resource.Id.false_button);

            // Attach events to these buttons
            _trueButton.Click += TrueButtonClicked;
            _falseButton.Click += FalseButtonClicked;
        }

        private void TrueButtonClicked(object sender, EventArgs e)
        {
            Toast.MakeText(ApplicationContext, Resource.String.correct_toast, ToastLength.Short).Show();
        }

        private void FalseButtonClicked(object sender, EventArgs e)
        {
            Toast.MakeText(ApplicationContext, Resource.String.incorrect_toast, ToastLength.Short).Show();
        }
    }
}
