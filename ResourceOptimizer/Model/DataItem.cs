namespace ResourceOptimizer.Model
{
    public class DataItem
    {
        public string Title
        {
            get;
        }

        public string FirstStep { get; }
        public string SecondStep { get; }
        public string ThirdStep { get; }
        public string FourthStep { get; }
        public string FifthStep { get; }
        public string SuccesMessage { get; }

        public DataItem(string title, string firstStep, string secondStep, string thirdStep, string fourthStep, string fifthStep, string succesMessage)
        {
            Title = title;
            FirstStep = firstStep;
            SecondStep = secondStep;
            ThirdStep = thirdStep;
            FourthStep = fourthStep;
            FifthStep = fifthStep;
            SuccesMessage = succesMessage;
        }
    }
}