namespace PSTDotNetTrainingBatch5.ConsoleApp4
{
    public interface IDapperExample
    {
        void Create(string title, string author, string content);
        void Delete();
        void Edit(int Id);
        void Read();
        void Update();
    }
}