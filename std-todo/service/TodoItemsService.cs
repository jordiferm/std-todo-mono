using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;


namespace stdtodo
{
	public class TodoItemsService
	{
		public TodoItemsService ()
		{
		}

		private String todoResourceUri(String id = null) {
			string uri = "http://localhost:8080/api/Todo";
			if ( !String.IsNullOrEmpty(id) ) {
				uri += "/" + id; 
			}
			return uri; 
		}

		public async Task<List<TodoItem>> findAllItems() {
			List<TodoItem> result = new List<TodoItem>(); 

			using (var httpClient = new HttpClient ()) {
				try {
					HttpResponseMessage response = await httpClient.GetAsync (todoResourceUri()); 
					if (response.IsSuccessStatusCode) {
						string responseJson = await response.Content.ReadAsStringAsync(); 
						result = JsonConvert.DeserializeObject<List<TodoItem>>(responseJson); 
					}
				}
				catch(Exception e) {
					Console.WriteLine("There was an error calling web service" + e.Message); 
				}
			}
			return result; 
		}

		public  async Task<Boolean> addItem(TodoItem item) {
			Boolean result = false; 
			string json = JsonConvert.SerializeObject(item);
			HttpContent content = new StringContent (json, Encoding.UTF8, "application/json"); 
				
			using (var httpClient = new HttpClient ()) {
				try {
					HttpResponseMessage response = await httpClient.PostAsync(todoResourceUri(), content );
					//WATCH: Here the server should return the created object or at least the object URI but something is wrong with the server. 
					/*string responseJson = await response.Content.ReadAsStringAsync(); 
					TodoItem resultItem = JsonConvert.DeserializeObject<TodoItem>(responseJson); */
					result = true;
				}
				catch(Exception e) {
					Console.WriteLine("There was an error calling web service" + e.Message); 
				}
			}
			return result; 
		}

		public async Task<Boolean> removeItem(TodoItem item) {
			Boolean result = false; 
			using (var httpClient = new HttpClient ()) {
				HttpResponseMessage response = await httpClient.DeleteAsync(todoResourceUri(item.Key)); 
				result = response.IsSuccessStatusCode; 
			}
			return result; 
		}

		public async Task<Boolean> updateItemStatus(TodoItem item, Boolean IsComplete) {
			Boolean result = false; 
			item.IsComplete = IsComplete; 
			string json = JsonConvert.SerializeObject(item);
			HttpContent content = new StringContent (json, Encoding.UTF8, "application/json"); 

			using (var httpClient = new HttpClient ()) {
				try {
					HttpResponseMessage response = await httpClient.PutAsync(todoResourceUri(item.Key), content );
					result = true;
				}
				catch(Exception e) {
					Console.WriteLine("There was an error calling web service" + e.Message); 
				}
			}
			return result; 
		}
	}
}

