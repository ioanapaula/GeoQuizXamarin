using System;

namespace XamarinGeoQuiz.Droid.Data
{
    public class Question
    {
        public Question(int textResId, bool answerTrue)
        {
            this.TextResId = textResId;
            this.AnswerTrue = answerTrue;
            this.IsAnswered = false;
        }

        public int TextResId { get; set; }

        public bool AnswerTrue { get; set; }

        public bool IsAnswered { get; set; }
    }
}
