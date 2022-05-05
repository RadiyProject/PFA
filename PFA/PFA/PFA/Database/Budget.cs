using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFA.Database
{
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public float limit { get; set; }
        public bool hasLimit { get; set; }
        public string userId { get; set; }
        [Ignore]
        public List<Target> targets { get; set; } = new List<Target>();
        public List<Target> targetsN
        {
            get
            {
                if (targetsString != null)
                    return JsonConvert.DeserializeObject<List<Target>>(targetsString);
                else
                    return null;
            }
        }
        public string targetsString { get; set; }
        public void SetTargets()
        {
            targetsString = JsonConvert.SerializeObject(targets);
        }

        public Budget()
        {
        }
        public Budget(float limit)
        {
            this.limit = limit;
            GetLimit();
        }

        public bool GetLimit()
        {
            if (limit <= 0)
                hasLimit = false;
            else
                hasLimit = true;
            return hasLimit;
        }
    }
}
