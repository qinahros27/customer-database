namespace src.FileHelper 
{
    public class FileHelper
    {
        private string _path;
        private FileInfo _fi;

        public FileHelper(string path)
        {
            _path = path;
            _fi = new FileInfo(path);
        }

        public string[]? GetAllData()
        {
            try
            {
                var data = File.ReadAllLines(_path);
                return data;
            }
            catch (Exception e)
            {
                throw ExceptionHandler.FetchDataException(e.Message);
            }
        }

        public void AddNewCustomer(string content)
        {
            try 
            {
                File.AppendAllText(_path, content);
            }
            catch (Exception e) 
            {
                throw ExceptionHandler.UpdateDataException(e.Message);
            }
            
        }
    }
}