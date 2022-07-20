using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace RecordsRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordsController : ControllerBase
    {
        FirebaseDB firebaseDB = new FirebaseDB();

        [HttpGet]
        public IEnumerable<Record> Get()
        {
            return firebaseDB.Get();
        }

        [HttpGet("params")]
        public IEnumerable<Record> Get(string category = "", string group = "")
        {
            return firebaseDB.Get(category, group);
        }

        [HttpPost]
        public Record Post([FromBody] Record record)
        {
            return firebaseDB.Create(record);
        }

        [HttpGet("limit")]
        public IEnumerable<Record> Get(int size)
        {
            return firebaseDB.Get(size);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            firebaseDB.Delete(id);
        }
    }
}
