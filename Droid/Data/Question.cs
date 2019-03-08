using System;

namespace XamarinGeoQuiz.Droid.Data
{
    public class Question
    {
        public Question(int textResId, bool answerTrue)
        {
            this.TextResId = textResId;
            this.AnswerTrue = answerTrue;
        }

        public int TextResId { get; set; }

        public bool AnswerTrue { get; set; }
    }
}
