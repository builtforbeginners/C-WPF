using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskForQuipu.DBModel
{
    public class QuipuContext:DbContext
    {
        public static string connectionString = "Data Source=127.0.0.1\\Sense; Initial Catalog=Client; Integrated Security=False; User Id=sa; Password=Sense17*; MultipleActiveResultSets=True";

        public QuipuContext() : base(connectionString)
        {

        }

        public virtual DbSet<Client> Clients { get; set; }
    }
}
