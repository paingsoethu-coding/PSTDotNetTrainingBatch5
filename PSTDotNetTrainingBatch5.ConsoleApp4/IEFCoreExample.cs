namespace PSTDotNetTrainingBatch5.ConsoleApp4;

public interface IEFCoreExample
{
    void Create(string title, string author, string content);
    void Delete(int id);
    void DeleteFlag(int id);
    void Edit(int id);
    void Read();
    void Update(int id, string title, string author, string content);
}