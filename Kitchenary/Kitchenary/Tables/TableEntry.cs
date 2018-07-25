using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


namespace TableClients
{
	public class PreferenceEntity : TableEntity
	{
		public PreferenceEntity(string user, string userId)
		{
			this.PartitionKey = user;
			this.RowKey = userId;
		}
		public PreferenceEntity()
		{ }
		public string dietPreference { get; set; }
		public string healthPreference { get; set; }
        public string otherPreference { get; set; }
        
    }

    public class PantryEntity : TableEntity
    {
        public PantryEntity(string user, string ingredientName)
        {
            this.PartitionKey = user;
            this.RowKey = ingredientName;
        }
        public PantryEntity()
        { }
       
        public string foodCategory { get; set; }

		public string quantity { get; set; }

		public DateTime ExpirationTime { get; set; }
	}

    
}
