namespace NBM.Filters;
using System.Text.Json;
using Newtonsoft.Json;

public class JSONService
{
    
        public void WriteToJson(object obj)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(obj);
                Console.WriteLine(jsonString);
        
                string filePath = @"../../../serialised.json";
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred creating Json: " + ex.Message);
            }
        }
}
    