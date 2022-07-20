namespace RecordsRest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FireSharp;
    using FireSharp.Config;
    using FireSharp.Interfaces;
    using FireSharp.Response;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class FirebaseDB
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "7IWxvtDuXkuiKqR3cz4NqtrWu6zQ2mM3XVdwcKPQ",
            BasePath = "https://records-a819b-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public FirebaseDB()
        {
            client = new FireSharp.FirebaseClient(config);
            
        }

        
        public Record Create(Record record)
        {
            try
            {
                var data = record;
                PushResponse response = client.Push("Records/", data);
                data.Id = response.Result.name;
                SetResponse setResponse = client.Set("Records/" + data.Id, data);
                if (response.Body.Equals(""))
                {
                    return null;
                }
                return record;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        public List<Record> Get()
        {
            
            FirebaseResponse response = client.Get("Records");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Record>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<Record>(((JProperty)item).Value.ToString()));
                }
            }
            return list;
        }

        public List<Record> Get(int limit)
        {
            FirebaseResponse response = client.Get("Records/limit?size="+limit);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Record>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<Record>(((JProperty)item).Value.ToString()));
                }
            }
            return list;
        }

        public List<Record> Get(string category = "",string group = "")
        {
            List<Record> allRecords = Get();
            if (!category.Equals("") && !group.Equals(""))
            {
                return allRecords.Where(r => r.Category.Equals(category) && r.Group.Equals(group)).ToList();
            }
            else if (!category.Equals("") && group.Equals(""))
            {
                return allRecords.Where(r => r.Category.Equals(category)).ToList();
            }
            else if (category.Equals("") && !group.Equals(""))
            {
                return allRecords.Where(r => r.Group.Equals(group)).ToList();
            }
            else
            {
                return allRecords;
            }
        }

        public void Delete(string id)
        {
            client.Delete("Records/" + id);
        }


    }
}