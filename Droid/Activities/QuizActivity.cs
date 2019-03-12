using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using XamarinGeoQuiz.Droid.Activities;
using XamarinGeoQuiz.Droid.Data;

namespace XamarinGeoQuiz.Droid
{
    [Activity(Label = "XamarinGeoQuiz", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme.NoActionBar")]
    public class QuizActivity : AppCompatActivity
    {
        private const string Tag = "QuizActivity";
        private const string KeyIndex = "index";
        private const string KeyArray = "array";
        private const string KeyScore = "score";
        private const string KeyCheater = "cheater";
        private const string KeyCheatedArray = "cheated";
        private const int RequestCodeCheat = 0;

        private Button _trueButton;
        private Button _falseButton;
        private Button _cheatButton;
        private TextView _questionTextView;
        private ImageButton _nextButton;
        private ImageButton _prevButton;
        private int score = 0;

        private List<int> answeredQuestions = new List<int>();
        private List<int> cheatedQuestions = new List<int>();
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

            Log.Debug(Tag, "onCreate(Bundle) called");

            SetContentView(Resource.Layout.Main);

            if (savedInstanceState != null)
            {
                answeredQuestions = savedInstanceState.GetIntArray(KeyArray).ToList();
                cheatedQuestions = savedInstanceState.GetIntArray(KeyCheatedArray).ToList();
                score = savedInstanceState.GetInt(KeyScore);
                currentIndex = savedInstanceState.GetInt(KeyIndex);
            }

            InitFields();
            SetListeners();
            UpdateQuestion();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            Log.Info(Tag, "onSavedInstanceState");
            outState.PutIntArray(KeyArray, answeredQuestions.ToArray());
            outState.PutIntArray(KeyCheatedArray, cheatedQuestions.ToArray());
            outState.PutInt(KeyScore, score);
            outState.PutInt(KeyIndex, currentIndex);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Log.Info(Tag, "onActivityResult");
            if (resultCode != Result.Ok)
            {
                return;
            }

            if (requestCode == RequestCodeCheat)
            {
                if (data == null)
                {
                    return;
                } 

                if (CheatActivity.WasAnswerShown(data))
                {
                    cheatedQuestions.Add(currentIndex);
                }
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            Log.Debug(Tag, "onStart() called");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug(Tag, "onResume() called");
        }

        protected override void OnPause()
        {
            base.OnPause();
            Log.Debug(Tag, "onPause() called");
        }

        protected override void OnStop()
        {
            base.OnStop();
            Log.Debug(Tag, "onStop() called");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Log.Debug(Tag, "onDestroy() called");
        }

        private void TrueButtonClicked(object sender, EventArgs e)
        {
            CheckAnswer(true);
        }

        private void FalseButtonClicked(object sender, EventArgs e)
        {
            CheckAnswer(false);
        }

        private void CheatButtonClicked(object sender, EventArgs e)
        {
            Log.Info(Tag, "CheatActivity started");
            var answerIsTrue = questionBank[currentIndex].AnswerTrue;
            Intent intent = CheatActivity.NewIntent(this, answerIsTrue);
            StartActivityForResult(intent, RequestCodeCheat);
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

            if (answeredQuestions.Contains(currentIndex))
            {
                SetButtonsVisibility(false);
            }
            else
            {
                SetButtonsVisibility(true);
            }
        }

        private void CheckAnswer(bool userPressedTrue)
        {
            bool answerIsTrue = questionBank[currentIndex].AnswerTrue;
            int messageResId = 0;

            if (cheatedQuestions.Contains(currentIndex))
            {
                messageResId = Resource.String.judgement_toast;
            }
            else
            {
                if (userPressedTrue == answerIsTrue)
                {
                    messageResId = Resource.String.correct_toast;
                    score++;
                }
                else
                {
                    messageResId = Resource.String.incorrect_toast;
                }
            }

            questionBank[currentIndex].IsAnswered = true;
            answeredQuestions.Add(currentIndex);

            SetButtonsVisibility(false);
            Toast.MakeText(this, messageResId, ToastLength.Short).Show();

            if (questionBank.Length == answeredQuestions.Count)
            {
                string finalScore = string.Format(GetString(Resource.String.final_score_msg), score * 100 / questionBank.Length);
                Toast.MakeText(this, finalScore, ToastLength.Short).Show();
            }
        }

        private void SetButtonsVisibility(bool buttonsAreVisible)
        {
            if (buttonsAreVisible)
            {
                _trueButton.Visibility = Android.Views.ViewStates.Visible;
                _falseButton.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                _trueButton.Visibility = Android.Views.ViewStates.Invisible;
                _falseButton.Visibility = Android.Views.ViewStates.Invisible;
            }
        }

        private void InitFields()
        {
            _trueButton = FindViewById<Button>(Resource.Id.true_button);
            _falseButton = FindViewById<Button>(Resource.Id.false_button);
            _cheatButton = FindViewById<Button>(Resource.Id.cheat_button);
            _questionTextView = FindViewById<TextView>(Resource.Id.question_text_view);
            _nextButton = FindViewById<ImageButton>(Resource.Id.next_button);
            _prevButton = FindViewById<ImageButton>(Resource.Id.prev_button);
        }

        private void SetListeners()
        {
            _trueButton.Click += TrueButtonClicked;
            _falseButton.Click += FalseButtonClicked;
            _cheatButton.Click += CheatButtonClicked;
            _nextButton.Click += GoToNextQuestion;
            _prevButton.Click += GoToPrevQuestion;
            _questionTextView.Click += GoToNextQuestion;
        }
    }
}
