namespace SWELearningApp
{
    public interface ITaggingApp
    {
        void Run();
    }

    public class TaggingApp : ITaggingApp
    {
        public void Run()
        {
            Console.WriteLine("Tagging App is running...");
        }
    }
}