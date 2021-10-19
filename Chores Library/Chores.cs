using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoresLibrary
{
    public class Chores
    {
        private SQLConnection choreDb { get; }

        public Chores()
        {
            choreDb = new SQLConnection();
        }

        public void run()
        {
            Console.WriteLine(choreDb.connectionString);
            choreDb.createDatabase("ChoresDB");
            choreDb.createTable("Chores");
            addExampleChores();
        }

        private void addChore(string Name, string Assignment)
        {
            choreDb.insertTable(choreDb.tableName, Name, Assignment);
        }

        private void addExampleChores()
        {
            addChore("Wash dishes", "Ryan");
            addChore("Vacuum", "Ryan");
            addChore("Dust", null);
        }

        private void updateChore()
        {
            throw new NotImplementedException();
        }

        private void deleteChore()
        {
            throw new NotImplementedException();
        }

        private void getChores()
        {
            throw new NotImplementedException();
        }
    }
}
