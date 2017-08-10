using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contr__test
{
    class class_db
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public byte[] img_ { get; set; }
        public string name_ { get; set; }
        public string adress_ { get; set; }
        public string phone_ { get; set; }
        public string email_ { get; set; }
        public string site_ { get; set; }
        public string info_ { get; set; }
    }
}
