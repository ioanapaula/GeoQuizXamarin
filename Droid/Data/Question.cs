using System;

namespace XamarinGeoQuiz.Droid.Data
{
    public class Question
    {
        private int textResId;
        private bool answerTrue;

        public Question(int textResId, bool answerTrue)
        {
            this.textResId = textResId;
            this.answerTrue = answerTrue;
        }

        public int GetTextResId()
        {
            return textResId;
        }

        public void SetTextResId(int textResId)
        {
            this.textResId = textResId;
        }

        public bool IsAnswerTrue()
        {
            return answerTrue;
        }

        public void SetAnswerTrue(bool answerTrue) 
        {
            this.answerTrue = answerTrue;
        }
    }
}
