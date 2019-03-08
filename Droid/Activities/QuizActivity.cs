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
        private ImageButton _nextButton;
        private ImageButton _prevButton;
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

            _nextButton = FindViewById<ImageButton>(Resource.Id.next_button);
            _nextButton.Click += GoToNextQuestion;

            _prevButton = FindViewById<ImageButton>(Resource.Id.prev_button);
            _prevButton.Click += GoToPrevQuestion;

            _questionTextView = FindViewById<TextView>(Resource.Id.question_text_view);
            _questionTextView.Click += GoToNextQuestion;
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

        private void GoToNextQuestion(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % questionBank.Length;
            UpdateQuestion();
        }

        private void GoToPrevQuestion(object sender, EventArgs e)
        {
            if (currentIndex == 0)
            {
                currentIndex = questionBank.Length - 1;
            }
            else
            {
                currentIndex = (currentIndex - 1) % questionBank.Length;
            } 

            UpdateQuestion();
        }

        private void UpdateQuestion()
        {
            int question = questionBank[currentIndex].TextResId;
            _questionTextView.SetText(question);
        }

        private void CheckAnswer(bool userPressedTrue)
        {
            bool answerIsTrue = questionBank[currentIndex].AnswerTrue;
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
