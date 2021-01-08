using Microsoft.EntityFrameworkCore;

namespace Implementation.DataAccess
{
    public static class DatabaseHelper
    {
        public static void Migrate()
        {
            new Db().Database.Migrate();
        }
    }
}