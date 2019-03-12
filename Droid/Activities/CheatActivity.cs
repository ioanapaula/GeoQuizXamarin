using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace XamarinGeoQuiz.Droid.Activities
{
    [Activity(Label = "XamarinGeoQuiz", Theme = "@style/AppTheme.NoActionBar")]
    public class CheatActivity : AppCompatActivity
    {
        private const string ExtraAnswerIsTrue = "com.companyname.xamaringeoquiz.answer_is_true";
        private const string ExtraAnswerShown = "com.companyname.xamaringeoquiz.answer_shown";
        private const string KeyAnswer = "answer";
        private const string KeyCheater = "isCheater";
        private bool _answerIsTrue;
        private bool _isAnswerShown;
        private Button _showAnswerButton;
        private TextView _answerTextView;

        public static Intent NewIntent(Context packageContext, bool answerIsTrue)
        {
            Intent intent = new Intent(packageContext, typeof(CheatActivity));
            intent.PutExtra(ExtraAnswerIsTrue, answerIsTrue);
            return intent;
        }

        public static bool WasAnswerShown(Intent result)
        {
            return result.GetBooleanExtra(ExtraAnswerShown, false);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Cheat);

            var cheatIntent = Intent;
            _answerIsTrue = Intent.GetBooleanExtra(ExtraAnswerIsTrue, false);

            _showAnswerButton = FindViewById<Button>(Resource.Id.show_answer_button);
            _answerTextView = FindViewById<TextView>(Resource.Id.answer_text_view);

            if (savedInstanceState != null)
            {
                _answerIsTrue = savedInstanceState.GetBoolean(KeyAnswer);
                _isAnswerShown = savedInstanceState.GetBoolean(KeyCheater);
            }

            if (_isAnswerShown)
            {
                DisplayAnswer(_answerIsTrue);
                SetAnswerShownResult(_isAnswerShown);
            }

            _showAnswerButton.Click += AnswerButtonClicked;
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutBoolean(KeyAnswer, _answerIsTrue);
            outState.PutBoolean(KeyCheater, _isAnswerShown);
        }

        private void AnswerButtonClicked(object sender, EventArgs e)
        {
            _isAnswerShown = true;
            DisplayAnswer(_answerIsTrue);
            SetAnswerShownResult(_isAnswerShown);
        }

        private void SetAnswerShownResult(bool isAnswerShown)
        {
            Intent data = new Intent();
            data.PutExtra(ExtraAnswerShown, isAnswerShown);
            SetResult(Result.Ok, data);
        }

        private void DisplayAnswer(bool answer)
        {
            if (answer)
            {
                _answerTextView.SetText(Resource.String.true_button);
            }
            else
            {
                _answerTextView.SetText(Resource.String.false_button);
            }
        }
    }
}
