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
            updateChore(1, "Wash Laundry", "Arthur Dent");

            
        }

        private int addChore(string Name, string Assignment)
        {
            return choreDb.insertTable(choreDb.tableName, Name, Assignment);
        }

        private void addExampleChores()
        {
            addChore("Wash dishes", "Ryan");
            addChore("Vacuum", "Ryan");
            addChore("Dust", null);
        }

        private int updateChore(int id, string choreName, string choreAssignment)
        {
            return choreDb.updateTable(choreDb.tableName, id, choreName, choreAssignment);
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
