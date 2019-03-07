using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using XamarinGeoQuiz.Droid.Data;

namespace XamarinGeoQuiz.Droid
{
    [Activity(Label = "XamarinGeoQuiz", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme.NoActionBar")]
    public class QuizActivity : AppCompatActivity
    {
        private Button _trueButton;
        private Button _falseButton;
        private Button _nextButton;
        private TextView _questionTextView;
        private Question[] questionBank = new Question[]
        {
            new Question(Resource.String.question_australia, true),
            new Question(Resource.String.question_oceans, true),
            new Question(Resource.String.question_mideast, false),
            new Question(Resource.String.question_africa, false),
            new Question(Resource.String.question_americas, true),
            new Question(Resource.String.question_asia, true)
        };

        private int currentIndex = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            _trueButton = FindViewById<Button>(Resource.Id.true_button);
            _falseButton = FindViewById<Button>(Resource.Id.false_button);

            _trueButton.Click += TrueButtonClicked;
            _falseButton.Click += FalseButtonClicked;

            _nextButton = FindViewById<Button>(Resource.Id.next_button);

            _nextButton.Click += NextButtonClicked;

            _questionTextView = FindViewById<TextView>(Resource.Id.question_text_view);
            _questionTextView.Click += QuestionViewClicked;
            UpdateQuestion();
        }

        private void TrueButtonClicked(object sender, EventArgs e)
        {
            CheckAnswer(true);
        }

        private void FalseButtonClicked(object sender, EventArgs e)
        {
            CheckAnswer(false);
        }

        private void NextButtonClicked(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % questionBank.Length;
            UpdateQuestion();
        }

        private void QuestionViewClicked(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % questionBank.Length;
            UpdateQuestion();
        }

        private void UpdateQuestion()
        {
            int question = questionBank[currentIndex].GetTextResId();
            _questionTextView.SetText(question);
        }

        private void CheckAnswer(bool userPressedTrue)
        {
            bool answerIsTrue = questionBank[currentIndex].IsAnswerTrue();
            int messageResId = 0;

            if (userPressedTrue == answerIsTrue)
            {
                messageResId = Resource.String.correct_toast;
            }
            else
            {
                messageResId = Resource.String.incorrect_toast;
            }

            Toast.MakeText(this, messageResId, ToastLength.Short).Show();
        }
    }
}
