using Helper;

namespace SpareManagement.Repository
{
    public class BaseRepository
    {
        protected MSSqlDBHelper _dbHelper;

        //protected LoggerWrapper _loggerWrapper;

        //public BaseRepository(LoggerWrapper loggerWrapper)
        //{
        //    _dbHelper = new MSSqlDBHelper();
        //    _loggerWrapper = loggerWrapper;
        //}


        public BaseRepository()
        {
            _dbHelper = new MSSqlDBHelper();
        }
    }
}
