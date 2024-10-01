using Business_Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class DANotas
    {
        private readonly Connection _connection;

        public DANotas(Connection connection)
        {
            _connection = connection;
        }

        public List<BENotas> ListNotas()
        {
            try
            {
                string query = "call crud_notas.SP_L_NOTAS();";
                string result = _connection.ExecuteQuery(query);
                List<BENotas> items = JsonConvert.DeserializeObject<List<BENotas>>(result);
                return items;
            }
            catch (Exception ex)
            {
                return new List<BENotas>();
            }
        }

        public bool InsertNotas(BENotas _BENotas)
        {
            try
            {
                string query = $"call crud_notas.SP_I_NOTAS('{_BENotas.name}','{_BENotas.description}');";
                string result = _connection.ExecuteQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditNotas(BENotas _BENotas)
        {
            try
            {
                string query = $"call crud_notas.SP_U_NOTAS({_BENotas.id},'{_BENotas.name}','{_BENotas.description}');";
                string result = _connection.ExecuteQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteNotas(int id)
        {
            try
            {
                string query = $"call crud_notas.SP_D_NOTAS({id});";
                string result = _connection.ExecuteQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
